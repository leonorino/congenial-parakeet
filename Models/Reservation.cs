namespace working.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Reservation
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Reservation()
        {
            Guest_Services = new HashSet<Guest_Services>();
            Payments = new HashSet<Payment>();
        }

        public int id { get; set; }

        public int? guest_id { get; set; }

        public int? room_id { get; set; }

        [Column(TypeName = "date")]
        public DateTime check_in_date { get; set; }

        [Column(TypeName = "date")]
        public DateTime check_out_date { get; set; }

        [Required]
        [StringLength(50)]
        public string status { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Guest_Services> Guest_Services { get; set; }

        public virtual Guest Guest { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Payment> Payments { get; set; }

        public virtual Room Room { get; set; }
    }
}
