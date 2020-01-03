using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using posdb;
using posrepository;
using posrepository.DTO;


namespace posUnitTest
{
    [TestClass]
    class SalesTest
    {
        [TestMethod]
        public void Create_()
        {
            SalesDTO salesDTO = new SalesDTO();
            salesDTO.total = 0;
            salesDTO.idcstatus = (int)CSTATUS.ACTIVO;
            salesDTO.maker = "TestClass";

            SalesDetailsDTO salesDetailsDTO = new SalesDetailsDTO();
            salesDetailsDTO.
            salesDTO.details.Add();

            IProducts products = new ProductsRepository();
            item = products.Create(item);
            //this.id = item.id;
            Assert.IsTrue(item.id > 0, "Item add");
        }
    }
}
