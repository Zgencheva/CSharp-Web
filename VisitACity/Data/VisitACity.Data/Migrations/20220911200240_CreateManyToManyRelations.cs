using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VisitACity.Data.Migrations
{
    public partial class CreateManyToManyRelations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attractions_Plans_PlanId",
                table: "Attractions");

            migrationBuilder.DropForeignKey(
                name: "FK_Cities_Countries_CountryId",
                table: "Cities");

            migrationBuilder.DropForeignKey(
                name: "FK_Cities_Plans_PlanId",
                table: "Cities");

            migrationBuilder.DropForeignKey(
                name: "FK_Plans_Countries_CountryId",
                table: "Plans");

            migrationBuilder.DropForeignKey(
                name: "FK_Restaurants_Plans_PlanId",
                table: "Restaurants");

            migrationBuilder.DropIndex(
                name: "IX_Restaurants_PlanId",
                table: "Restaurants");

            migrationBuilder.DropIndex(
                name: "IX_Cities_PlanId",
                table: "Cities");

            migrationBuilder.DropIndex(
                name: "IX_Attractions_PlanId",
                table: "Attractions");

            migrationBuilder.DropColumn(
                name: "PlanId",
                table: "Restaurants");

            migrationBuilder.DropColumn(
                name: "PlanId",
                table: "Cities");

            migrationBuilder.DropColumn(
                name: "PlanId",
                table: "Attractions");

            migrationBuilder.AlterColumn<int>(
                name: "CountryId",
                table: "Plans",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "CityId",
                table: "Plans",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "CountryId",
                table: "Cities",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "AttractionPlan",
                columns: table => new
                {
                    AttractionsId = table.Column<int>(type: "int", nullable: false),
                    PlansId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttractionPlan", x => new { x.AttractionsId, x.PlansId });
                    table.ForeignKey(
                        name: "FK_AttractionPlan_Attractions_AttractionsId",
                        column: x => x.AttractionsId,
                        principalTable: "Attractions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AttractionPlan_Plans_PlansId",
                        column: x => x.PlansId,
                        principalTable: "Plans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PlanRestaurant",
                columns: table => new
                {
                    PlansId = table.Column<int>(type: "int", nullable: false),
                    RestaurantsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanRestaurant", x => new { x.PlansId, x.RestaurantsId });
                    table.ForeignKey(
                        name: "FK_PlanRestaurant_Plans_PlansId",
                        column: x => x.PlansId,
                        principalTable: "Plans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PlanRestaurant_Restaurants_RestaurantsId",
                        column: x => x.RestaurantsId,
                        principalTable: "Restaurants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Plans_CityId",
                table: "Plans",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_AttractionPlan_PlansId",
                table: "AttractionPlan",
                column: "PlansId");

            migrationBuilder.CreateIndex(
                name: "IX_PlanRestaurant_RestaurantsId",
                table: "PlanRestaurant",
                column: "RestaurantsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cities_Countries_CountryId",
                table: "Cities",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Plans_Cities_CityId",
                table: "Plans",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Plans_Countries_CountryId",
                table: "Plans",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cities_Countries_CountryId",
                table: "Cities");

            migrationBuilder.DropForeignKey(
                name: "FK_Plans_Cities_CityId",
                table: "Plans");

            migrationBuilder.DropForeignKey(
                name: "FK_Plans_Countries_CountryId",
                table: "Plans");

            migrationBuilder.DropTable(
                name: "AttractionPlan");

            migrationBuilder.DropTable(
                name: "PlanRestaurant");

            migrationBuilder.DropIndex(
                name: "IX_Plans_CityId",
                table: "Plans");

            migrationBuilder.DropColumn(
                name: "CityId",
                table: "Plans");

            migrationBuilder.AddColumn<int>(
                name: "PlanId",
                table: "Restaurants",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CountryId",
                table: "Plans",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CountryId",
                table: "Cities",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "PlanId",
                table: "Cities",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PlanId",
                table: "Attractions",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Restaurants_PlanId",
                table: "Restaurants",
                column: "PlanId");

            migrationBuilder.CreateIndex(
                name: "IX_Cities_PlanId",
                table: "Cities",
                column: "PlanId");

            migrationBuilder.CreateIndex(
                name: "IX_Attractions_PlanId",
                table: "Attractions",
                column: "PlanId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attractions_Plans_PlanId",
                table: "Attractions",
                column: "PlanId",
                principalTable: "Plans",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Cities_Countries_CountryId",
                table: "Cities",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Cities_Plans_PlanId",
                table: "Cities",
                column: "PlanId",
                principalTable: "Plans",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Plans_Countries_CountryId",
                table: "Plans",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Restaurants_Plans_PlanId",
                table: "Restaurants",
                column: "PlanId",
                principalTable: "Plans",
                principalColumn: "Id");
        }
    }
}
