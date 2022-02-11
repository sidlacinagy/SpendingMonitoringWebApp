using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication3.Migrations
{
    public partial class relations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubUser_User_UserEmail",
                table: "SubUser");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "PwRecoveryToken");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "AccountVerificationToken");

            migrationBuilder.RenameColumn(
                name: "Verified",
                table: "User",
                newName: "verified");

            migrationBuilder.RenameColumn(
                name: "Password",
                table: "User",
                newName: "password");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "User",
                newName: "email");

            migrationBuilder.RenameColumn(
                name: "UserEmail",
                table: "SubUser",
                newName: "Useremail");

            migrationBuilder.RenameColumn(
                name: "SubUserName",
                table: "SubUser",
                newName: "subUserName");

            migrationBuilder.RenameColumn(
                name: "SubuserId",
                table: "SubUser",
                newName: "subuserId");

            migrationBuilder.RenameIndex(
                name: "IX_SubUser_UserEmail",
                table: "SubUser",
                newName: "IX_SubUser_Useremail");

            migrationBuilder.RenameColumn(
                name: "SubuserId",
                table: "Spending",
                newName: "subuserId");

            migrationBuilder.RenameColumn(
                name: "ProductCategory",
                table: "Spending",
                newName: "productCategory");

            migrationBuilder.RenameColumn(
                name: "Product",
                table: "Spending",
                newName: "product");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Spending",
                newName: "price");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "Spending",
                newName: "date");

            migrationBuilder.RenameColumn(
                name: "ExpirationDate",
                table: "PwRecoveryToken",
                newName: "expirationDate");

            migrationBuilder.RenameColumn(
                name: "Token",
                table: "PwRecoveryToken",
                newName: "token");

            migrationBuilder.RenameColumn(
                name: "ExpirationDate",
                table: "AccountVerificationToken",
                newName: "expirationDate");

            migrationBuilder.RenameColumn(
                name: "Token",
                table: "AccountVerificationToken",
                newName: "token");

            migrationBuilder.AddColumn<string>(
                name: "useremail",
                table: "PwRecoveryToken",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Useremail",
                table: "AccountVerificationToken",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Spending_subuserId",
                table: "Spending",
                column: "subuserId");

            migrationBuilder.CreateIndex(
                name: "IX_PwRecoveryToken_useremail",
                table: "PwRecoveryToken",
                column: "useremail");

            migrationBuilder.CreateIndex(
                name: "IX_AccountVerificationToken_Useremail",
                table: "AccountVerificationToken",
                column: "Useremail");

            migrationBuilder.AddForeignKey(
                name: "FK_AccountVerificationToken_User_Useremail",
                table: "AccountVerificationToken",
                column: "Useremail",
                principalTable: "User",
                principalColumn: "email",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PwRecoveryToken_User_useremail",
                table: "PwRecoveryToken",
                column: "useremail",
                principalTable: "User",
                principalColumn: "email",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Spending_SubUser_subuserId",
                table: "Spending",
                column: "subuserId",
                principalTable: "SubUser",
                principalColumn: "subuserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SubUser_User_Useremail",
                table: "SubUser",
                column: "Useremail",
                principalTable: "User",
                principalColumn: "email",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountVerificationToken_User_Useremail",
                table: "AccountVerificationToken");

            migrationBuilder.DropForeignKey(
                name: "FK_PwRecoveryToken_User_useremail",
                table: "PwRecoveryToken");

            migrationBuilder.DropForeignKey(
                name: "FK_Spending_SubUser_subuserId",
                table: "Spending");

            migrationBuilder.DropForeignKey(
                name: "FK_SubUser_User_Useremail",
                table: "SubUser");

            migrationBuilder.DropIndex(
                name: "IX_Spending_subuserId",
                table: "Spending");

            migrationBuilder.DropIndex(
                name: "IX_PwRecoveryToken_useremail",
                table: "PwRecoveryToken");

            migrationBuilder.DropIndex(
                name: "IX_AccountVerificationToken_Useremail",
                table: "AccountVerificationToken");

            migrationBuilder.DropColumn(
                name: "useremail",
                table: "PwRecoveryToken");

            migrationBuilder.DropColumn(
                name: "Useremail",
                table: "AccountVerificationToken");

            migrationBuilder.RenameColumn(
                name: "verified",
                table: "User",
                newName: "Verified");

            migrationBuilder.RenameColumn(
                name: "password",
                table: "User",
                newName: "Password");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "User",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "subUserName",
                table: "SubUser",
                newName: "SubUserName");

            migrationBuilder.RenameColumn(
                name: "Useremail",
                table: "SubUser",
                newName: "UserEmail");

            migrationBuilder.RenameColumn(
                name: "subuserId",
                table: "SubUser",
                newName: "SubuserId");

            migrationBuilder.RenameIndex(
                name: "IX_SubUser_Useremail",
                table: "SubUser",
                newName: "IX_SubUser_UserEmail");

            migrationBuilder.RenameColumn(
                name: "subuserId",
                table: "Spending",
                newName: "SubuserId");

            migrationBuilder.RenameColumn(
                name: "productCategory",
                table: "Spending",
                newName: "ProductCategory");

            migrationBuilder.RenameColumn(
                name: "product",
                table: "Spending",
                newName: "Product");

            migrationBuilder.RenameColumn(
                name: "price",
                table: "Spending",
                newName: "Price");

            migrationBuilder.RenameColumn(
                name: "date",
                table: "Spending",
                newName: "Date");

            migrationBuilder.RenameColumn(
                name: "expirationDate",
                table: "PwRecoveryToken",
                newName: "ExpirationDate");

            migrationBuilder.RenameColumn(
                name: "token",
                table: "PwRecoveryToken",
                newName: "Token");

            migrationBuilder.RenameColumn(
                name: "expirationDate",
                table: "AccountVerificationToken",
                newName: "ExpirationDate");

            migrationBuilder.RenameColumn(
                name: "token",
                table: "AccountVerificationToken",
                newName: "Token");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "PwRecoveryToken",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "AccountVerificationToken",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_SubUser_User_UserEmail",
                table: "SubUser",
                column: "UserEmail",
                principalTable: "User",
                principalColumn: "Email",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
