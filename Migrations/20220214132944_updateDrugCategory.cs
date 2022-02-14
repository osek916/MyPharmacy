using Microsoft.EntityFrameworkCore.Migrations;

namespace MyPharmacy.Migrations
{
    public partial class updateDrugCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "DrugCategory",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "DrugCategory");
        }
    }
}
