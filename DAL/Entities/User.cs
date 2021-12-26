namespace DAL.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("User")]
    public partial class User
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User()
        {
            Basket = new HashSet<Basket>();
            Order = new HashSet<Order>();
            Order1 = new HashSet<Order>();
            Order2 = new HashSet<Order>();
        }

        [Key]
        public int User_Id { get; set; }

        [Required]
        [StringLength(50)]
        public string User_Login { get; set; }

        [Required]
        [StringLength(50)]
        public string User_Password { get; set; }

        [Required]
        [StringLength(50)]
        public string User_FullName { get; set; }

        [StringLength(50)]
        public string User_PhoneNumer { get; set; }

        [Required]
        [StringLength(50)]
        public string User_Passport { get; set; }

        public int User_UserType { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Basket> Basket { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order> Order { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order> Order1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order> Order2 { get; set; }

        public virtual Type Type { get; set; }
    }
}
