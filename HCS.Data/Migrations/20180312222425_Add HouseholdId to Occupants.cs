using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace HCS.Data.Migrations
{
    public partial class AddHouseholdIdtoOccupants : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HouseholdId",
                table: "Occupants",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Occupants_HouseholdId",
                table: "Occupants",
                column: "HouseholdId");

            migrationBuilder.AddForeignKey(
                name: "FK_Occupants_Consumers_HouseholdId",
                table: "Occupants",
                column: "HouseholdId",
                principalTable: "Consumers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Occupants_Consumers_HouseholdId",
                table: "Occupants");

            migrationBuilder.DropIndex(
                name: "IX_Occupants_HouseholdId",
                table: "Occupants");

            migrationBuilder.DropColumn(
                name: "HouseholdId",
                table: "Occupants");
        }
    }
}
