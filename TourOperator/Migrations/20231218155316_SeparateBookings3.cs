using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TourOperator.Migrations
{
    /// <inheritdoc />
    public partial class SeparateBookings3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_RoomBooking_RoomBookingId",
                table: "Bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_TourBooking_TourBookingId",
                table: "Bookings");

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

            migrationBuilder.AddColumn<int>(
                name: "BookingId",
                table: "TourBooking",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BookingId",
                table: "RoomBooking",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TourBooking_BookingId",
                table: "TourBooking",
                column: "BookingId",
                unique: true,
                filter: "[BookingId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_RoomBooking_BookingId",
                table: "RoomBooking",
                column: "BookingId",
                unique: true,
                filter: "[BookingId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_RoomBooking_Bookings_BookingId",
                table: "RoomBooking",
                column: "BookingId",
                principalTable: "Bookings",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TourBooking_Bookings_BookingId",
                table: "TourBooking",
                column: "BookingId",
                principalTable: "Bookings",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoomBooking_Bookings_BookingId",
                table: "RoomBooking");

            migrationBuilder.DropForeignKey(
                name: "FK_TourBooking_Bookings_BookingId",
                table: "TourBooking");

            migrationBuilder.DropIndex(
                name: "IX_TourBooking_BookingId",
                table: "TourBooking");

            migrationBuilder.DropIndex(
                name: "IX_RoomBooking_BookingId",
                table: "RoomBooking");

            migrationBuilder.DropColumn(
                name: "BookingId",
                table: "TourBooking");

            migrationBuilder.DropColumn(
                name: "BookingId",
                table: "RoomBooking");

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

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_RoomBookingId",
                table: "Bookings",
                column: "RoomBookingId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_TourBookingId",
                table: "Bookings",
                column: "TourBookingId");

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
    }
}
