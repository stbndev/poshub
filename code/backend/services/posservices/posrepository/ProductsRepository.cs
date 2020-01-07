using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.Entity;
using NLog;
using posrepository.DTO;
using mrgvn.db;
namespace posrepository
{
    public interface IProducts
    {
        PRODUCT Create(ProductDTO dto);
        PRODUCTENTRy CreateEntry(ProductDTO dto);
        List<PRODUCT> Read(int id = 0, string barcode = "", int idcstatus = -100, decimal price = -100, decimal cost = -100, int existence = -100, bool all = false);
        PRODUCT Update(ProductDTO dto);
        bool Delete(int id, int cstatus);
    }

    public class ProductsRepository : IProducts
    {
        private static readonly Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        public PRODUCT Create(ProductDTO dto)
        {
            PRODUCT p = new PRODUCT();
            try
            {
                using (var context = new posContext())
                {
                    // check if no exist barcode 
                    var checkExist = Read(barcode: dto.barcode);

                    if (checkExist.Count() > 0)
                    {
                        Logger.Error("barcode unavailable: {0}", dto.barcode);
                        p.id= 0;
                    }
                    else
                    {
                        p.name = dto.name;
                        p.barcode = dto.barcode;
                        p.idcstatus = dto.idcstatus;
                        p.price = dto.unitary_price;
                        p.unitary_cost = dto.unitary_cost;
                        p.existence = dto.existence;
                        context.Entry(p).State = EntityState.Added;
                        context.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                p.id = -1;
                Logger.Error(ex.Message);
            }
            return p;
        }

        public PRODUCTENTRy CreateEntry(ProductDTO dto)
        {
            PRODUCTENTRy pe  = new PRODUCTENTRy();
            try
            {
                using (var context = new posContext())
                {
                    using (var transaction = context.Database.BeginTransaction())
                    {
                        try
                        {
                            pe = new PRODUCTENTRy();
                            pe.total = dto.unitary_cost * dto.quantity;
                            pe.create_date = PosUtil.ConvertToTimestamp(DateTime.Now);
                            pe.idcstatus = dto.idcstatus;
                            context.Entry<PRODUCTENTRy>(pe).State = EntityState.Added;
                            context.SaveChanges();

                            PRODUCTENTRYDETAIL ped = new PRODUCTENTRYDETAIL();
                            ped.idproductentries = pe.id;
                            ped.unitary_cost = dto.unitary_cost;
                            ped.quantity = dto.quantity;
                            ped.idproducts = dto.idproducts;
                            context.Entry<PRODUCTENTRYDETAIL>(ped).State = EntityState.Added;
                            // context.SaveChanges();

                            PRODUCT product = context.PRODUCTS.FirstOrDefault(x => x.id == dto.idproducts);

                            if (pe.idcstatus == (int)CSTATUS.ACTIVO)
                                product.existence = product.existence + dto.quantity;
                            else
                                product.existence = product.existence - dto.quantity;

                            product.unitary_cost = dto.unitary_cost;

                            context.Entry<PRODUCT>(product).State = EntityState.Modified;
                            context.SaveChanges();
                            transaction.Commit();

                            Logger.Info("PRODUCTENTRIES PRODUCTENTRIESDETAILS PRODUCT");
                            return pe;
                        }
                        catch (Exception tex)
                        {
                            transaction.Rollback();
                            Logger.Error(tex.Message);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
                return null;
            }
            return pe;
        }

        public bool Delete(int id, int cstatus)
        {
            try
            {
                using (var context = new posContext())
                {
                    var item = Read(id: id).FirstOrDefault();
                    item.idcstatus = cstatus;
                    context.Entry(item).State = EntityState.Modified;
                    context.SaveChanges();
                }
                Logger.Info(string.Format("Product:{0} cstatus:{1}", id, cstatus));
                return true;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
                return false;
            }
        }

        public List<PRODUCT> Read(int id = 0, string barcode = "", int idcstatus = -100, decimal price = -100, decimal cost = -100, int existence = -100, bool all = false)
        {
            List<PRODUCT> listProducts = new List<PRODUCT>();
            try
            {
                using (var context = new posContext())
                {
                    // filters 
                    if (all)
                    {
                        listProducts = context.PRODUCTS.ToList();
                    }
                    else if (!string.IsNullOrEmpty(barcode))
                    {
                        listProducts = context.PRODUCTS.Where(x => x.barcode == barcode).ToList();
                    }
                    else if (id > 0)
                    {
                        listProducts = context.PRODUCTS.Where(x => x.id == id).ToList();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }
            return listProducts;
        }

        public PRODUCT Update(ProductDTO dto)
        {
            PRODUCT item = new PRODUCT();
            try
            {
                if (dto.idproducts <= 0)
                    return null;
                
                using (var context = new posContext())
                {
                    item = context.PRODUCTS.FirstOrDefault(x => x.id == dto.idproducts);
                    item.name = dto.name;
                    item.barcode = dto.barcode;
                    item.idcstatus = dto.idcstatus;
                    item.price = dto.unitary_price;
                    item.unitary_cost= dto.unitary_cost;
                    item.existence = dto.existence;
                    context.Entry(item).State = EntityState.Modified;
                    context.SaveChanges();
                    Logger.Info("Product Update {0} ", item.id);
                }
            }
            catch (Exception ex)
            {
                item.id = -1;
                Logger.Error(ex.Message);
            }
            return item;
        }
    }
}
