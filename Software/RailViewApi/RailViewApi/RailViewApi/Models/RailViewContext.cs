using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace RailViewApi.Models
{
    public partial class RailViewContext : DbContext
    {
        public RailViewContext()
        {
        }

        public RailViewContext(DbContextOptions<RailViewContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Alert> Alerts { get; set; } = null!;
        public virtual DbSet<Testnametable> Testnametables { get; set; } = null!;
        public virtual DbSet<Testtable> Testtables { get; set; } = null!;

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        //warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        //        optionsBuilder.UseMySql(config["Parent_Key:Child_Key"], Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.27-mysql"));
        //    }
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8mb4_0900_ai_ci")
                .HasCharSet("utf8mb4");

            modelBuilder.Entity<Alert>(entity =>
            {
                entity.ToTable("alerts");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Alert1)
                    .HasColumnType("enum('person','train','other')")
                    .HasColumnName("alert");

                entity.Property(e => e.AlertChecked).HasColumnName("alert_checked");

                entity.Property(e => e.CamId).HasColumnName("cam_id");

                entity.Property(e => e.LocationX).HasColumnName("location_x");

                entity.Property(e => e.LocationY).HasColumnName("location_y");

                entity.Property(e => e.Route)
                    .HasMaxLength(255)
                    .HasColumnName("route");

                entity.Property(e => e.Times)
                    .HasColumnType("timestamp")
                    .HasColumnName("times")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");
            });

            modelBuilder.Entity<Testnametable>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("testnametable");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .HasColumnName("First_Name")
                    .IsFixedLength();
            });

            modelBuilder.Entity<Testtable>(entity =>
            {
                entity.ToTable("testtable");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Title)
                    .HasMaxLength(255)
                    .HasColumnName("title");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
