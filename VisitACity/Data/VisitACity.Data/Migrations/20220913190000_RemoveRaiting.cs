#nullable disable

namespace VisitACity.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class RemoveRaiting : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Raiting",
                table: "Restaurants");

            migrationBuilder.DropColumn(
                name: "Raiting",
                table: "Attractions");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Raiting",
                table: "Restaurants",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Raiting",
                table: "Attractions",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
