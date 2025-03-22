namespace working.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class User
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User()
        {
            Cleaning_Schedule = new HashSet<Cleaning_Schedule>();
        }

        public int id { get; set; }

        [Required]
        [StringLength(255)]
        public string last_name { get; set; }

        [Required]
        [StringLength(255)]
        public string first_name { get; set; }

        [Required]
        [StringLength(255)]
        public string username { get; set; }

        public int role { get; set; }

        [Required]
        [StringLength(255)]
        public string password { get; set; }

        public int? FailedLoginAttempts { get; set; }

        public bool? IsLocked { get; set; }

        public bool? IsFirstLogin { get; set; }

        public DateTime? LastLoginDate { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cleaning_Schedule> Cleaning_Schedule { get; set; }

        public virtual Role Role1 { get; set; }
    }
}
