namespace DAL.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Order")]
    public partial class Order
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Order()
        {
            OrderString = new HashSet<OrderString>();
        }

        [Key]
        public int Order_Id { get; set; }

        public DateTime Order_CreationTime { get; set; }

        [Column(TypeName = "money")]
        public decimal Order_Price { get; set; }

        [Required]
        [StringLength(50)]
        public string Order_Address { get; set; }

        [Required]
        [StringLength(50)]
        public string Order_PhoneNumber { get; set; }

        public int Order_Client { get; set; }

        public int? Order_Courier { get; set; }

        public int? Order_Worker { get; set; }

        public int Order_Status { get; set; }

        public virtual User User { get; set; }

        public virtual User User1 { get; set; }

        public virtual Status Status { get; set; }

        public virtual User User2 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderString> OrderString { get; set; }
    }
}
