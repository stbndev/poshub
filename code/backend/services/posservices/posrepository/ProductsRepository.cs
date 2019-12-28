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
    public interface IProducts
    {
        // PRODUCT GetById(int id);
        PRODUCT Create(PRODUCT product);
        List<PRODUCT> Read(int id = 0, string barcode = "", int idcstatus = -100, decimal price = -100, decimal cost = -100, int existence = -100, bool all = false);
        PRODUCT Update(PRODUCT product);
        bool Delete(int id, int cstatus);
    }

    public class ProductsRepository : IProducts
    {
        private static readonly Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        public PRODUCT Create(PRODUCT product)
        {
            try
            {
                using (var context = new posContext())
                {
                    // check if no exist barcode 
                    var checkExist = Read(barcode: product.barcode);

                    if (checkExist.Count() > 0)
                    {
                        Logger.Error("barcode unavailable: {0}", product.barcode);
                        product.id = 0;
                        //return new PRODUCT();
                    }
                    else
                    {
                        context.Entry(product).State = EntityState.Added;
                        context.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                product.id = -1;
                Logger.Error(ex.Message);
            }
            return product;
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

        public PRODUCT Update(PRODUCT productargument)
        {
            PRODUCT item = new PRODUCT();
            try
            {
                using (var context = new posContext())
                {
                    item = context.PRODUCTS.FirstOrDefault(x => x.id == productargument.id);
                    item.name = productargument.name;
                    item.barcode = productargument.barcode;
                    item.idcstatus = productargument.idcstatus;
                    item.price = productargument.price;
                    item.cost= productargument.cost;
                    item.existence = productargument.existence;
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
