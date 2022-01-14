﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Spitali.Models;

namespace Spitali.Migrations
{
    [DbContext(typeof(SpitalsContext))]
    [Migration("20220113001702_another")]
    partial class another
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.12")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Spitali.Models.Department", b =>
                {
                    b.Property<int>("DepartmentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("DepartmentID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Departments")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.HasKey("DepartmentId");

                    b.ToTable("Department");
                });

            modelBuilder.Entity("Spitali.Models.Doctor", b =>
                {
                    b.Property<int>("DoctorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("DoctorID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Departments")
                        .HasColumnType("int");

                    b.Property<int>("Hospital")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.Property<int>("Number")
                        .HasColumnType("int");

                    b.Property<int>("Salary")
                        .HasColumnType("int");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.HasKey("DoctorId");

                    b.HasIndex("Departments");

                    b.HasIndex("Hospital");

                    b.ToTable("Doctor");
                });

            modelBuilder.Entity("Spitali.Models.Fields", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FieldName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Fields");
                });

            modelBuilder.Entity("Spitali.Models.HospitalName", b =>
                {
                    b.Property<int>("HospitalNameId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("HospitalNameID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("HospitalName1")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("HospitalName");

                    b.Property<int>("InstitutionType")
                        .HasColumnType("int");

                    b.HasKey("HospitalNameId");

                    b.ToTable("HospitalName");
                });

            modelBuilder.Entity("Spitali.Models.Spital", b =>
                {
                    b.Property<int>("SpitalId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("SpitalID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(500)
                        .IsUnicode(false)
                        .HasColumnType("varchar(500)");

                    b.Property<int>("Departments")
                        .HasColumnType("int");

                    b.Property<int>("EmployeeNumber")
                        .HasColumnType("int");

                    b.Property<int>("HospitalName")
                        .HasColumnType("int");

                    b.HasKey("SpitalId");

                    b.HasIndex("Departments");

                    b.HasIndex("HospitalName");

                    b.ToTable("Spitals");
                });

            modelBuilder.Entity("Spitali.Models.Doctor", b =>
                {
                    b.HasOne("Spitali.Models.Department", "Department")
                        .WithMany("Doctors")
                        .HasForeignKey("Departments")
                        .HasConstraintName("FK__Doctor__Departme__4F7CD00D")
                        .IsRequired();

                    b.HasOne("Spitali.Models.HospitalName", "Hospitals")
                        .WithMany("Doctors")
                        .HasForeignKey("Hospital")
                        .HasConstraintName("FK__Doctor__Hospital__5070F446")
                        .IsRequired();

                    b.Navigation("Department");

                    b.Navigation("Hospitals");
                });

            modelBuilder.Entity("Spitali.Models.Spital", b =>
                {
                    b.HasOne("Spitali.Models.Department", "Department")
                        .WithMany("Spitals")
                        .HasForeignKey("Departments")
                        .HasConstraintName("FK__Spitals__Departm__48CFD27E")
                        .IsRequired();

                    b.HasOne("Spitali.Models.HospitalName", "HospitalNames")
                        .WithMany("Spitals")
                        .HasForeignKey("HospitalName")
                        .HasConstraintName("FK__Spitals__Hospita__47DBAE45")
                        .IsRequired();

                    b.Navigation("Department");

                    b.Navigation("HospitalNames");
                });

            modelBuilder.Entity("Spitali.Models.Department", b =>
                {
                    b.Navigation("Doctors");

                    b.Navigation("Spitals");
                });

            modelBuilder.Entity("Spitali.Models.HospitalName", b =>
                {
                    b.Navigation("Doctors");

                    b.Navigation("Spitals");
                });
#pragma warning restore 612, 618
        }
    }
}
