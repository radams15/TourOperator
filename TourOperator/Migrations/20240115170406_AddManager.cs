using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TourOperator.Migrations
{
    /// <inheritdoc />
    public partial class AddManager : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "FullName", "RoleId" },
                values: new object[] { "Rhys Adams", 2 });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "FullName", "PassportNo", "Password", "PhoneNo", "RoleId", "Username" },
                values: new object[] { 2, "Manager", "123123123", "d74ff0ee8da3b9806b18c877dbf29bbde50b5bd8e4dad7a3a725000feb82e8f1", "123123123", 1, "manager" });

            migrationBuilder.UpdateData(
                table: "Hotels",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Kings Hotel Brighton");

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
                value: "London Marriott Hotel");

            migrationBuilder.UpdateData(
                table: "Hotels",
                keyColumn: "Id",
                keyValue: 4,
                column: "Name",
                value: "Nevis Bank Inn, Fort William");

            migrationBuilder.UpdateData(
                table: "Hotels",
                keyColumn: "Id",
                keyValue: 5,
                column: "Name",
                value: "Travelodge Brighton Seafront");

            migrationBuilder.UpdateData(
                table: "Operators",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Kings");

            migrationBuilder.UpdateData(
                table: "Operators",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Hilton");

            migrationBuilder.UpdateData(
                table: "Operators",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Marriott");

            migrationBuilder.UpdateData(
                table: "Operators",
                keyColumn: "Id",
                keyValue: 4,
                column: "Name",
                value: "Independent");

            migrationBuilder.UpdateData(
                table: "Operators",
                keyColumn: "Id",
                keyValue: 5,
                column: "Name",
                value: "Travelodge");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "MANAGER");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "CUSTOMER");

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 1,
                column: "Price",
                value: 18000);

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 2,
                column: "Price",
                value: 40000);

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 3,
                column: "Price",
                value: 52000);

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
                value: 30000);

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 8,
                column: "Price",
                value: 50000);

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 9,
                column: "Price",
                value: 90000);

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 10,
                column: "Price",
                value: 9000);

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 11,
                column: "Price",
                value: 10000);

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 12,
                column: "Price",
                value: 15500);

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "FullName", "RoleId" },
                values: new object[] { "", 1 });

            migrationBuilder.UpdateData(
                table: "Hotels",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Hilton London Hotel");

            migrationBuilder.UpdateData(
                table: "Hotels",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "London Marriott Hotel");

            migrationBuilder.UpdateData(
                table: "Hotels",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Travelodge Brighton Seafront");

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
                value: "Nevis Bank Inn, Fort William");

            migrationBuilder.UpdateData(
                table: "Operators",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Hilton");

            migrationBuilder.UpdateData(
                table: "Operators",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Marriott");

            migrationBuilder.UpdateData(
                table: "Operators",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Travelodge");

            migrationBuilder.UpdateData(
                table: "Operators",
                keyColumn: "Id",
                keyValue: 4,
                column: "Name",
                value: "Kings");

            migrationBuilder.UpdateData(
                table: "Operators",
                keyColumn: "Id",
                keyValue: 5,
                column: "Name",
                value: "Independent");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "CUSTOMER");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "MANAGER");

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 1,
                column: "Price",
                value: 37500);

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 2,
                column: "Price",
                value: 77500);

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 3,
                column: "Price",
                value: 95000);

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
                keyValue: 7,
                column: "Price",
                value: 8000);

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 8,
                column: "Price",
                value: 12000);

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 9,
                column: "Price",
                value: 15000);

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
        }
    }
}
