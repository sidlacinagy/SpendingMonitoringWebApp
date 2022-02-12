using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication3.Migrations
{
    public partial class addsalt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "password",
                table: "User",
                newName: "salt");

            migrationBuilder.AddColumn<string>(
                name: "passwordHash",
                table: "User",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "passwordHash",
                table: "User");

            migrationBuilder.RenameColumn(
                name: "salt",
                table: "User",
                newName: "password");
        }
    }
}
