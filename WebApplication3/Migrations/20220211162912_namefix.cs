using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication3.Migrations
{
    public partial class namefix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountVerificationToken_User_Useremail",
                table: "AccountVerificationToken");

            migrationBuilder.RenameColumn(
                name: "Useremail",
                table: "AccountVerificationToken",
                newName: "useremail");

            migrationBuilder.RenameIndex(
                name: "IX_AccountVerificationToken_Useremail",
                table: "AccountVerificationToken",
                newName: "IX_AccountVerificationToken_useremail");

            migrationBuilder.AddForeignKey(
                name: "FK_AccountVerificationToken_User_useremail",
                table: "AccountVerificationToken",
                column: "useremail",
                principalTable: "User",
                principalColumn: "email",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountVerificationToken_User_useremail",
                table: "AccountVerificationToken");

            migrationBuilder.RenameColumn(
                name: "useremail",
                table: "AccountVerificationToken",
                newName: "Useremail");

            migrationBuilder.RenameIndex(
                name: "IX_AccountVerificationToken_useremail",
                table: "AccountVerificationToken",
                newName: "IX_AccountVerificationToken_Useremail");

            migrationBuilder.AddForeignKey(
                name: "FK_AccountVerificationToken_User_Useremail",
                table: "AccountVerificationToken",
                column: "Useremail",
                principalTable: "User",
                principalColumn: "email",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
