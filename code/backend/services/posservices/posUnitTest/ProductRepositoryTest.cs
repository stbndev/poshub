using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using posdb;
using posrepository;
namespace posUnitTest
{
    [TestClass]
    public class ProductRepositoryTest
    {
        [TestMethod]
        public void GetById_()
        {
            IProducts products = new ProductsRepository();
            var item = products.GetById(1);
            Assert.IsNotNull(item,"Get Item");

        }
    }
}
