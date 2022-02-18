using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication3.Migrations
{
    public partial class subuser3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Spending_SubUser_subUserName",
                table: "Spending");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SubUser",
                table: "SubUser");

            migrationBuilder.DropIndex(
                name: "IX_Spending_subUserName",
                table: "Spending");

            migrationBuilder.DropColumn(
                name: "subUserName",
                table: "Spending");

            migrationBuilder.AlterColumn<string>(
                name: "subUserName",
                table: "SubUser",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)")
                .OldAnnotation("Relational:ColumnOrder", 1);

            migrationBuilder.AddColumn<int>(
                name: "subuserId",
                table: "SubUser",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "subuserId",
                table: "Spending",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_SubUser",
                table: "SubUser",
                column: "subuserId");

            migrationBuilder.CreateIndex(
                name: "IX_Spending_subuserId",
                table: "Spending",
                column: "subuserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Spending_SubUser_subuserId",
                table: "Spending",
                column: "subuserId",
                principalTable: "SubUser",
                principalColumn: "subuserId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Spending_SubUser_subuserId",
                table: "Spending");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SubUser",
                table: "SubUser");

            migrationBuilder.DropIndex(
                name: "IX_Spending_subuserId",
                table: "Spending");

            migrationBuilder.DropColumn(
                name: "subuserId",
                table: "SubUser");

            migrationBuilder.DropColumn(
                name: "subuserId",
                table: "Spending");

            migrationBuilder.AlterColumn<string>(
                name: "subUserName",
                table: "SubUser",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)")
                .Annotation("Relational:ColumnOrder", 1);

            migrationBuilder.AddColumn<string>(
                name: "subUserName",
                table: "Spending",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SubUser",
                table: "SubUser",
                column: "subUserName");

            migrationBuilder.CreateIndex(
                name: "IX_Spending_subUserName",
                table: "Spending",
                column: "subUserName");

            migrationBuilder.AddForeignKey(
                name: "FK_Spending_SubUser_subUserName",
                table: "Spending",
                column: "subUserName",
                principalTable: "SubUser",
                principalColumn: "subUserName",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
