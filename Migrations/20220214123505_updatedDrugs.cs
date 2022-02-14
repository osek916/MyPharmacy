using Microsoft.EntityFrameworkCore.Migrations;

namespace MyPharmacy.Migrations
{
    public partial class updatedDrugs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DrugInformations_Drugs_DrugId",
                table: "DrugInformations");

            migrationBuilder.DropIndex(
                name: "IX_DrugInformations_DrugId",
                table: "DrugInformations");

            migrationBuilder.DropColumn(
                name: "DrugId",
                table: "DrugInformations");

            migrationBuilder.AddColumn<int>(
                name: "DrugInformationId",
                table: "Drugs",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Drugs_DrugInformationId",
                table: "Drugs",
                column: "DrugInformationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Drugs_DrugInformations_DrugInformationId",
                table: "Drugs",
                column: "DrugInformationId",
                principalTable: "DrugInformations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Drugs_DrugInformations_DrugInformationId",
                table: "Drugs");

            migrationBuilder.DropIndex(
                name: "IX_Drugs_DrugInformationId",
                table: "Drugs");

            migrationBuilder.DropColumn(
                name: "DrugInformationId",
                table: "Drugs");

            migrationBuilder.AddColumn<int>(
                name: "DrugId",
                table: "DrugInformations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_DrugInformations_DrugId",
                table: "DrugInformations",
                column: "DrugId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_DrugInformations_Drugs_DrugId",
                table: "DrugInformations",
                column: "DrugId",
                principalTable: "Drugs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
