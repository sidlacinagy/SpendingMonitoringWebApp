using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication3.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TokenModel",
                columns: table => new
                {
                    RefreshToken = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    JWTToken = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RefreshTokenExpiryTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsUsed = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TokenModel", x => x.RefreshToken);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    salt = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    passwordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    verified = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.email);
                });

            migrationBuilder.CreateTable(
                name: "AccountVerificationToken",
                columns: table => new
                {
                    token = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    useremail = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    expirationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountVerificationToken", x => x.token);
                    table.ForeignKey(
                        name: "FK_AccountVerificationToken_User_useremail",
                        column: x => x.useremail,
                        principalTable: "User",
                        principalColumn: "email",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PwRecoveryToken",
                columns: table => new
                {
                    token = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    useremail = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    expirationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PwRecoveryToken", x => x.token);
                    table.ForeignKey(
                        name: "FK_PwRecoveryToken_User_useremail",
                        column: x => x.useremail,
                        principalTable: "User",
                        principalColumn: "email",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubUser",
                columns: table => new
                {
                    subUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    subUserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Useremail = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubUser", x => x.subUserId);
                    table.ForeignKey(
                        name: "FK_SubUser_User_Useremail",
                        column: x => x.Useremail,
                        principalTable: "User",
                        principalColumn: "email",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Spending",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    subUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    product = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    productCategory = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    price = table.Column<int>(type: "int", nullable: false),
                    date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Spending", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Spending_SubUser_subUserId",
                        column: x => x.subUserId,
                        principalTable: "SubUser",
                        principalColumn: "subUserId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccountVerificationToken_useremail",
                table: "AccountVerificationToken",
                column: "useremail");

            migrationBuilder.CreateIndex(
                name: "IX_PwRecoveryToken_useremail",
                table: "PwRecoveryToken",
                column: "useremail");

            migrationBuilder.CreateIndex(
                name: "IX_Spending_subUserId",
                table: "Spending",
                column: "subUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SubUser_Useremail",
                table: "SubUser",
                column: "Useremail");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountVerificationToken");

            migrationBuilder.DropTable(
                name: "PwRecoveryToken");

            migrationBuilder.DropTable(
                name: "Spending");

            migrationBuilder.DropTable(
                name: "TokenModel");

            migrationBuilder.DropTable(
                name: "SubUser");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
