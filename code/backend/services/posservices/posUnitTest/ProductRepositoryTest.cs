using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using posdb;
using posrepository;
using posrepository.DTO;
namespace posUnitTest
{
    [TestClass]
    public class ProductRepositoryTest
    {
        public int id { get; set; } = 5;

        //[TestMethod]
        //public void ProductCreate_()
        //{
        //    ProductDTO dto = new ProductDTO();
        //    dto.name = "HALLS EXTRA STRONG";
        //    dto.barcode = "7622210427106";
        //    dto.idcstatus = (int)CSTATUS.ACTIVO;
        //    dto.price = (decimal)7;
        //    dto.unitary_cost= (decimal)4.58;
        //    dto.existence = 12;
        //    IProducts products = new ProductsRepository();
        //    var item = products.Create(dto);
        //    //this.id = item.id;
        //    Assert.IsTrue(item.id > 0, "Item add");
        //}

        [TestMethod]
        public void ProductEntryCreate_()
        {
            ProductDTO dto = new ProductDTO();
            dto.idcstatus = (int)CSTATUS.ACTIVO;
            dto.unitary_cost = (decimal)4.58;
            dto.quantity = 1;
            dto.idproducts = 2;

            IProducts products = new ProductsRepository();
            var item = products.CreateEntry(dto);
            
            Assert.IsTrue(item.id > 0, "Item add");
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
