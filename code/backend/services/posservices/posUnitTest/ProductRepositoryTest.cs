using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using posdb;
using posrepository;
namespace posUnitTest
{
    [TestClass]
    public class ProductRepositoryTest
    {
        public int id { get; set; } = 5;

        [TestMethod]
        public void Create_()
        {
            //PRODUCT item = new PRODUCT();
            //// item.id = 1;
            //item.idcstatus = (int)CSTATUS.ACTIVO;
            //item.name = "SOL Tubi-Papa ORIGINAL";
            //item.price = (decimal)13;
            //item.cost = (decimal)9.75;
            //item.existence = 1;
            //item.barcode = "7503020501623";

            //IProducts products = new ProductsRepository();
            //item = products.Create(item);
            ////this.id = item.id;
            //Assert.IsTrue(item.id > 0, "Item add");
        }

        //[TestMethod]
        //public void Update_()
        //{
        //    PRODUCT item = new PRODUCT();
        //    item.id = this.id;
        //    item.idcstatus = (int)CSTATUS.INACTIVO;
        //    item.name = "SOL Tubi-Papa ORIGINAL";
        //    item.price = (decimal)13;
        //    item.cost = (decimal)9.7;
        //    item.existence = 2;
        //    item.barcode = "7503020501623";

        //    IProducts products = new ProductsRepository();
        //    item = products.Update(item);
        //    Assert.IsTrue(item.id > 0, "Item update");
        //}

        //[TestMethod]
        //public void Delete_()
        //{
        //    IProducts products = new ProductsRepository();
        //    int idcstatus = (int)CSTATUS.INACTIVO;
        //    bool flag = products.Delete(this.id, idcstatus);
        //    Assert.IsTrue(flag, "Delete product");
        //}

    }
}
