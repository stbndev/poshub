using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using posdb;
using posrepository;
using posrepository.DTO;

namespace posUnitTest
{
    [TestClass]
    public class ProductentriesRepositoryTest
    {
        [TestMethod]
        public void Create_()
        {
            //ProductEntriesDTO productEntriesDTO = new ProductEntriesDTO();
            //productEntriesDTO.idcstatus = (int)CSTATUS.ACTIVO;
            //productEntriesDTO.unitary_cost = (decimal)9.50; // pasar a decimal
            //productEntriesDTO.quantity = 10;
            //productEntriesDTO.idproducts = 4;

            //IProductentries productentries = new ProductentriesRepository();
            //var item = productentries.Create(productEntriesDTO);
            //Assert.IsTrue(item.id > 0, "Item add");
        }

        //[TestMethod]
        //public void Delete_() 
        //{
        //    IProductentries productentries = new ProductentriesRepository();
        //    var item = productentries.Delete(18);
        //    Assert.IsTrue(item, "Item delete");
        //}

        [TestMethod]
        public void Update_()
        {
            ProductEntriesDTO productEntriesDTO = new ProductEntriesDTO();
            productEntriesDTO.idcstatus = (int)CSTATUS.ACTIVO;
            productEntriesDTO.unitary_cost = (decimal)10.77; 
            productEntriesDTO.quantity = 60;
            productEntriesDTO.idproducts = 1;
            productEntriesDTO.idproductentries = 1;

            IProductentries productentries = new ProductentriesRepository();
            var item = productentries.Update(productEntriesDTO);
            Assert.IsTrue(item.id > 0, "Item et");
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
