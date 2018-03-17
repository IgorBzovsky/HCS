using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace HCS.Data.Migrations
{
    public partial class AddforeignkeyConsumedUtilityIdtoConsumptionNorm : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ConsumptionNorms_ConsumedUtilityId",
                table: "ConsumptionNorms",
                column: "ConsumedUtilityId");

            migrationBuilder.AddForeignKey(
                name: "FK_ConsumptionNorms_ConsumedUtilities_ConsumedUtilityId",
                table: "ConsumptionNorms",
                column: "ConsumedUtilityId",
                principalTable: "ConsumedUtilities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConsumptionNorms_ConsumedUtilities_ConsumedUtilityId",
                table: "ConsumptionNorms");

            migrationBuilder.DropIndex(
                name: "IX_ConsumptionNorms_ConsumedUtilityId",
                table: "ConsumptionNorms");
        }
    }
}
