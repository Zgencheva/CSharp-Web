#nullable disable

namespace VisitACity.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class RemovePlansFromCountries : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Plans_Countries_CountryId",
                table: "Plans");

            migrationBuilder.DropIndex(
                name: "IX_Plans_CountryId",
                table: "Plans");

            migrationBuilder.DropColumn(
                name: "CountryId",
                table: "Plans");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CountryId",
                table: "Plans",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Plans_CountryId",
                table: "Plans",
                column: "CountryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Plans_Countries_CountryId",
                table: "Plans",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id");
        }
    }
}
