namespace posdb
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PRODUCTENTRIES")]
    public partial class PRODUCTENTRy
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PRODUCTENTRy()
        {
            PRODUCTENTRYDETAILS = new HashSet<PRODUCTENTRYDETAIL>();
        }

        public int id { get; set; }

        public decimal unitary_cost { get; set; }

        public int quantity { get; set; }

        public int idproducts { get; set; }

        public virtual PRODUCT PRODUCT { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PRODUCTENTRYDETAIL> PRODUCTENTRYDETAILS { get; set; }
    }
}
