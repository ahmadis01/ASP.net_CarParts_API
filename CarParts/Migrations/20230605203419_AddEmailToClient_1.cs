using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarParts.Migrations
{
    public partial class AddEmailToClient_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Clients",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Clients",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "ba26dec0-4b9a-4f66-828d-8ec4e62502eb");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "cc00301e-aedc-4816-a29d-83da181cab46");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "3dcdee5e-85bc-477f-a764-44ff9215bf6c");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "dd57be36-6da1-4079-9f62-91352efe3d9c", "AQAAAAEAACcQAAAAEBdd2siVOJWCWnFM65qJYXEBMybAKPxeWEFioY1sZZ7pvHPwk+ocHW0ARDT3Msqgwg==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Clients");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "6275b3ad-4bd4-4583-a87a-d2b08b8b83cb");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "3da14fe2-5f2b-4ade-9c11-604cf339544b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "d4e75409-4f96-4d06-ae94-dace344236ec");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "38fc04eb-21e1-4a0b-a3c4-985974e5f6c8", "AQAAAAEAACcQAAAAEFFwmaCkxNdTvFerbQNxAS5MxUd3uzQvNfLlZckKEjBJxRY02jwG27tYgBRdeFvixw==" });
        }
    }
}
