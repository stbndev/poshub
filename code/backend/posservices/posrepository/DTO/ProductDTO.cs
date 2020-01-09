using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace posrepository.DTO
{
    public class ProductDTO
    {
        public int idproducts { get; set; }
        public int idproductentries { get; set; }
        public int idproductentrydetails { get; set; }
        public int idcstatus { get; set; }
        public long create_date { get; set; }
        public decimal unitary_cost { get; set; }
        public decimal unitary_price { get; set; }
        public int existence { get; set; }
        public int quantity { get; set; }
        public string name { get; set; }
        public string barcode { get; set; }
    }
}
