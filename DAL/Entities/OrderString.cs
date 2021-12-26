namespace DAL.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("OrderString")]
    public partial class OrderString
    {
        [Key]
        public int OrderString_Id { get; set; }

        public int OrderString_Count { get; set; }

        public int OrderString_Order { get; set; }

        public int OrderString_Pizza { get; set; }

        public virtual Order Order { get; set; }

        public virtual Pizza Pizza { get; set; }
    }
}
