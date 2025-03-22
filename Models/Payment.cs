namespace working.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Payment
    {
        public int id { get; set; }

        public int? reservation_id { get; set; }

        public decimal amount { get; set; }

        [Column(TypeName = "date")]
        public DateTime payment_date { get; set; }

        [StringLength(50)]
        public string payment_method { get; set; }

        public virtual Reservation Reservation { get; set; }
    }
}
