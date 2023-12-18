using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TourOperator.Migrations
{
    /// <inheritdoc />
    public partial class SeparateBookings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RoomBookingId",
                table: "Bookings",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TourBookingId",
                table: "Bookings",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "RoomBooking",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoomId = table.Column<int>(type: "int", nullable: true),
                    DateFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateTo = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomBooking", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoomBooking_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TourBooking",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TourId = table.Column<int>(type: "int", nullable: true),
                    DateFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateTo = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TourBooking", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TourBooking_Tours_TourId",
                        column: x => x.TourId,
                        principalTable: "Tours",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_RoomBookingId",
                table: "Bookings",
                column: "RoomBookingId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_TourBookingId",
                table: "Bookings",
                column: "TourBookingId");

            migrationBuilder.CreateIndex(
                name: "IX_RoomBooking_RoomId",
                table: "RoomBooking",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_TourBooking_TourId",
                table: "TourBooking",
                column: "TourId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_RoomBooking_RoomBookingId",
                table: "Bookings",
                column: "RoomBookingId",
                principalTable: "RoomBooking",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_TourBooking_TourBookingId",
                table: "Bookings",
                column: "TourBookingId",
                principalTable: "TourBooking",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_RoomBooking_RoomBookingId",
                table: "Bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_TourBooking_TourBookingId",
                table: "Bookings");

            migrationBuilder.DropTable(
                name: "RoomBooking");

            migrationBuilder.DropTable(
                name: "TourBooking");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_RoomBookingId",
                table: "Bookings");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_TourBookingId",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "RoomBookingId",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "TourBookingId",
                table: "Bookings");
        }
    }
}
