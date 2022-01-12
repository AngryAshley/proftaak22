using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace RailViewApi.Models
{
    public partial class RailViewv2Context : DbContext
    {
        public RailViewv2Context()
        {
        }

        public RailViewv2Context(DbContextOptions<RailViewv2Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Accident> Accidents { get; set; } = null!;
        public virtual DbSet<Camera> Cameras { get; set; } = null!;
        public virtual DbSet<Coordinate> Coordinates { get; set; } = null!;
        public virtual DbSet<Department> Departments { get; set; } = null!;
        public virtual DbSet<Employee> Employees { get; set; } = null!;
        public virtual DbSet<EmployeeLogin> EmployeeLogins { get; set; } = null!;
        public virtual DbSet<Notification> Notifications { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql(config["ConnectionStrings:RailViewv2Db"], Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.27-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8mb4_0900_ai_ci")
                .HasCharSet("utf8mb4");

            modelBuilder.Entity<Accident>(entity =>
            {
                entity.ToTable("Accident");

                entity.Property(e => e.AccidentId).HasColumnName("Accident_ID");

                entity.Property(e => e.AccidentDate)
                    .HasColumnType("timestamp")
                    .ValueGeneratedOnAddOrUpdate()
                    .HasColumnName("Accident_Date")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.AccidentType)
                    .HasColumnType("enum('person','train','other')")
                    .HasColumnName("Accident_Type");
            });

            modelBuilder.Entity<Camera>(entity =>
            {
                entity.ToTable("Camera");

                entity.Property(e => e.CameraId).HasColumnName("Camera_ID");

                entity.Property(e => e.CameraName)
                    .HasMaxLength(255)
                    .HasColumnName("Camera_Name");

                entity.Property(e => e.CoordinatesId).HasColumnName("Coordinates_ID");

                entity.Property(e => e.StreamLink)
                    .HasMaxLength(255)
                    .HasColumnName("Stream_Link");
            });

            modelBuilder.Entity<Coordinate>(entity =>
            {
                entity.HasKey(e => e.CoordinatesId)
                    .HasName("PRIMARY");

                entity.Property(e => e.CoordinatesId).HasColumnName("Coordinates_ID");

                entity.Property(e => e.Latitude).HasColumnName("latitude");

                entity.Property(e => e.Longtitude).HasColumnName("longtitude");
            });

            modelBuilder.Entity<Department>(entity =>
            {
                entity.ToTable("Department");

                entity.Property(e => e.DepartmentId).HasColumnName("Department_ID");

                entity.Property(e => e.City).HasColumnType("text");

                entity.Property(e => e.HouseNumber).HasColumnName("House_Number");

                entity.Property(e => e.PostalCode)
                    .HasMaxLength(20)
                    .HasColumnName("Postal_Code");

                entity.Property(e => e.Streetname).HasColumnType("text");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("Employee");

                entity.Property(e => e.EmployeeId).HasColumnName("Employee_ID");

                entity.Property(e => e.DepartmentId).HasColumnName("Department_ID");

                entity.Property(e => e.LoginId).HasColumnName("Login_ID");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .HasColumnName("NAME");

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(20)
                    .HasColumnName("Phone_Number");
            });

            modelBuilder.Entity<EmployeeLogin>(entity =>
            {
                entity.HasKey(e => e.LoginId)
                    .HasName("PRIMARY");

                entity.ToTable("Employee_login");

                entity.Property(e => e.LoginId).HasColumnName("Login_ID");

                entity.Property(e => e.EmployeeId).HasColumnName("Employee_ID");

                entity.Property(e => e.Password).HasColumnType("text");

                entity.Property(e => e.Username).HasColumnType("text");
            });

            modelBuilder.Entity<Notification>(entity =>
            {
                entity.ToTable("Notification");

                entity.Property(e => e.NotificationId).HasColumnName("Notification_ID");

                entity.Property(e => e.AccidentId).HasColumnName("Accident_ID");

                entity.Property(e => e.CameraId).HasColumnName("Camera_ID");

                entity.Property(e => e.EmployeeId).HasColumnName("Employee_ID");

                entity.Property(e => e.RequiredAction).HasColumnName("Required_Action");

                entity.Property(e => e.StatusType)
                    .HasColumnType("enum('closed','open','unknown')")
                    .HasColumnName("Status_Type");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
