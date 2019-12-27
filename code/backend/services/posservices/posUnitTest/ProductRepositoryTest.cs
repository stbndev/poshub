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
        public void Create_()
        {
            PRODUCT item = new PRODUCT();
            item.idcstatus = 1;
            item.name = "SOL Tubi-Papa ORIGINAL";
            item.price = (decimal)13;
            item.cost = (decimal)9.7;
            item.existence = 10;
            item.barcode = "7503020501623";
                
            IProducts products = new ProductsRepository();
            item = products.Create(item);

            Assert.IsTrue(item.id == 0, "Not insert Item");
            //Assert.( item.id > 0, "Add Item");
        }
        //[TestMethod]
        //public void GetById_()
        //{
        //    IProducts products = new ProductsRepository();
        //    var item = products.GetById(1);
        //    Assert.IsNotNull(item,"Get Item");
        //}

    }
}
