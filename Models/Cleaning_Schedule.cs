namespace working.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Cleaning_Schedule
    {
        public int id { get; set; }

        public int? room_id { get; set; }

        [Column(TypeName = "date")]
        public DateTime cleaning_date { get; set; }

        public int? cleaner_id { get; set; }

        [Required]
        [StringLength(50)]
        public string status { get; set; }

        public virtual Room Room { get; set; }

        public virtual User User { get; set; }
    }
}
