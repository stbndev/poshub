using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using posdb;
using posrepository;

namespace posUnitTest
{
    [TestClass]
    public class ProductentriesRepositoryTest
    {
        [TestMethod]
        public void Create_()
        {
            
            PRODUCTENTRy item = new PRODUCTENTRy();
            item.idproducts = 4;
            item.quantity = 10;
            item.unitary_cost = (decimal)4.5;
            
            IProductentries productentries = new ProductentriesRepository();
            item = productentries.Create(item);
            Assert.IsTrue(item.id > 0, "Item add");
        }

        [TestMethod]
        public void Update_()
        {
            PRODUCTENTRy item = new PRODUCTENTRy();
            item.idproducts = 4;
            item.quantity = 1000;
            item.unitary_cost = (decimal)9.5;
            item.id = 1;

            IProductentries productentries = new ProductentriesRepository();
            item = productentries.Update(item);
            Assert.IsTrue(item.id > 0, "Item Set");
        }

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
