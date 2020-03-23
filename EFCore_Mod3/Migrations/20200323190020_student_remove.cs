using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EFCore_Mod3.Migrations
{
    public partial class student_remove : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 26);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "DateBirth", "ItsErased", "Name" },
                values: new object[] { 26, new DateTime(1991, 2, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "RamoSebastian" });
        }
    }
}
