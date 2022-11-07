using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VisitACity.Data.Migrations
{
    public partial class RemoveCommentsFromAttractions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Attractions_AttractionId",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_AttractionId",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "AttractionId",
                table: "Reviews");

            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                table: "Restaurants",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "ApplicationUserAttraction",
                columns: table => new
                {
                    AttractionsReviewedId = table.Column<int>(type: "int", nullable: false),
                    UsersReviewsId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserAttraction", x => new { x.AttractionsReviewedId, x.UsersReviewsId });
                    table.ForeignKey(
                        name: "FK_ApplicationUserAttraction_AspNetUsers_UsersReviewsId",
                        column: x => x.UsersReviewsId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ApplicationUserAttraction_Attractions_AttractionsReviewedId",
                        column: x => x.AttractionsReviewedId,
                        principalTable: "Attractions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserAttraction_UsersReviewsId",
                table: "ApplicationUserAttraction",
                column: "UsersReviewsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationUserAttraction");

            migrationBuilder.AddColumn<int>(
                name: "AttractionId",
                table: "Reviews",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                table: "Restaurants",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_AttractionId",
                table: "Reviews",
                column: "AttractionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Attractions_AttractionId",
                table: "Reviews",
                column: "AttractionId",
                principalTable: "Attractions",
                principalColumn: "Id");
        }
    }
}
