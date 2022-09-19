#nullable disable

namespace VisitACity.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class Images : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Days",
                table: "Plans");

            migrationBuilder.AddColumn<string>(
                name: "AttractionUrl",
                table: "Attractions",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AttractionUrl",
                table: "Attractions");

            migrationBuilder.AddColumn<int>(
                name: "Days",
                table: "Plans",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
