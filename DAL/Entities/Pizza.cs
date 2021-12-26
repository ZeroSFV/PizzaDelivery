namespace DAL.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Pizza")]
    public partial class Pizza
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Pizza()
        {
            Basket = new HashSet<Basket>();
            OrderString = new HashSet<OrderString>();
        }

        [Key]
        public int Pizza_id { get; set; }

        [Required]
        public string Pizza_Name { get; set; }

        [Column(TypeName = "money")]
        public decimal Pizza_Price { get; set; }

        [Required]
        public string Pizza_Description { get; set; }

        public bool Pizza_Prescence { get; set; }

        [Required]
        public string Pizza_Consistance { get; set; }

        public int Pizza_Size { get; set; }

        public string Pizza_Photo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Basket> Basket { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderString> OrderString { get; set; }

        public virtual Size Size { get; set; }
    }
}
