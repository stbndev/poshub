using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using posdb;
using System.Data.Entity;

namespace posrepository
{
    public interface IProducts
    {
        // PRODUCT GetById(int id);
        PRODUCT Create(PRODUCT product);
        List<PRODUCT> Read(int id = 0, string barcode = "", int idcstatus = -100, decimal price = -100, decimal cost = -100, int existence = -100, bool all = false);
        PRODUCT Update(PRODUCT product);
        PRODUCT Delete(PRODUCT product);
    }

    public class ProductsRepository : IProducts
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

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
                        Logger.Debug("{0} barcode unavailable ", product.barcode);
                        Logger.Info ("{0} barcode unavailable ",product.barcode);
                        Logger.Warn ("{0} barcode unavailable ",product.barcode);
                        Logger.Error("{0} barcode unavailable ",product.barcode);
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

        public PRODUCT Delete(PRODUCT product)
        {
            throw new NotImplementedException();
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
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }
            return listProducts;
        }

        public PRODUCT Update(PRODUCT product)
        {
            throw new NotImplementedException();
        }
    }
}
