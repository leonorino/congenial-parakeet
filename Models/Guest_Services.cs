namespace working.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Guest_Services
    {
        public int id { get; set; }

        public int? reservation_id { get; set; }

        public int? service_id { get; set; }

        public int quantity { get; set; }

        [Required]
        [StringLength(50)]
        public string status { get; set; }

        public virtual Reservation Reservation { get; set; }

        public virtual Service Service { get; set; }
    }
}
