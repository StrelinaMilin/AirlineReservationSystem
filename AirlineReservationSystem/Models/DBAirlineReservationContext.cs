using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using AirlineReservationSystem.Models;

#nullable disable

namespace AirlineReservationSystem.Models
{
    public partial class DBAirlineReservationContext : DbContext
    {
        public DBAirlineReservationContext()
        {
        }

        public DBAirlineReservationContext(DbContextOptions<DBAirlineReservationContext> options)
            : base(options)
        {
        }

        public virtual DbSet<BankDetail> BankDetails { get; set; }
        public virtual DbSet<TblAdmin> TblAdmins { get; set; }
        public virtual DbSet<TblFlightDetail> TblFlightDetails { get; set; }
        public virtual DbSet<TblFlightMaster> TblFlightMasters { get; set; }
        public virtual DbSet<TblPassenger> TblPassengers { get; set; }
        public virtual DbSet<TblUser> TblUsers { get; set; }
        public virtual DbSet<Tblticket> Tbltickets { get; set; }
        public virtual DbSet<Login> Logins { get; set; }
        public virtual DbSet<Flight> Flights { get; set; }
        public virtual DbSet<ReservedFlight> ReservedFlights { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data source=STS580L-STRELIN\\SQLEXPRESS;user id=sa;password=Strelina5*; Database=DBAirlineReservation;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<BankDetail>(entity =>
            {
                entity.HasKey(e => e.Bid)
                    .HasName("PK__BankDeta__C6D111C975723D53");

                entity.Property(e => e.Bid).ValueGeneratedNever();

                entity.Property(e => e.Balance).HasColumnType("money");

                entity.Property(e => e.Cardholdername)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("cardholdername");

                entity.Property(e => e.Cardno)
                    .HasMaxLength(16)
                    .IsUnicode(false)
                    .HasColumnName("cardno");

                entity.Property(e => e.Cardtype)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("cardtype");

                entity.Property(e => e.Cvv)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("cvv");
            });

            modelBuilder.Entity<TblAdmin>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("tblAdmin");

                entity.Property(e => e.AdminPassword)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Adminemail)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("adminemail");
            });

            modelBuilder.Entity<TblFlightDetail>(entity =>
            {
                entity.HasKey(e => e.FlightDetailsId)
                    .HasName("PK__tblFligh__324D8F2EFA57A23E");

                entity.ToTable("tblFlightDetails");

                entity.Property(e => e.FlightDetailsId).HasColumnName("FlightDetailsID");

                entity.Property(e => e.BusinessclassFare).HasColumnType("money");

                entity.Property(e => e.DepatureDate).HasColumnType("date");

                entity.Property(e => e.Destination)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.EconomyclassFare).HasColumnType("money");

                entity.Property(e => e.Source)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.HasOne(d => d.Flight)
                    .WithMany(p => p.TblFlightDetails)
                    .HasForeignKey(d => d.Flightid)
                    .HasConstraintName("FK__tblFlight__Fligh__3A81B327");
            });

            modelBuilder.Entity<TblFlightMaster>(entity =>
            {
                entity.HasKey(e => e.Flightid)
                    .HasName("PK__tblFligh__8A990066DA056B53");

                entity.ToTable("tblFlightMaster");

                entity.Property(e => e.BusinessSeats).HasColumnName("businessSeats");

                entity.Property(e => e.EconomySeats).HasColumnName("economySeats");

                entity.Property(e => e.Fname)
                    .HasMaxLength(40)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblPassenger>(entity =>
            {
                entity.HasKey(e => e.PassengerId)
                    .HasName("PK__tblPasse__88915FB0474DED2F");

                entity.ToTable("tblPassenger");

                entity.Property(e => e.FlightDetailsId).HasColumnName("FlightDetailsID");

                entity.Property(e => e.PassengerName)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Seatno)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Tid).HasColumnName("tid");

                entity.Property(e => e.Userid).HasColumnName("userid");

                entity.HasOne(d => d.FlightDetails)
                    .WithMany(p => p.TblPassengers)
                    .HasForeignKey(d => d.FlightDetailsId)
                    .HasConstraintName("FK__tblPassen__Fligh__3E52440B");

                entity.HasOne(d => d.TidNavigation)
                    .WithMany(p => p.TblPassengers)
                    .HasForeignKey(d => d.Tid)
                    .HasConstraintName("FK__tblPassenge__tid__5AEE82B9");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.TblPassengers)
                    .HasForeignKey(d => d.Userid)
                    .HasConstraintName("FK__tblPassen__useri__3D5E1FD2");
            });

            modelBuilder.Entity<TblUser>(entity =>
            {
                entity.HasKey(e => e.Userid)
                    .HasName("PK__tblUser__CBA1B257988E50F3");

                entity.ToTable("tblUser");

                entity.HasIndex(e => e.Emailid, "UQ__tblUser__7EDA1EE6904BE17A")
                    .IsUnique();

                entity.HasIndex(e => e.Phoneno, "UQ__tblUser__F2111EDA9C5636EB")
                    .IsUnique();

                entity.Property(e => e.Userid).HasColumnName("userid");

                entity.Property(e => e.Confirmpassword)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("confirmpassword");

                entity.Property(e => e.Dateofbirth).HasColumnType("date");

                entity.Property(e => e.Emailid)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Phoneno)
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Tblticket>(entity =>
            {
                entity.HasKey(e => e.Tid)
                    .HasName("PK__Tblticke__DC105B0FC9815CF8");

                entity.ToTable("Tblticket");

                entity.Property(e => e.Tid).HasColumnName("tid");

                entity.Property(e => e.Classtype)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("classtype");

                entity.Property(e => e.FlightDetailsId).HasColumnName("FlightDetailsID");

                entity.Property(e => e.ReturnDate)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.TotalAmount).HasColumnType("money");

                entity.Property(e => e.Type)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Userid).HasColumnName("userid");

                entity.HasOne(d => d.FlightDetails)
                    .WithMany(p => p.Tbltickets)
                    .HasForeignKey(d => d.FlightDetailsId)
                    .HasConstraintName("FK__Tblticket__Fligh__4222D4EF");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Tbltickets)
                    .HasForeignKey(d => d.Userid)
                    .HasConstraintName("FK__Tblticket__useri__412EB0B6");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

        public DbSet<AirlineReservationSystem.Models.Ticket> Ticket { get; set; }
    }
}
