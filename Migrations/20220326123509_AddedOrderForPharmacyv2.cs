using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyPharmacy.Migrations
{
    public partial class AddedOrderForPharmacyv2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrderForPharmacyId",
                table: "Drugs",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "OrderForPharmacies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateOfOrder = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateOfReceipt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    StatusId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    PharmacyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderForPharmacies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderForPharmacies_Pharmacies_PharmacyId",
                        column: x => x.PharmacyId,
                        principalTable: "Pharmacies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderForPharmacies_Statuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Statuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderForPharmacies_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Drugs_OrderForPharmacyId",
                table: "Drugs",
                column: "OrderForPharmacyId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderForPharmacies_PharmacyId",
                table: "OrderForPharmacies",
                column: "PharmacyId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderForPharmacies_StatusId",
                table: "OrderForPharmacies",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderForPharmacies_UserId",
                table: "OrderForPharmacies",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Drugs_OrderForPharmacies_OrderForPharmacyId",
                table: "Drugs",
                column: "OrderForPharmacyId",
                principalTable: "OrderForPharmacies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Drugs_OrderForPharmacies_OrderForPharmacyId",
                table: "Drugs");

            migrationBuilder.DropTable(
                name: "OrderForPharmacies");

            migrationBuilder.DropIndex(
                name: "IX_Drugs_OrderForPharmacyId",
                table: "Drugs");

            migrationBuilder.DropColumn(
                name: "OrderForPharmacyId",
                table: "Drugs");
        }
    }
}
