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
    public interface ILostItemsRepository
    {
        LOSTITEM Create(LostItemDTO dto);
        LOSTITEMDETAIL CreateDetails(LostItemDTO dto);
        List<LOSTITEM> Read(int id = 0, string barcode = "", int idcstatus = -100, decimal price = -100, decimal cost = -100, int existence = -100, bool all = false);
        LOSTITEM Update(LostItemDTO dto);
        LOSTITEMDETAIL UpdateEntry(LostItemDTO dto);

        bool Delete(int id);
        bool DeleteDetail(int id);
    }
    public class LostItemsRepository : ILostItemsRepository
    {
        private static readonly Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        public LOSTITEM Create(LostItemDTO dto)
        {
            LOSTITEM li = new LOSTITEM();

            using (var context = new posContext())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        li.idcstatus = dto.idcstatus;
                        li.create_date = PosUtil.ConvertToTimestamp(DateTime.Now);
                        li.maker = dto.maker;
                        context.Entry<LOSTITEM>(li).State = EntityState.Added;
                        context.SaveChanges();

                        decimal tmptotal = 0;

                        foreach (var detail in dto.details)
                        {
                            PRODUCT product = context.PRODUCTS.FirstOrDefault(x => x.id == detail.idproducts);
                            LOSTITEMDETAIL lids = new LOSTITEMDETAIL();
                            lids.idlostitems = li.id;
                            lids.unitary_cost = product.unitary_cost;
                            lids.idproducts = detail.idproducts;
                            lids.quantity = detail.quantity;
                            context.Entry<LOSTITEMDETAIL>(lids).State = EntityState.Added;
                            context.SaveChanges();
                            tmptotal = tmptotal + (lids.unitary_cost * lids.quantity);
                            
                        }

                        li.total = tmptotal;
                        context.Entry<LOSTITEM>(li).State = EntityState.Modified;
                        context.SaveChanges();
                        transaction.Commit();
                        Logger.Info("PRODUCTENTRIES PRODUCTENTRIESDETAILS PRODUCT");
                    }
                    catch (Exception tex)
                    {
                        transaction.Rollback();
                        li.id = -1;
                        Logger.Error(tex.Message);
                        Logger.Error(tex.InnerException.Message);
                    }
                }
            }
            return li;

        }



        public LOSTITEMDETAIL CreateDetails(LostItemDTO dto)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public bool DeleteDetail(int id)
        {
            throw new NotImplementedException();
        }

        public List<LOSTITEM> Read(int id = 0, string barcode = "", int idcstatus = -100, decimal price = -100, decimal cost = -100, int existence = -100, bool all = false)
        {
            throw new NotImplementedException();
        }

        public LOSTITEM Update(LostItemDTO dto)
        {
            throw new NotImplementedException();
        }

        public LOSTITEMDETAIL UpdateEntry(LostItemDTO dto)
        {
            throw new NotImplementedException();
        }
    }
}
