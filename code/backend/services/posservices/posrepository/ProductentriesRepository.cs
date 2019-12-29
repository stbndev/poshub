using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using posdb;
using System.Data.Entity;
using NLog;

namespace posrepository
{
    public interface IProductentries
    {

        PRODUCTENTRy Create(PRODUCTENTRy itemparam);
        List<PRODUCTENTRy> Read(int id = 0, bool all = false);
        PRODUCTENTRy Update(PRODUCTENTRy itemparam);
        bool Delete(int id);
    }

    public class ProductentriesRepository : IProductentries
    {
        private static readonly Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        public PRODUCTENTRy Create(PRODUCTENTRy itemparam)
        {
            try
            {
                using (var context = new posContext())
                {
                    using (var transaction = context.Database.BeginTransaction())
                    {
                        try
                        {
                            itemparam.create_date = PosUtil.ConvertToTimestamp(DateTime.Now);
                            context.Entry<PRODUCTENTRy>(itemparam).State = EntityState.Added;

                            var tmpitem = itemparam.PRODUCTENTRYDETAILS.FirstOrDefault();

                            PRODUCTENTRYDETAIL itemanother = new PRODUCTENTRYDETAIL();
                            itemanother.idproductentries = itemparam.id;
                            itemanother.unitary_cost = tmpitem.unitary_cost;
                            itemanother.quantity = tmpitem.quantity;
                            itemanother.idproducts = tmpitem.idproducts;

                            // context.Entry<PRODUCTENTRYDETAIL>(itemanother).State = EntityState.Added;
                            context.SaveChanges();
                            transaction.Commit();
                            Logger.Info("id {0} total {1} create_date {2} idcstatus {3}", itemparam.id, itemparam.total, itemparam.create_date, itemparam.idcstatus);
                            Logger.Info("id {0} idproductentries {1} unity_cost {2} quantity {3} idproducts {4}", itemanother.id, itemanother.idproductentries, itemanother.unitary_cost, itemanother.quantity, itemanother.idproducts);
                        }
                        catch (Exception)
                        {
                            transaction.Rollback();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                itemparam.id = -1;
                Logger.Error(ex.Message);
            }
            return itemparam;
        }

        public bool Delete(int id)
        {
            try
            {
                using (var context = new posContext())
                {
                    var item = Read(id: id).FirstOrDefault();
                    item.idcstatus = (int)CSTATUS.ELIMINADO;
                    context.Entry(item).State = EntityState.Modified;
                    context.SaveChanges();
                }
                Logger.Info(string.Format("DELETED {0} ", id));
                return true;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
                return false;
            }


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
                    {
                        listItems = context.PRODUCTENTRIES.ToList();
                    }
                    else if (id > 0)
                    {
                        listItems = context.PRODUCTENTRIES.Where(x => x.id == id).ToList();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }
            return listItems;
        }

        public PRODUCTENTRy Update(PRODUCTENTRy itemparam)
        {
            PRODUCTENTRy item = new PRODUCTENTRy();
            try
            {
                using (var context = new posContext())
                {
                    item = context.PRODUCTENTRIES.FirstOrDefault(x => x.id == itemparam.id);
                    //item.unitary_cost = itemparam.unitary_cost;
                    //item.quantity = itemparam.quantity;
                    //item.idproducts = itemparam.idproducts;
                    context.Entry(item).State = EntityState.Modified;
                    context.SaveChanges();
                    Logger.Info("PRODUCTENTRy Update {0} ", item.id);
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
