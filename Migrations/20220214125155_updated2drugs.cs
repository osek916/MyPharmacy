using Microsoft.EntityFrameworkCore.Migrations;

namespace MyPharmacy.Migrations
{
    public partial class updated2drugs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DrugCategory",
                table: "DrugInformations");

            migrationBuilder.CreateTable(
                name: "DrugCategory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DrugCategory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DrugCategoryDrugInformation",
                columns: table => new
                {
                    DrugCategoriesId = table.Column<int>(type: "int", nullable: false),
                    DrugInformationsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DrugCategoryDrugInformation", x => new { x.DrugCategoriesId, x.DrugInformationsId });
                    table.ForeignKey(
                        name: "FK_DrugCategoryDrugInformation_DrugCategory_DrugCategoriesId",
                        column: x => x.DrugCategoriesId,
                        principalTable: "DrugCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DrugCategoryDrugInformation_DrugInformations_DrugInformationsId",
                        column: x => x.DrugInformationsId,
                        principalTable: "DrugInformations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DrugCategoryDrugInformation_DrugInformationsId",
                table: "DrugCategoryDrugInformation",
                column: "DrugInformationsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DrugCategoryDrugInformation");

            migrationBuilder.DropTable(
                name: "DrugCategory");

            migrationBuilder.AddColumn<string>(
                name: "DrugCategory",
                table: "DrugInformations",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
