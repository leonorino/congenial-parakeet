namespace working.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Room
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Room()
        {
            Cleaning_Schedule = new HashSet<Cleaning_Schedule>();
            Reservations = new HashSet<Reservation>();
            Room_Access = new HashSet<Room_Access>();
        }

        public int id { get; set; }

        [Required]
        [StringLength(100)]
        public string floor { get; set; }

        public int number { get; set; }

        [Required]
        [StringLength(100)]
        public string category { get; set; }

        [StringLength(50)]
        public string status { get; set; }

        public int? price { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cleaning_Schedule> Cleaning_Schedule { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Reservation> Reservations { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Room_Access> Room_Access { get; set; }
    }
}
