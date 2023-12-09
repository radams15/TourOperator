using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TourOperator.Migrations
{
    /// <inheritdoc />
    public partial class RenameNULLOperator : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Hotels",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Travelodge Brighton Seafront");

            migrationBuilder.UpdateData(
                table: "Hotels",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "London Marriott Hotel");

            migrationBuilder.UpdateData(
                table: "Hotels",
                keyColumn: "Id",
                keyValue: 4,
                column: "Name",
                value: "Leonardo Hotel Brighton");

            migrationBuilder.UpdateData(
                table: "Hotels",
                keyColumn: "Id",
                keyValue: 5,
                column: "Name",
                value: "Nevis Bank Inn, Fort William");

            migrationBuilder.UpdateData(
                table: "Hotels",
                keyColumn: "Id",
                keyValue: 6,
                column: "Name",
                value: "Hilton London Hotel");

            migrationBuilder.UpdateData(
                table: "Operators",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Travelodge");

            migrationBuilder.UpdateData(
                table: "Operators",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Marriott");

            migrationBuilder.UpdateData(
                table: "Operators",
                keyColumn: "Id",
                keyValue: 4,
                column: "Name",
                value: "Leonardo");

            migrationBuilder.UpdateData(
                table: "Operators",
                keyColumn: "Id",
                keyValue: 5,
                column: "Name",
                value: "Independent");

            migrationBuilder.UpdateData(
                table: "Operators",
                keyColumn: "Id",
                keyValue: 6,
                column: "Name",
                value: "Hilton");

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 1,
                column: "Price",
                value: 8000);

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 2,
                column: "Price",
                value: 12000);

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 3,
                column: "Price",
                value: 15000);

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 4,
                column: "Price",
                value: 30000);

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 5,
                column: "Price",
                value: 50000);

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 6,
                column: "Price",
                value: 90000);

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
                value: 9000);

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 14,
                column: "Price",
                value: 10000);

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 15,
                column: "Price",
                value: 15500);

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 16,
                column: "Price",
                value: 37500);

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 17,
                column: "Price",
                value: 77500);

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 18,
                column: "Price",
                value: 95000);

            migrationBuilder.UpdateData(
                table: "Tours",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Length", "Name", "Price" },
                values: new object[] { 12, "Best of Britain", 290000 });

            migrationBuilder.UpdateData(
                table: "Tours",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Length", "Name", "Price" },
                values: new object[] { 6, "Real Britain", 120000 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Hotels",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Nevis Bank Inn, Fort William");

            migrationBuilder.UpdateData(
                table: "Hotels",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Hilton London Hotel");

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

            migrationBuilder.UpdateData(
                table: "Operators",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "NULL");

            migrationBuilder.UpdateData(
                table: "Operators",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Hilton");

            migrationBuilder.UpdateData(
                table: "Operators",
                keyColumn: "Id",
                keyValue: 4,
                column: "Name",
                value: "Marriott");

            migrationBuilder.UpdateData(
                table: "Operators",
                keyColumn: "Id",
                keyValue: 5,
                column: "Name",
                value: "Leonardo");

            migrationBuilder.UpdateData(
                table: "Operators",
                keyColumn: "Id",
                keyValue: 6,
                column: "Name",
                value: "Travelodge");

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 1,
                column: "Price",
                value: 9000);

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 2,
                column: "Price",
                value: 10000);

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 3,
                column: "Price",
                value: 15500);

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
                keyValue: 3,
                columns: new[] { "Length", "Name", "Price" },
                values: new object[] { 12, "Best of Britain", 290000 });
        }
    }
}
