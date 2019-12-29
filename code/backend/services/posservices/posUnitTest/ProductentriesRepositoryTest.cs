using System;
using System.Collections.Generic;
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
            item.total = (decimal)5;
            item.idcstatus = 4;
            item.PRODUCTENTRYDETAILS = new List<PRODUCTENTRYDETAIL>();
            
            PRODUCTENTRYDETAIL tmp = new PRODUCTENTRYDETAIL();
            tmp.unitary_cost = 9; // pasar a decimal
            tmp.quantity = 10;
            tmp.idproducts = 7;
            item.PRODUCTENTRYDETAILS.Add(tmp);

            IProductentries productentries = new ProductentriesRepository();
            item = productentries.Create(item);
            Assert.IsTrue(item.id > 0, "Item add");
        }

        private long ConvertToTimestamp(DateTime value)
        {
            long epoch = (value.Ticks - 621355968000000000) / 10000000;
            return epoch;
        }

        //[TestMethod]
        //public void Update_()
        //{
        //PRODUCTENTRy item = new PRODUCTENTRy();
        //item.idproducts = 4;
        //item.quantity = 1000;
        //item.unitary_cost = (decimal)9.5;
        //item.id = 1;

        //IProductentries productentries = new ProductentriesRepository();
        //item = productentries.Update(item);
        //Assert.IsTrue(item.id > 0, "Item Set");
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
