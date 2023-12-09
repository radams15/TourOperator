using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TourOperator.Migrations
{
    /// <inheritdoc />
    public partial class AddSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Rooms_RoomId",
                table: "Bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Tours_TourId",
                table: "Bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_Hotels_Operators_OperatorId",
                table: "Hotels");

            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_Hotels_HotelId",
                table: "Rooms");

            migrationBuilder.DropIndex(
                name: "IX_Rooms_HotelId",
                table: "Rooms");

            migrationBuilder.DropIndex(
                name: "IX_Hotels_OperatorId",
                table: "Hotels");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_RoomId",
                table: "Bookings");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_TourId",
                table: "Bookings");

            migrationBuilder.InsertData(
                table: "Hotels",
                columns: new[] { "Id", "Description", "Name", "OperatorId" },
                values: new object[,]
                {
                    { 1, "a hotel", "Nevis Bank Inn, Fort William", 1 },
                    { 2, "a hotel", "Leonardo Hotel Brighton", 2 },
                    { 3, "a hotel", "Hilton London Hotel", 3 },
                    { 4, "a hotel", "Kings Hotel Brighton", 4 },
                    { 5, "a hotel", "Travelodge Brighton Seafront", 5 },
                    { 6, "a hotel", "London Marriott Hotel", 6 }
                });

            migrationBuilder.InsertData(
                table: "Rooms",
                columns: new[] { "Id", "HotelId", "Name", "Price", "Spaces" },
                values: new object[,]
                {
                    { 1, 1, "Single Bed", 9000, 20 },
                    { 2, 1, "Double Bed", 10000, 20 },
                    { 3, 1, "Family Suite", 15500, 20 },
                    { 4, 2, "Single Bed", 18000, 20 },
                    { 5, 2, "Double Bed", 40000, 20 },
                    { 6, 2, "Family Suite", 52000, 20 },
                    { 7, 3, "Single Bed", 37500, 20 },
                    { 8, 3, "Double Bed", 77500, 20 },
                    { 9, 3, "Family Suite", 95000, 20 },
                    { 10, 4, "Single Bed", 18000, 20 },
                    { 11, 4, "Double Bed", 40000, 20 },
                    { 12, 4, "Family Suite", 52000, 20 },
                    { 13, 5, "Single Bed", 8000, 20 },
                    { 14, 5, "Double Bed", 12000, 20 },
                    { 15, 5, "Family Suite", 15000, 20 },
                    { 16, 6, "Single Bed", 30000, 20 },
                    { 17, 6, "Double Bed", 50000, 20 },
                    { 18, 6, "Family Suite", 90000, 20 }
                });

            migrationBuilder.InsertData(
                table: "Tours",
                columns: new[] { "Id", "Description", "Length", "Name", "Price", "Spaces" },
                values: new object[,]
                {
                    { 1, "a tour", 12, "Best of Britain", 290000, 30 },
                    { 2, "a tour", 6, "Real Britain", 120000, 30 },
                    { 3, "a tour", 16, "Britain and Ireland Explorer", 200000, 40 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Hotels",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Hotels",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Hotels",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Hotels",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Hotels",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Hotels",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Tours",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Tours",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Tours",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_HotelId",
                table: "Rooms",
                column: "HotelId");

            migrationBuilder.CreateIndex(
                name: "IX_Hotels_OperatorId",
                table: "Hotels",
                column: "OperatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_RoomId",
                table: "Bookings",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_TourId",
                table: "Bookings",
                column: "TourId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Rooms_RoomId",
                table: "Bookings",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Tours_TourId",
                table: "Bookings",
                column: "TourId",
                principalTable: "Tours",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Hotels_Operators_OperatorId",
                table: "Hotels",
                column: "OperatorId",
                principalTable: "Operators",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_Hotels_HotelId",
                table: "Rooms",
                column: "HotelId",
                principalTable: "Hotels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
