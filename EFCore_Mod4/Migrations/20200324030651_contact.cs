using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EFCore_Mod4.Migrations
{
    public partial class contact : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ItsErased",
                table: "Students");

            migrationBuilder.CreateTable(
                name: "Contacts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<int>(nullable: false),
                    Relation = table.Column<int>(nullable: false),
                    StudentId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contacts_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "DateBirth", "Name" },
                values: new object[] { 1, new DateTime(1999, 2, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Juan Carlos" });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "DateBirth", "Name" },
                values: new object[] { 2, new DateTime(1999, 2, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Pepe Juan" });

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_StudentId",
                table: "Contacts",
                column: "StudentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contacts");

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.AddColumn<bool>(
                name: "ItsErased",
                table: "Students",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
