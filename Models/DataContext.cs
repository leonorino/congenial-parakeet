namespace working.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class DataContext : DbContext
    {
        public DataContext()
            : base("name=DataContext")
        {
        }

        public virtual DbSet<Cleaning_Schedule> Cleaning_Schedule { get; set; }
        public virtual DbSet<Guest_Services> Guest_Services { get; set; }
        public virtual DbSet<Guest> Guests { get; set; }
        public virtual DbSet<Integration> Integrations { get; set; }
        public virtual DbSet<Payment> Payments { get; set; }
        public virtual DbSet<Reservation> Reservations { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Room_Access> Room_Access { get; set; }
        public virtual DbSet<Room> Rooms { get; set; }
        public virtual DbSet<Service> Services { get; set; }
        public virtual DbSet<Statistics_Hotel> Statistics_Hotel { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cleaning_Schedule>()
                .Property(e => e.status)
                .IsUnicode(false);

            modelBuilder.Entity<Guest_Services>()
                .Property(e => e.status)
                .IsUnicode(false);

            modelBuilder.Entity<Guest>()
                .Property(e => e.first_name)
                .IsUnicode(false);

            modelBuilder.Entity<Guest>()
                .Property(e => e.last_name)
                .IsUnicode(false);

            modelBuilder.Entity<Guest>()
                .Property(e => e.document_number)
                .IsUnicode(false);

            modelBuilder.Entity<Guest>()
                .HasMany(e => e.Reservations)
                .WithOptional(e => e.Guest)
                .HasForeignKey(e => e.guest_id)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Guest>()
                .HasMany(e => e.Room_Access)
                .WithOptional(e => e.Guest)
                .HasForeignKey(e => e.guest_id)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Integration>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<Integration>()
                .Property(e => e.integration_details)
                .IsUnicode(false);

            modelBuilder.Entity<Payment>()
                .Property(e => e.amount)
                .HasPrecision(2, 2);

            modelBuilder.Entity<Payment>()
                .Property(e => e.payment_method)
                .IsUnicode(false);

            modelBuilder.Entity<Reservation>()
                .Property(e => e.status)
                .IsUnicode(false);

            modelBuilder.Entity<Reservation>()
                .HasMany(e => e.Guest_Services)
                .WithOptional(e => e.Reservation)
                .HasForeignKey(e => e.reservation_id)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Reservation>()
                .HasMany(e => e.Payments)
                .WithOptional(e => e.Reservation)
                .HasForeignKey(e => e.reservation_id)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Role>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<Role>()
                .HasMany(e => e.Users)
                .WithRequired(e => e.Role1)
                .HasForeignKey(e => e.role);

            modelBuilder.Entity<Room_Access>()
                .Property(e => e.access_card_code)
                .IsUnicode(false);

            modelBuilder.Entity<Room_Access>()
                .Property(e => e.status)
                .IsUnicode(false);

            modelBuilder.Entity<Room>()
                .Property(e => e.floor)
                .IsUnicode(false);

            modelBuilder.Entity<Room>()
                .Property(e => e.category)
                .IsUnicode(false);

            modelBuilder.Entity<Room>()
                .Property(e => e.status)
                .IsUnicode(false);

            modelBuilder.Entity<Room>()
                .HasMany(e => e.Cleaning_Schedule)
                .WithOptional(e => e.Room)
                .HasForeignKey(e => e.room_id)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Room>()
                .HasMany(e => e.Reservations)
                .WithOptional(e => e.Room)
                .HasForeignKey(e => e.room_id)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Room>()
                .HasMany(e => e.Room_Access)
                .WithOptional(e => e.Room)
                .HasForeignKey(e => e.room_id)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Service>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<Service>()
                .Property(e => e.price)
                .HasPrecision(2, 2);

            modelBuilder.Entity<Service>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<Service>()
                .HasMany(e => e.Guest_Services)
                .WithOptional(e => e.Service)
                .HasForeignKey(e => e.service_id)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Statistics_Hotel>()
                .Property(e => e.occupancy_rate)
                .HasPrecision(2, 2);

            modelBuilder.Entity<Statistics_Hotel>()
                .Property(e => e.adr)
                .HasPrecision(2, 2);

            modelBuilder.Entity<Statistics_Hotel>()
                .Property(e => e.revpar)
                .HasPrecision(2, 2);

            modelBuilder.Entity<Statistics_Hotel>()
                .Property(e => e.revenue)
                .HasPrecision(2, 2);

            modelBuilder.Entity<User>()
                .Property(e => e.last_name)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.first_name)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.username)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.password)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Cleaning_Schedule)
                .WithOptional(e => e.User)
                .HasForeignKey(e => e.cleaner_id)
                .WillCascadeOnDelete();
        }
    }
}
