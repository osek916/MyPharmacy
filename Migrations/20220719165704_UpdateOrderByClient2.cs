using Microsoft.EntityFrameworkCore.Migrations;

namespace MyPharmacy.Migrations
{
    public partial class UpdateOrderByClient2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PharmacyId",
                table: "OrderByClients",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderByClients_PharmacyId",
                table: "OrderByClients",
                column: "PharmacyId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderByClients_Pharmacies_PharmacyId",
                table: "OrderByClients",
                column: "PharmacyId",
                principalTable: "Pharmacies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderByClients_Pharmacies_PharmacyId",
                table: "OrderByClients");

            migrationBuilder.DropIndex(
                name: "IX_OrderByClients_PharmacyId",
                table: "OrderByClients");

            migrationBuilder.DropColumn(
                name: "PharmacyId",
                table: "OrderByClients");
        }
    }
}
