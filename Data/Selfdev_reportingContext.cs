using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace IT_Inventory_rest_api.Data
{
    public partial class Selfdev_reportingContext : DbContext
    {
        public Selfdev_reportingContext()
        {
        }

        public Selfdev_reportingContext(DbContextOptions<Selfdev_reportingContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AdUsersTe> AdUsersTes { get; set; }
        public virtual DbSet<TeGepek> TeGepeks { get; set; }
        public virtual DbSet<TeLeltar> TeLeltars { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

                optionsBuilder.UseSqlServer("Server=hukemssql02;Database=Selfdev_reporting;persist security info=True;user id=self_ITInventory;password=fKHMy8BvxX4XzYbQ;MultipleActiveResultSets=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Hungarian_CI_AS");

            modelBuilder.Entity<AdUsersTe>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("AD_Users_TE");

                entity.Property(e => e.Cn)
                    .HasMaxLength(4000)
                    .HasColumnName("cn");

                entity.Property(e => e.DisplayName)
                    .HasMaxLength(4000)
                    .HasColumnName("displayName");

                entity.Property(e => e.Edmxid).HasColumnName("EDMXID");

                entity.Property(e => e.GivenName)
                    .HasMaxLength(4000)
                    .HasColumnName("givenName");

                entity.Property(e => e.ObjectGuid)
                    .HasMaxLength(4000)
                    .HasColumnName("objectGUID");

                entity.Property(e => e.ObjectSid)
                    .HasMaxLength(4000)
                    .HasColumnName("objectSid");

                entity.Property(e => e.PhysicalDeliveryOfficeName)
                    .HasMaxLength(4000)
                    .HasColumnName("physicalDeliveryOfficeName");

                entity.Property(e => e.SAmaccountName)
                    .HasMaxLength(4000)
                    .HasColumnName("sAMAccountName");

                entity.Property(e => e.Sn)
                    .HasMaxLength(4000)
                    .HasColumnName("sn");

                entity.Property(e => e.Title)
                    .HasMaxLength(4000)
                    .HasColumnName("title");
            });

            modelBuilder.Entity<TeGepek>(entity =>
            {
                entity.HasKey(e => e.Nid);

                entity.ToTable("TE_Gepek");

                entity.Property(e => e.Nid).HasColumnName("nid");

                entity.Property(e => e.ComputerName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Computer name");

                entity.Property(e => e.DeviceManufacturer)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Device manufacturer");

                entity.Property(e => e.DeviceModel)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Device model");

                entity.Property(e => e.SerialNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Serial number");
            });

            modelBuilder.Entity<TeLeltar>(entity =>
            {
                entity.HasKey(e => e.Nid);

                entity.ToTable("TE_Leltar");

                entity.Property(e => e.Nid).HasColumnName("nid");

                entity.Property(e => e.Csoport)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Felhasznalo)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Gyarto)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Hely)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.LeltariSzam)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("Leltari_Szam");

                entity.Property(e => e.Modell)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Nev)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Sorozatszam)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Statusz)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Tipusok)
                    .IsRequired()
                    .HasMaxLength(20);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
