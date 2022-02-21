﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace MyPharmacy.Migrations
{
    public partial class AddPharmacyToUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Users_PharmacyId",
                table: "Users",
                column: "PharmacyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Pharmacies_PharmacyId",
                table: "Users",
                column: "PharmacyId",
                principalTable: "Pharmacies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Pharmacies_PharmacyId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_PharmacyId",
                table: "Users");
        }
    }
}
