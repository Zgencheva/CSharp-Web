using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VisitACity.Data.Migrations
{
    public partial class removeSomeMappings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CityPlan");

            migrationBuilder.AddColumn<int>(
                name: "PlanId",
                table: "Cities",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cities_PlanId",
                table: "Cities",
                column: "PlanId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cities_Plans_PlanId",
                table: "Cities",
                column: "PlanId",
                principalTable: "Plans",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cities_Plans_PlanId",
                table: "Cities");

            migrationBuilder.DropIndex(
                name: "IX_Cities_PlanId",
                table: "Cities");

            migrationBuilder.DropColumn(
                name: "PlanId",
                table: "Cities");

            migrationBuilder.CreateTable(
                name: "CityPlan",
                columns: table => new
                {
                    CitiesId = table.Column<int>(type: "int", nullable: false),
                    PlansId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CityPlan", x => new { x.CitiesId, x.PlansId });
                    table.ForeignKey(
                        name: "FK_CityPlan_Cities_CitiesId",
                        column: x => x.CitiesId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CityPlan_Plans_PlansId",
                        column: x => x.PlansId,
                        principalTable: "Plans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CityPlan_PlansId",
                table: "CityPlan",
                column: "PlansId");
        }
    }
}
