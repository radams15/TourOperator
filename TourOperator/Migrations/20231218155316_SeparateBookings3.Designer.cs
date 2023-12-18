﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TourOperator.Contexts;

#nullable disable

namespace TourOperator.Migrations
{
    [DbContext(typeof(TourDbContext))]
    [Migration("20231218155316_SeparateBookings3")]
    partial class SeparateBookings3
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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

                    b.Property<string>("CustomerUsername")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("DateBooked")
                        .HasColumnType("datetime2");

                    b.Property<bool>("DepositPaid")
                        .HasColumnType("bit");

                    b.Property<int>("Due")
                        .HasColumnType("int");

                    b.Property<int?>("RoomId")
                        .HasColumnType("int");

                    b.Property<int>("TotalCost")
                        .HasColumnType("int");

                    b.Property<int?>("TourId")
                        .HasColumnType("int");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CustomerUsername");

                    b.HasIndex("RoomId");

                    b.HasIndex("TourId");

                    b.ToTable("Bookings");
                });

            modelBuilder.Entity("TourOperator.Models.Entities.Customer", b =>
                {
                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PassportNo")
                        .HasColumnType("int");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PhoneNo")
                        .HasColumnType("int");

                    b.HasKey("Username");

                    b.ToTable("Customers");
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
                            Name = "Travelodge Brighton Seafront",
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
                            Name = "Kings Hotel Brighton",
                            OperatorId = 3
                        },
                        new
                        {
                            Id = 4,
                            Description = "a hotel",
                            Name = "Leonardo Hotel Brighton",
                            OperatorId = 4
                        },
                        new
                        {
                            Id = 5,
                            Description = "a hotel",
                            Name = "Nevis Bank Inn, Fort William",
                            OperatorId = 5
                        },
                        new
                        {
                            Id = 6,
                            Description = "a hotel",
                            Name = "Hilton London Hotel",
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
                            Name = "Travelodge"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Marriott"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Kings"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Leonardo"
                        },
                        new
                        {
                            Id = 5,
                            Name = "Independent"
                        },
                        new
                        {
                            Id = 6,
                            Name = "Hilton"
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
                            Price = 8000,
                            Spaces = 20
                        },
                        new
                        {
                            Id = 2,
                            HotelId = 1,
                            Name = "Double Bed",
                            Price = 12000,
                            Spaces = 20
                        },
                        new
                        {
                            Id = 3,
                            HotelId = 1,
                            Name = "Family Suite",
                            Price = 15000,
                            Spaces = 20
                        },
                        new
                        {
                            Id = 4,
                            HotelId = 2,
                            Name = "Single Bed",
                            Price = 30000,
                            Spaces = 20
                        },
                        new
                        {
                            Id = 5,
                            HotelId = 2,
                            Name = "Double Bed",
                            Price = 50000,
                            Spaces = 20
                        },
                        new
                        {
                            Id = 6,
                            HotelId = 2,
                            Name = "Family Suite",
                            Price = 90000,
                            Spaces = 20
                        },
                        new
                        {
                            Id = 7,
                            HotelId = 3,
                            Name = "Single Bed",
                            Price = 18000,
                            Spaces = 20
                        },
                        new
                        {
                            Id = 8,
                            HotelId = 3,
                            Name = "Double Bed",
                            Price = 40000,
                            Spaces = 20
                        },
                        new
                        {
                            Id = 9,
                            HotelId = 3,
                            Name = "Family Suite",
                            Price = 52000,
                            Spaces = 20
                        },
                        new
                        {
                            Id = 10,
                            HotelId = 4,
                            Name = "Single Bed",
                            Price = 18000,
                            Spaces = 20
                        },
                        new
                        {
                            Id = 11,
                            HotelId = 4,
                            Name = "Double Bed",
                            Price = 40000,
                            Spaces = 20
                        },
                        new
                        {
                            Id = 12,
                            HotelId = 4,
                            Name = "Family Suite",
                            Price = 52000,
                            Spaces = 20
                        },
                        new
                        {
                            Id = 13,
                            HotelId = 5,
                            Name = "Single Bed",
                            Price = 9000,
                            Spaces = 20
                        },
                        new
                        {
                            Id = 14,
                            HotelId = 5,
                            Name = "Double Bed",
                            Price = 10000,
                            Spaces = 20
                        },
                        new
                        {
                            Id = 15,
                            HotelId = 5,
                            Name = "Family Suite",
                            Price = 15500,
                            Spaces = 20
                        },
                        new
                        {
                            Id = 16,
                            HotelId = 6,
                            Name = "Single Bed",
                            Price = 37500,
                            Spaces = 20
                        },
                        new
                        {
                            Id = 17,
                            HotelId = 6,
                            Name = "Double Bed",
                            Price = 77500,
                            Spaces = 20
                        },
                        new
                        {
                            Id = 18,
                            HotelId = 6,
                            Name = "Family Suite",
                            Price = 95000,
                            Spaces = 20
                        });
                });

            modelBuilder.Entity("TourOperator.Models.Entities.RoomBooking", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("BookingId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("DateFrom")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateTo")
                        .HasColumnType("datetime2");

                    b.Property<int?>("RoomId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BookingId")
                        .IsUnique()
                        .HasFilter("[BookingId] IS NOT NULL");

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
                            Length = 12,
                            Name = "Best of Britain",
                            Price = 290000,
                            Spaces = 30
                        },
                        new
                        {
                            Id = 2,
                            Description = "a tour",
                            Length = 16,
                            Name = "Britain and Ireland Explorer",
                            Price = 200000,
                            Spaces = 40
                        },
                        new
                        {
                            Id = 3,
                            Description = "a tour",
                            Length = 6,
                            Name = "Real Britain",
                            Price = 120000,
                            Spaces = 30
                        });
                });

            modelBuilder.Entity("TourOperator.Models.Entities.TourBooking", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("BookingId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("DateFrom")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateTo")
                        .HasColumnType("datetime2");

                    b.Property<int?>("TourId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BookingId")
                        .IsUnique()
                        .HasFilter("[BookingId] IS NOT NULL");

                    b.HasIndex("TourId");

                    b.ToTable("TourBooking");
                });

            modelBuilder.Entity("TourOperator.Models.Entities.Booking", b =>
                {
                    b.HasOne("TourOperator.Models.Entities.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerUsername");

                    b.HasOne("TourOperator.Models.Entities.Room", null)
                        .WithMany("Bookings")
                        .HasForeignKey("RoomId");

                    b.HasOne("TourOperator.Models.Entities.Tour", null)
                        .WithMany("Bookings")
                        .HasForeignKey("TourId");

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
                    b.HasOne("TourOperator.Models.Entities.Booking", "Booking")
                        .WithOne("RoomBooking")
                        .HasForeignKey("TourOperator.Models.Entities.RoomBooking", "BookingId");

                    b.HasOne("TourOperator.Models.Entities.Room", "Room")
                        .WithMany()
                        .HasForeignKey("RoomId");

                    b.Navigation("Booking");

                    b.Navigation("Room");
                });

            modelBuilder.Entity("TourOperator.Models.Entities.TourBooking", b =>
                {
                    b.HasOne("TourOperator.Models.Entities.Booking", "Booking")
                        .WithOne("TourBooking")
                        .HasForeignKey("TourOperator.Models.Entities.TourBooking", "BookingId");

                    b.HasOne("TourOperator.Models.Entities.Tour", "Tour")
                        .WithMany()
                        .HasForeignKey("TourId");

                    b.Navigation("Booking");

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