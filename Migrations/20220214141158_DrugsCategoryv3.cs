using Microsoft.EntityFrameworkCore.Migrations;

namespace MyPharmacy.Migrations
{
    public partial class DrugsCategoryv3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DrugCategoryDrugInformation_DrugCategory_DrugCategoriesId",
                table: "DrugCategoryDrugInformation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DrugCategory",
                table: "DrugCategory");

            migrationBuilder.RenameTable(
                name: "DrugCategory",
                newName: "DrugCategories");

            migrationBuilder.AlterColumn<string>(
                name: "CategoryName",
                table: "DrugCategories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_DrugCategories",
                table: "DrugCategories",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DrugCategoryDrugInformation_DrugCategories_DrugCategoriesId",
                table: "DrugCategoryDrugInformation",
                column: "DrugCategoriesId",
                principalTable: "DrugCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DrugCategoryDrugInformation_DrugCategories_DrugCategoriesId",
                table: "DrugCategoryDrugInformation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DrugCategories",
                table: "DrugCategories");

            migrationBuilder.RenameTable(
                name: "DrugCategories",
                newName: "DrugCategory");

            migrationBuilder.AlterColumn<string>(
                name: "CategoryName",
                table: "DrugCategory",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DrugCategory",
                table: "DrugCategory",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DrugCategoryDrugInformation_DrugCategory_DrugCategoriesId",
                table: "DrugCategoryDrugInformation",
                column: "DrugCategoriesId",
                principalTable: "DrugCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
