using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TourOperator.Migrations
{
    /// <inheritdoc />
    public partial class AddOperatorSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Hotels",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Hilton London Hotel");

            migrationBuilder.UpdateData(
                table: "Hotels",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Kings Hotel Brighton");

            migrationBuilder.UpdateData(
                table: "Hotels",
                keyColumn: "Id",
                keyValue: 4,
                column: "Name",
                value: "London Marriott Hotel");

            migrationBuilder.UpdateData(
                table: "Hotels",
                keyColumn: "Id",
                keyValue: 5,
                column: "Name",
                value: "Leonardo Hotel Brighton");

            migrationBuilder.UpdateData(
                table: "Hotels",
                keyColumn: "Id",
                keyValue: 6,
                column: "Name",
                value: "Travelodge Brighton Seafront");

            migrationBuilder.InsertData(
                table: "Operators",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "NULL" },
                    { 2, "Hilton" },
                    { 3, "Kings" },
                    { 4, "Marriott" },
                    { 5, "Leonardo" },
                    { 6, "Travelodge" }
                });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 4,
                column: "Price",
                value: 37500);

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 5,
                column: "Price",
                value: 77500);

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 6,
                column: "Price",
                value: 95000);

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 7,
                column: "Price",
                value: 18000);

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 8,
                column: "Price",
                value: 40000);

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 9,
                column: "Price",
                value: 52000);

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 10,
                column: "Price",
                value: 30000);

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 11,
                column: "Price",
                value: 50000);

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 12,
                column: "Price",
                value: 90000);

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 13,
                column: "Price",
                value: 18000);

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 14,
                column: "Price",
                value: 40000);

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 15,
                column: "Price",
                value: 52000);

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 16,
                column: "Price",
                value: 8000);

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 17,
                column: "Price",
                value: 12000);

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 18,
                column: "Price",
                value: 15000);

            migrationBuilder.UpdateData(
                table: "Tours",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Length", "Name", "Price" },
                values: new object[] { 6, "Real Britain", 120000 });

            migrationBuilder.UpdateData(
                table: "Tours",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Length", "Name", "Price", "Spaces" },
                values: new object[] { 16, "Britain and Ireland Explorer", 200000, 40 });

            migrationBuilder.UpdateData(
                table: "Tours",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Length", "Name", "Price", "Spaces" },
                values: new object[] { 12, "Best of Britain", 290000, 30 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Operators",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Operators",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Operators",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Operators",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Operators",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Operators",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.UpdateData(
                table: "Hotels",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Leonardo Hotel Brighton");

            migrationBuilder.UpdateData(
                table: "Hotels",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Hilton London Hotel");

            migrationBuilder.UpdateData(
                table: "Hotels",
                keyColumn: "Id",
                keyValue: 4,
                column: "Name",
                value: "Kings Hotel Brighton");

            migrationBuilder.UpdateData(
                table: "Hotels",
                keyColumn: "Id",
                keyValue: 5,
                column: "Name",
                value: "Travelodge Brighton Seafront");

            migrationBuilder.UpdateData(
                table: "Hotels",
                keyColumn: "Id",
                keyValue: 6,
                column: "Name",
                value: "London Marriott Hotel");

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 4,
                column: "Price",
                value: 18000);

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 5,
                column: "Price",
                value: 40000);

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 6,
                column: "Price",
                value: 52000);

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 7,
                column: "Price",
                value: 37500);

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 8,
                column: "Price",
                value: 77500);

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 9,
                column: "Price",
                value: 95000);

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 10,
                column: "Price",
                value: 18000);

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 11,
                column: "Price",
                value: 40000);

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 12,
                column: "Price",
                value: 52000);

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 13,
                column: "Price",
                value: 8000);

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 14,
                column: "Price",
                value: 12000);

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 15,
                column: "Price",
                value: 15000);

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 16,
                column: "Price",
                value: 30000);

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 17,
                column: "Price",
                value: 50000);

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 18,
                column: "Price",
                value: 90000);

            migrationBuilder.UpdateData(
                table: "Tours",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Length", "Name", "Price" },
                values: new object[] { 12, "Best of Britain", 290000 });

            migrationBuilder.UpdateData(
                table: "Tours",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Length", "Name", "Price", "Spaces" },
                values: new object[] { 6, "Real Britain", 120000, 30 });

            migrationBuilder.UpdateData(
                table: "Tours",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Length", "Name", "Price", "Spaces" },
                values: new object[] { 16, "Britain and Ireland Explorer", 200000, 40 });
        }
    }
}
