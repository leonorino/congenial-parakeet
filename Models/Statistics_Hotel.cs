namespace working.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Statistics_Hotel
    {
        public int id { get; set; }

        [Column(TypeName = "date")]
        public DateTime date { get; set; }

        public decimal? occupancy_rate { get; set; }

        public decimal? adr { get; set; }

        public decimal? revpar { get; set; }

        public decimal? revenue { get; set; }
    }
}
