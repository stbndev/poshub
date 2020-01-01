namespace posdb
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CSTATUS")]
    public partial class CSTATU
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CSTATU()
        {
            PRODUCTS = new HashSet<PRODUCT>();
        }

        public int id { get; set; }

        [Required]
        [StringLength(280)]
        public string name { get; set; }

        [Required]
        [StringLength(280)]
        public string maker { get; set; }

        public int create_date { get; set; }

        public int modification_date { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PRODUCT> PRODUCTS { get; set; }
    }
}
