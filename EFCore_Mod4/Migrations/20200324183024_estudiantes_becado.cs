using Microsoft.EntityFrameworkCore.Migrations;

namespace EFCore_Mod4.Migrations
{
    public partial class estudiantes_becado : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Students",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "InstitutionThatAwardsTheScholarship",
                table: "Students",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 1,
                column: "Discriminator",
                value: "Student");

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 2,
                column: "Discriminator",
                value: "Student");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "InstitutionThatAwardsTheScholarship",
                table: "Students");
        }
    }
}
