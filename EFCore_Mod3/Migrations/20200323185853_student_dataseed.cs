using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EFCore_Mod3.Migrations
{
    public partial class student_dataseed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "DateBirth", "ItsErased", "Name" },
                values: new object[] { 25, new DateTime(1999, 2, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "JuanCarlos" });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "DateBirth", "ItsErased", "Name" },
                values: new object[] { 26, new DateTime(1991, 2, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "RamoSebastian" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 26);
        }
    }
}
