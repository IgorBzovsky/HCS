using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace HCS.Data.Migrations
{
    public partial class AddTarifftoConsumedUtility : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TariffId",
                table: "ConsumedUtilities",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ConsumedUtilities_TariffId",
                table: "ConsumedUtilities",
                column: "TariffId");

            migrationBuilder.AddForeignKey(
                name: "FK_ConsumedUtilities_Tariffs_TariffId",
                table: "ConsumedUtilities",
                column: "TariffId",
                principalTable: "Tariffs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConsumedUtilities_Tariffs_TariffId",
                table: "ConsumedUtilities");

            migrationBuilder.DropIndex(
                name: "IX_ConsumedUtilities_TariffId",
                table: "ConsumedUtilities");

            migrationBuilder.DropColumn(
                name: "TariffId",
                table: "ConsumedUtilities");
        }
    }
}
