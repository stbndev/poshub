﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace posrepository.DTO
{
    public class SalesDTO
    {
        public int idsales { get; set; }
        public decimal total { get; set; }
        public int idcstatus { get; set; }
        public string maker { get; set; }
        
        public List<SalesDetailsDTO> details { get; set; }
    }

    public class SalesDetailsDTO 
    {
        public int idsaledetails { get; set; }
        public decimal unitary_cost { get; set; }
        public decimal unitary_price { get; set; }
        public int quantity { get; set; }
        public int idproducts { get; set; }



    }
}
