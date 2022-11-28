using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VisitACity.Data.Migrations
{
    public partial class RemoveMAppingTableBetweenPlansAndAttractions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AttractionsPlans");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AttractionsPlans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AttractionId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    PlanId = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttractionsPlans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AttractionsPlans_Attractions_AttractionId",
                        column: x => x.AttractionId,
                        principalTable: "Attractions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AttractionsPlans_Plans_PlanId",
                        column: x => x.PlanId,
                        principalTable: "Plans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AttractionsPlans_AttractionId",
                table: "AttractionsPlans",
                column: "AttractionId");

            migrationBuilder.CreateIndex(
                name: "IX_AttractionsPlans_IsDeleted",
                table: "AttractionsPlans",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_AttractionsPlans_PlanId",
                table: "AttractionsPlans",
                column: "PlanId");
        }
    }
}
