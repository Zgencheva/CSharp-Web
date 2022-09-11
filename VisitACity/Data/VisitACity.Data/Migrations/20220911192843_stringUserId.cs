using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VisitACity.Data.Migrations
{
    public partial class stringUserId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Plans_AspNetUsers_UserId1",
                table: "Plans");

            migrationBuilder.DropIndex(
                name: "IX_Plans_UserId1",
                table: "Plans");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Plans");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Plans",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Plans_UserId",
                table: "Plans",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Plans_AspNetUsers_UserId",
                table: "Plans",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Plans_AspNetUsers_UserId",
                table: "Plans");

            migrationBuilder.DropIndex(
                name: "IX_Plans_UserId",
                table: "Plans");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Plans",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "Plans",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Plans_UserId1",
                table: "Plans",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Plans_AspNetUsers_UserId1",
                table: "Plans",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
