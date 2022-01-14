using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Spitali.Models
{
    public partial class SpitalsContext : DbContext
    {
        public SpitalsContext()
        {
        }

        public SpitalsContext(DbContextOptions<SpitalsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Doctor> Doctors { get; set; }
        public virtual DbSet<HospitalName> HospitalNames { get; set; }
        public virtual DbSet<Spital> Spitals { get; set; }
   

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-6EM94PP;Initial Catalog=Spitals;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Department>(entity =>
            {
                entity.ToTable("Department");

                entity.Property(e => e.DepartmentId).HasColumnName("DepartmentID");

                entity.Property(e => e.Departments)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Doctor>(entity =>
            {
                entity.ToTable("Doctor");

                entity.Property(e => e.DoctorId).HasColumnName("DoctorID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Surname)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.Doctors)
                    .HasForeignKey(d => d.Departments)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Doctor__Departme__4F7CD00D");

                entity.HasOne(d => d.Hospitals)
                    .WithMany(p => p.Doctors)
                    .HasForeignKey(d => d.Hospital)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Doctor__Hospital__5070F446");
            });

            modelBuilder.Entity<HospitalName>(entity =>
            {
                entity.ToTable("HospitalName");

                entity.Property(e => e.HospitalNameId).HasColumnName("HospitalNameID");

                entity.Property(e => e.HospitalName1)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("HospitalName");
            });

            modelBuilder.Entity<Spital>(entity =>
            {
                entity.Property(e => e.SpitalId).HasColumnName("SpitalID");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.Spitals)
                    .HasForeignKey(d => d.Departments)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Spitals__Departm__48CFD27E");

                entity.HasOne(d => d.HospitalNames)
                    .WithMany(p => p.Spitals)
                    .HasForeignKey(d => d.HospitalName)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Spitals__Hospita__47DBAE45");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
