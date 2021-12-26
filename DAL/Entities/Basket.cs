namespace DAL.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Basket")]
    public partial class Basket
    {
        [Key]
        public int Basket_Id { get; set; }

        public int Basket_Amount { get; set; }

        public decimal Basket_Price { get; set; }

        public int Basket_User { get; set; }

        public int Basket_Pizza { get; set; }

        public virtual Pizza Pizza { get; set; }

        public virtual User User { get; set; }
    }
}
