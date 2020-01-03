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
        bool Delete(SALE sale);
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
                            }



                            context.SaveChanges();

                            product.existence = product.existence - dto.quantity;

                            context.Entry<PRODUCT>(product).State = EntityState.Modified;

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
                productEntry.id = -1;
                Logger.Error(ex.Message);
            }
            return productEntry;
        }

        public bool Delete(SALE sale)
        {
            throw new NotImplementedException();
        }

        public List<SALE> Read(int id = 0, bool all = false)
        {
            throw new NotImplementedException();
        }

        public SALE Update(SALE sale)
        {
            throw new NotImplementedException();
        }
    }
}
