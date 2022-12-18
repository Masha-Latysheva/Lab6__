﻿// <auto-generated />
using System;
using Logistic.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Logistic.DAL.Migrations
{
    [DbContext(typeof(LogisticContext))]
    [Migration("20220918124157_AddedTransportationDateColumn")]
    partial class AddedTransportationDateColumn
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.15")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Logistic.DAL.Entities.Car", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CarryingVolume")
                        .HasColumnType("int");

                    b.Property<int>("CarryingWeight")
                        .HasColumnType("int");

                    b.Property<string>("Mark")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("OrganizationId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OrganizationId");

                    b.ToTable("Cars");
                });

            modelBuilder.Entity("Logistic.DAL.Entities.Cargo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Volume")
                        .HasColumnType("int");

                    b.Property<int>("Weight")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Cargos");
                });

            modelBuilder.Entity("Logistic.DAL.Entities.Driver", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Passport")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Drivers");
                });

            modelBuilder.Entity("Logistic.DAL.Entities.Organization", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Organizations");
                });

            modelBuilder.Entity("Logistic.DAL.Entities.Point", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Points");
                });

            modelBuilder.Entity("Logistic.DAL.Entities.Rate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CarryingRate")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("VolumeRate")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Rates");
                });

            modelBuilder.Entity("Logistic.DAL.Entities.Route", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("EndPointId")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RouteLength")
                        .HasColumnType("int");

                    b.Property<int?>("StartPointId")
                        .IsRequired()
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EndPointId");

                    b.HasIndex("StartPointId");

                    b.ToTable("Routes");
                });

            modelBuilder.Entity("Logistic.DAL.Entities.Transportation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CarId")
                        .HasColumnType("int");

                    b.Property<int>("CargoCount")
                        .HasColumnType("int");

                    b.Property<int>("CargoId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("DriverId")
                        .HasColumnType("int");

                    b.Property<int>("RateId")
                        .HasColumnType("int");

                    b.Property<int>("RouteId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CarId");

                    b.HasIndex("CargoId");

                    b.HasIndex("DriverId");

                    b.HasIndex("RateId");

                    b.HasIndex("RouteId");

                    b.ToTable("Transportations");
                });

            modelBuilder.Entity("Logistic.DAL.Entities.Car", b =>
                {
                    b.HasOne("Logistic.DAL.Entities.Organization", "Organization")
                        .WithMany("Cars")
                        .HasForeignKey("OrganizationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Organization");
                });

            modelBuilder.Entity("Logistic.DAL.Entities.Route", b =>
                {
                    b.HasOne("Logistic.DAL.Entities.Point", "EndPoint")
                        .WithMany("EndRoutes")
                        .HasForeignKey("EndPointId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Logistic.DAL.Entities.Point", "StartPoint")
                        .WithMany("StartRoutes")
                        .HasForeignKey("StartPointId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("EndPoint");

                    b.Navigation("StartPoint");
                });

            modelBuilder.Entity("Logistic.DAL.Entities.Transportation", b =>
                {
                    b.HasOne("Logistic.DAL.Entities.Car", "Car")
                        .WithMany("Transportations")
                        .HasForeignKey("CarId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Logistic.DAL.Entities.Cargo", "Cargo")
                        .WithMany("Transportations")
                        .HasForeignKey("CargoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Logistic.DAL.Entities.Driver", "Driver")
                        .WithMany("Transportations")
                        .HasForeignKey("DriverId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Logistic.DAL.Entities.Rate", "Rate")
                        .WithMany("Transportations")
                        .HasForeignKey("RateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Logistic.DAL.Entities.Route", "Route")
                        .WithMany("Transportations")
                        .HasForeignKey("RouteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Car");

                    b.Navigation("Cargo");

                    b.Navigation("Driver");

                    b.Navigation("Rate");

                    b.Navigation("Route");
                });

            modelBuilder.Entity("Logistic.DAL.Entities.Car", b =>
                {
                    b.Navigation("Transportations");
                });

            modelBuilder.Entity("Logistic.DAL.Entities.Cargo", b =>
                {
                    b.Navigation("Transportations");
                });

            modelBuilder.Entity("Logistic.DAL.Entities.Driver", b =>
                {
                    b.Navigation("Transportations");
                });

            modelBuilder.Entity("Logistic.DAL.Entities.Organization", b =>
                {
                    b.Navigation("Cars");
                });

            modelBuilder.Entity("Logistic.DAL.Entities.Point", b =>
                {
                    b.Navigation("EndRoutes");

                    b.Navigation("StartRoutes");
                });

            modelBuilder.Entity("Logistic.DAL.Entities.Rate", b =>
                {
                    b.Navigation("Transportations");
                });

            modelBuilder.Entity("Logistic.DAL.Entities.Route", b =>
                {
                    b.Navigation("Transportations");
                });
#pragma warning restore 612, 618
        }
    }
}