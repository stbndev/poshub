using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using posdb;
using posrepository.DTO;
using System.Data.Entity;

namespace posrepository
{
    public interface ISales
    {
        SALE Create(SalesDTO dto);
        List<SALE> Read(int id = 0, bool all = false);
        SALE Update(SALE sale);
        bool Delete(int id);
    }

    public class SalesRepository : ISales
    {
        private static readonly Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        public SALE Create(SalesDTO dto)
        {
            SALE sale = new SALE();
            try
            {
                using (var context = new posContext())
                {
                    using (var transaction = context.Database.BeginTransaction())
                    {
                        try
                        {
                            sale.total = 0;
                            sale.idcstatus = dto.idcstatus;
                            sale.maker = dto.maker;
                            sale.create_date = PosUtil.ConvertToTimestamp(DateTime.Now);
                            context.Entry<SALE>(sale).State = EntityState.Added;
                            context.SaveChanges();

                            decimal tmptotal = 0;
                            foreach (var item in dto.details)
                            {
                                PRODUCT product = context.PRODUCTS.FirstOrDefault(x => x.id == item.idproducts);
                                SALEDETAIL saledetails = new SALEDETAIL();
                                saledetails.idsales = sale.id;
                                saledetails.unitary_cost = product.unitary_cost;
                                saledetails.unitary_price = product.price;
                                saledetails.quantity = item.quantity;
                                saledetails.idproducts = item.idproducts;
                                context.Entry<SALEDETAIL>(saledetails).State = EntityState.Added;
                                tmptotal = tmptotal + (saledetails.unitary_price * saledetails.quantity);
                                product.existence = product.existence - item.quantity;
                                context.Entry<PRODUCT>(product).State = EntityState.Modified;
                            }
                            sale.total = tmptotal;
                            context.SaveChanges();
                            transaction.Commit();
                            Logger.Info("PRODUCTENTRIES PRODUCTENTRIESDETAILS PRODUCT");
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
                sale.id = -1;
                Logger.Error(ex.Message);
            }
            return sale;
        }

        public bool Delete(int id)
        {
            bool flag = false;
            try
            {
                using (posContext context = new posContext())
                {

                    SALE sale = Read(id: id).FirstOrDefault();

                    if (sale.idcstatus != (int)CSTATUS.ACTIVO)
                        return false;

                    using (var transaction = context.Database.BeginTransaction())
                    {
                        foreach (var item in sale.SALEDETAILS)
                        {
                            try
                            {
                                sale.idcstatus = (int)CSTATUS.ELIMINADO;
                                sale.modification_date = PosUtil.ConvertToTimestamp(DateTime.Now);
                                //  PRODUCT product = context.PRODUCTS.FirstOrDefault(x => x.id == item.idproducts);
                                PRODUCT product = item.PRODUCT;
                                product.existence = product.existence + item.quantity;
                                context.Entry<PRODUCT>(product).State = EntityState.Modified;
                                context.SaveChanges();
                            }
                            catch (Exception tx)
                            {
                                transaction.Rollback();
                                Logger.Error(tx.Message);
                            }
                        }
                        context.Entry<SALE>(sale).State = EntityState.Modified;
                        context.SaveChanges();
                        transaction.Commit();
                        flag = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }
            return flag;
        }

        public List<SALE> Read(int id = 0, bool all = false)
        {
            List<SALE> sales = new List<SALE>();
            try
            {
                using (var context = new posContext())
                {
                    // filters 
                    if (all)
                        sales = context.SALES.
                            Include(x => x.SALEDETAILS).
                            Include(x => x.SALEDETAILS.Select(sd => sd.PRODUCT)).ToList();
                    else if (id >= 0)
                        sales = context.SALES.Include(x => x.SALEDETAILS).
                                              Include(x => x.SALEDETAILS.Select(sd => sd.PRODUCT)).Where(x => x.id == id).ToList();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }
            return sales;
        }

        public SALE Update(SALE sale)
        {
            throw new NotImplementedException();
        }
    }
}
