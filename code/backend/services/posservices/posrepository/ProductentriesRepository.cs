using NLog;
using posdb;
using posrepository.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;

namespace posrepository
{
    public interface IProductentries
    {
        PRODUCTENTRy Create(ProductEntriesDTO itemparam);
        List<PRODUCTENTRy> Read(int id = 0, bool all = false);
        PRODUCTENTRy Update(ProductEntriesDTO itemparam);
        bool Delete(int id);
    }

    public class ProductentriesRepository : IProductentries
    {
        private static readonly Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        public PRODUCTENTRy Create(ProductEntriesDTO dto)
        {
            PRODUCTENTRy productEntry = new PRODUCTENTRy();
            try
            {
                using (var context = new posContext())
                {
                    using (var transaction = context.Database.BeginTransaction())
                    {
                        try
                        {
                            if (dto.idproductentries > 0)
                                productEntry = context.PRODUCTENTRIES.FirstOrDefault(x => x.id == dto.idproductentries);

                            productEntry.total = dto.unitary_cost * dto.quantity;
                            productEntry.create_date = PosUtil.ConvertToTimestamp(DateTime.Now);
                            productEntry.idcstatus = dto.idcstatus;
                            context.Entry<PRODUCTENTRy>(productEntry).State = EntityState.Added;
                            context.SaveChanges();

                            PRODUCTENTRYDETAIL pd = new PRODUCTENTRYDETAIL();
                            pd.idproductentries = productEntry.id;
                            pd.unitary_cost = dto.unitary_cost;
                            pd.quantity = dto.quantity;
                            pd.idproducts = dto.idproducts;
                            productEntry.PRODUCTENTRYDETAILS.Add(pd);

                            PRODUCT product = context.PRODUCTS.FirstOrDefault(x => x.id == dto.idproducts);

                            if (productEntry.idcstatus == (int)CSTATUS.ACTIVO)
                                product.existence = product.existence + dto.quantity;
                            else
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

        public bool Delete(int id)
        {
            bool flag = false;
            try
            {
                using (var context = new posContext())
                {
                    using (var transaction = context.Database.BeginTransaction())
                    {
                        try
                        {
                            var productentry = Read(id: id).FirstOrDefault();
                            if (productentry.idcstatus != (int)CSTATUS.ELIMINADO)
                            {
                                productentry.idcstatus = (int)CSTATUS.ELIMINADO;
                                var productentrydetails = productentry.PRODUCTENTRYDETAILS.FirstOrDefault(x => x.idproductentries == id);

                                var product = context.PRODUCTS.FirstOrDefault(x => x.id == productentrydetails.idproducts);
                                var extractitems = product.existence - productentrydetails.quantity;
                                product.existence = extractitems;
                                context.Entry(product).State = EntityState.Modified;
                                context.Entry(productentry).State = EntityState.Modified;
                                context.SaveChanges();
                                transaction.Commit();
                                flag = true;
                                Logger.Info(string.Format("PRODUCTENTRY DELETED {0} ", id));
                                Logger.Info(string.Format("PRODUCT MINUS{0} ", productentrydetails.quantity));
                            }
                            else
                                flag = false;
                        }
                        catch (Exception tex)
                        {
                            flag = false;
                            transaction.Rollback();
                            Logger.Error(tex.Message);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
                flag = false;
            }
            return flag;
        }

        public List<PRODUCTENTRy> Read(int id = 0, bool all = false)
        {
            List<PRODUCTENTRy> listItems = new List<PRODUCTENTRy>();
            try
            {
                using (var context = new posContext())
                {
                    // filters 
                    if (all)
                        listItems = context.PRODUCTENTRIES.Include(x => x.PRODUCTENTRYDETAILS).ToList();
                    else if (id > 0)
                        listItems = context.PRODUCTENTRIES.Include(x => x.PRODUCTENTRYDETAILS).Where(x => x.id == id).ToList();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }
            return listItems;
        }

        public PRODUCTENTRy Update(ProductEntriesDTO dto)
        {
            PRODUCTENTRy productEntryDB = new PRODUCTENTRy();
            try
            {
                if (dto.idproductentries <= 0)
                    return null;

                using (var context = new posContext())
                {
                    using (var transaction = context.Database.BeginTransaction())
                    {
                        try
                        {

                            productEntryDB = Read(id: dto.idproductentries).FirstOrDefault();
                            PRODUCTENTRYDETAIL productEntrydetailDB = productEntryDB.PRODUCTENTRYDETAILS.FirstOrDefault();

                            productEntryDB.idcstatus = dto.idcstatus;
                            productEntryDB.total = dto.unitary_cost * dto.quantity;
                            productEntrydetailDB.quantity = dto.quantity;
                            productEntrydetailDB.unitary_cost = dto.unitary_cost;

                            context.Entry<PRODUCTENTRy>(productEntryDB).State = EntityState.Added;
                            context.SaveChanges();

                            var productDB = context.PRODUCTS.FirstOrDefault(x => x.id == productEntrydetailDB.idproducts);

                            if (dto.idcstatus == (int)CSTATUS.ACTIVO)
                                productDB.existence = (productDB.existence - productEntrydetailDB.quantity) + dto.quantity;
                            else
                                productDB.existence = (productDB.existence - productEntrydetailDB.quantity) - dto.quantity;

                            context.Entry<PRODUCT>(productDB).State = EntityState.Modified;
                            context.SaveChanges();
                            transaction.Commit();

                            Logger.Info("PRODUCTENTRIES: id {0} total {1} create_date {2} idcstatus {3}", productEntryDB.id, productEntryDB.total, productEntryDB.create_date, productEntryDB.idcstatus);
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
                // productEntryParam.id = -1;
                Logger.Error(ex.Message);
                return null;
            }
            return productEntryDB;
        }
    }
}
