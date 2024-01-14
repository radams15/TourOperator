﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TourOperator.Contexts;

#nullable disable

namespace TourOperator.Migrations
{
    [DbContext(typeof(TourDbContext))]
    partial class TourDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("TourOperator.Models.Entities.Booking", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("Cancelled")
                        .HasColumnType("bit");

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateBooked")
                        .HasColumnType("datetime2");

                    b.Property<bool>("DepositPaid")
                        .HasColumnType("bit");

                    b.Property<int>("Due")
                        .HasColumnType("int");

                    b.Property<int>("TotalCost")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("Bookings");
                });

            modelBuilder.Entity("TourOperator.Models.Entities.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<string>("PassportNo")
                        .IsRequired()
                        .HasMaxLength(12)
                        .HasColumnType("nvarchar(12)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<string>("PhoneNo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.HasKey("Id");

                    b.HasIndex("Username")
                        .IsUnique();

                    b.ToTable("Customers");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            FullName = "Rhys Adams",
                            PassportNo = "",
                            Password = "d74ff0ee8da3b9806b18c877dbf29bbde50b5bd8e4dad7a3a725000feb82e8f1",
                            PhoneNo = "",
                            Username = "rhys"
                        });
                });

            modelBuilder.Entity("TourOperator.Models.Entities.Hotel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("OperatorId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OperatorId");

                    b.ToTable("Hotels");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "a hotel",
                            Name = "Leonardo Hotel Brighton",
                            OperatorId = 1
                        },
                        new
                        {
                            Id = 2,
                            Description = "a hotel",
                            Name = "London Marriott Hotel",
                            OperatorId = 2
                        },
                        new
                        {
                            Id = 3,
                            Description = "a hotel",
                            Name = "Travelodge Brighton Seafront",
                            OperatorId = 3
                        },
                        new
                        {
                            Id = 4,
                            Description = "a hotel",
                            Name = "Kings Hotel Brighton",
                            OperatorId = 4
                        },
                        new
                        {
                            Id = 5,
                            Description = "a hotel",
                            Name = "Hilton London Hotel",
                            OperatorId = 5
                        },
                        new
                        {
                            Id = 6,
                            Description = "a hotel",
                            Name = "Nevis Bank Inn, Fort William",
                            OperatorId = 6
                        });
                });

            modelBuilder.Entity("TourOperator.Models.Entities.Operator", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Operators");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Leonardo"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Marriott"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Travelodge"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Kings"
                        },
                        new
                        {
                            Id = 5,
                            Name = "Hilton"
                        },
                        new
                        {
                            Id = 6,
                            Name = "Independent"
                        });
                });

            modelBuilder.Entity("TourOperator.Models.Entities.Room", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("HotelId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PackageDiscount")
                        .HasColumnType("int");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.Property<int>("Spaces")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("HotelId");

                    b.ToTable("Rooms");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            HotelId = 1,
                            Name = "Single Bed",
                            PackageDiscount = 10,
                            Price = 18000,
                            Spaces = 20
                        },
                        new
                        {
                            Id = 2,
                            HotelId = 1,
                            Name = "Double Bed",
                            PackageDiscount = 20,
                            Price = 40000,
                            Spaces = 20
                        },
                        new
                        {
                            Id = 3,
                            HotelId = 1,
                            Name = "Family Suite",
                            PackageDiscount = 40,
                            Price = 52000,
                            Spaces = 20
                        },
                        new
                        {
                            Id = 4,
                            HotelId = 2,
                            Name = "Single Bed",
                            PackageDiscount = 10,
                            Price = 30000,
                            Spaces = 20
                        },
                        new
                        {
                            Id = 5,
                            HotelId = 2,
                            Name = "Double Bed",
                            PackageDiscount = 20,
                            Price = 50000,
                            Spaces = 20
                        },
                        new
                        {
                            Id = 6,
                            HotelId = 2,
                            Name = "Family Suite",
                            PackageDiscount = 40,
                            Price = 90000,
                            Spaces = 20
                        },
                        new
                        {
                            Id = 7,
                            HotelId = 3,
                            Name = "Single Bed",
                            PackageDiscount = 10,
                            Price = 8000,
                            Spaces = 20
                        },
                        new
                        {
                            Id = 8,
                            HotelId = 3,
                            Name = "Double Bed",
                            PackageDiscount = 20,
                            Price = 12000,
                            Spaces = 20
                        },
                        new
                        {
                            Id = 9,
                            HotelId = 3,
                            Name = "Family Suite",
                            PackageDiscount = 40,
                            Price = 15000,
                            Spaces = 20
                        },
                        new
                        {
                            Id = 10,
                            HotelId = 4,
                            Name = "Single Bed",
                            PackageDiscount = 10,
                            Price = 18000,
                            Spaces = 20
                        },
                        new
                        {
                            Id = 11,
                            HotelId = 4,
                            Name = "Double Bed",
                            PackageDiscount = 20,
                            Price = 40000,
                            Spaces = 20
                        },
                        new
                        {
                            Id = 12,
                            HotelId = 4,
                            Name = "Family Suite",
                            PackageDiscount = 40,
                            Price = 52000,
                            Spaces = 20
                        },
                        new
                        {
                            Id = 13,
                            HotelId = 5,
                            Name = "Single Bed",
                            PackageDiscount = 10,
                            Price = 37500,
                            Spaces = 20
                        },
                        new
                        {
                            Id = 14,
                            HotelId = 5,
                            Name = "Double Bed",
                            PackageDiscount = 20,
                            Price = 77500,
                            Spaces = 20
                        },
                        new
                        {
                            Id = 15,
                            HotelId = 5,
                            Name = "Family Suite",
                            PackageDiscount = 40,
                            Price = 95000,
                            Spaces = 20
                        },
                        new
                        {
                            Id = 16,
                            HotelId = 6,
                            Name = "Single Bed",
                            PackageDiscount = 10,
                            Price = 9000,
                            Spaces = 20
                        },
                        new
                        {
                            Id = 17,
                            HotelId = 6,
                            Name = "Double Bed",
                            PackageDiscount = 20,
                            Price = 10000,
                            Spaces = 20
                        },
                        new
                        {
                            Id = 18,
                            HotelId = 6,
                            Name = "Family Suite",
                            PackageDiscount = 40,
                            Price = 15500,
                            Spaces = 20
                        });
                });

            modelBuilder.Entity("TourOperator.Models.Entities.RoomBooking", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BookingId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateFrom")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateTo")
                        .HasColumnType("datetime2");

                    b.Property<int>("RoomId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BookingId")
                        .IsUnique();

                    b.HasIndex("RoomId");

                    b.ToTable("RoomBooking");
                });

            modelBuilder.Entity("TourOperator.Models.Entities.Tour", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Length")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.Property<int>("Spaces")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Tours");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "a tour",
                            Length = 6,
                            Name = "Real Britain",
                            Price = 120000,
                            Spaces = 30
                        },
                        new
                        {
                            Id = 2,
                            Description = "a tour",
                            Length = 12,
                            Name = "Best of Britain",
                            Price = 290000,
                            Spaces = 30
                        },
                        new
                        {
                            Id = 3,
                            Description = "a tour",
                            Length = 16,
                            Name = "Britain and Ireland Explorer",
                            Price = 200000,
                            Spaces = 40
                        });
                });

            modelBuilder.Entity("TourOperator.Models.Entities.TourBooking", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BookingId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateFrom")
                        .HasColumnType("datetime2");

                    b.Property<int>("TourId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BookingId")
                        .IsUnique();

                    b.HasIndex("TourId");

                    b.ToTable("TourBooking");
                });

            modelBuilder.Entity("TourOperator.Models.Entities.Booking", b =>
                {
                    b.HasOne("TourOperator.Models.Entities.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("TourOperator.Models.Entities.Hotel", b =>
                {
                    b.HasOne("TourOperator.Models.Entities.Operator", "Operator")
                        .WithMany()
                        .HasForeignKey("OperatorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Operator");
                });

            modelBuilder.Entity("TourOperator.Models.Entities.Room", b =>
                {
                    b.HasOne("TourOperator.Models.Entities.Hotel", "Hotel")
                        .WithMany("Rooms")
                        .HasForeignKey("HotelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Hotel");
                });

            modelBuilder.Entity("TourOperator.Models.Entities.RoomBooking", b =>
                {
                    b.HasOne("TourOperator.Models.Entities.Booking", null)
                        .WithOne("RoomBooking")
                        .HasForeignKey("TourOperator.Models.Entities.RoomBooking", "BookingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TourOperator.Models.Entities.Room", "Room")
                        .WithMany("Bookings")
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Room");
                });

            modelBuilder.Entity("TourOperator.Models.Entities.TourBooking", b =>
                {
                    b.HasOne("TourOperator.Models.Entities.Booking", null)
                        .WithOne("TourBooking")
                        .HasForeignKey("TourOperator.Models.Entities.TourBooking", "BookingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TourOperator.Models.Entities.Tour", "Tour")
                        .WithMany("Bookings")
                        .HasForeignKey("TourId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Tour");
                });

            modelBuilder.Entity("TourOperator.Models.Entities.Booking", b =>
                {
                    b.Navigation("RoomBooking");

                    b.Navigation("TourBooking");
                });

            modelBuilder.Entity("TourOperator.Models.Entities.Hotel", b =>
                {
                    b.Navigation("Rooms");
                });

            modelBuilder.Entity("TourOperator.Models.Entities.Room", b =>
                {
                    b.Navigation("Bookings");
                });

            modelBuilder.Entity("TourOperator.Models.Entities.Tour", b =>
                {
                    b.Navigation("Bookings");
                });
#pragma warning restore 612, 618
        }
    }
}
