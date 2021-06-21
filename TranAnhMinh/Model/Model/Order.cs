namespace Model.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Order")]
    public partial class Order
    {
        public int ID { get; set; }

        public DateTime? CreateDate { get; set; }

        public int? CustomerID { get; set; }

        public int? TotalMoney { get; set; }

        public bool Status { get; set; }
    }
}
