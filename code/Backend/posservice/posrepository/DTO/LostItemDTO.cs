using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace posrepository.DTO
{
    public class LostItemDTO
    {
        public int idlostitems { get; set; }
        public int idcstatus { get; set; }
        public long date { get; set; }
        public decimal price { get; set; }
        
        public decimal total { get; set; }
        public string maker { get; set; }

        public List<LostItemDetailsDTO> details { get; set; } = new List<LostItemDetailsDTO>();

        public static implicit operator LostItemDTO(SalesDTO v)
        {
            throw new NotImplementedException();
        }
    }

    public class LostItemDetailsDTO 
    {
        public int idlostitemdetails { get; set; }
        public decimal cost { get; set; }
        public int quantity { get; set; }

        public int idproducts { get; set; }

    }
}
