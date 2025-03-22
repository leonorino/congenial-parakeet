namespace working.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Room_Access
    {
        public int id { get; set; }

        public int? guest_id { get; set; }

        public int? room_id { get; set; }

        [Required]
        [StringLength(50)]
        public string access_card_code { get; set; }

        [StringLength(50)]
        public string status { get; set; }

        public virtual Guest Guest { get; set; }

        public virtual Room Room { get; set; }
    }
}
