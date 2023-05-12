using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarParts.Migrations
{
    public partial class AddNameForStore : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Stores",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsSeller",
                table: "Clients",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "95821970-bd2d-4257-ab34-95f82f5c91c7");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "798a57eb-c1cd-4c1c-93f5-4aef2c49eb19");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "216e17c1-6ea4-4fe4-b376-f084aa12578f");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "c9ed9b7c-6cef-46da-b583-8ec188dd2175", "AQAAAAEAACcQAAAAEEXFEzoaAawVYPGsBRvA021kp2S0RFJk73V5oqreNq/SuB1kKIoJZs0HV7q3rpmi1Q==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Stores");

            migrationBuilder.DropColumn(
                name: "IsSeller",
                table: "Clients");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "25acceff-31e0-442b-b66d-0b94016ac222");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "d855c93e-0bdc-4fe4-84bc-d019d712e0c6");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "90c69fe2-7e84-4827-bae2-2e7ab933869d");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "d7be0506-103b-454c-bd2f-fda33da1b48b", "AQAAAAEAACcQAAAAEFl33ogJhqHOIFtShk6Wf2AR1g/m46LnFSCnZA0tI0/ZbLTGe7hab/suF6PS2eyd9g==" });
        }
    }
}
