using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using posdb;

namespace posrepository
{
    public interface IProducts 
    {
        PRODUCT GetById(int id);
        
        PRODUCT Create(PRODUCT product);
        List<PRODUCT> Read();
        PRODUCT Update(PRODUCT product);
        PRODUCT Delete(PRODUCT product);
    }

    public class ProductsRepository : IProducts
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        public PRODUCT Create(PRODUCT product)
        {
            throw new NotImplementedException();
        }

        public PRODUCT Delete(PRODUCT product)
        {
            throw new NotImplementedException();
        }

        public PRODUCT GetById(int id)
        {
            var item = new PRODUCT();
            try
            {
                using (var context = new posedb())
                {
                    item = context.PRODUCTS.Where(x => x.id == id).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
                Logger.Error(ex.InnerException.Message);
            }
            return item;
        }

        public List<PRODUCT> Read()
        {
            throw new NotImplementedException();
        }

        public PRODUCT Update(PRODUCT product)
        {
            throw new NotImplementedException();
        }
    }
}
