using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace HCS.Data.Migrations
{
    public partial class RenameExemption : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConsumptionNorm_ConsumedUtilities_ConsumedUtilityId",
                table: "ConsumptionNorm");

            migrationBuilder.DropForeignKey(
                name: "FK_ConsumptionNorm_Occupants_OccupantId",
                table: "ConsumptionNorm");

            migrationBuilder.DropForeignKey(
                name: "FK_Occupants_Exemption_ExemptionId",
                table: "Occupants");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Exemption",
                table: "Exemption");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ConsumptionNorm",
                table: "ConsumptionNorm");

            migrationBuilder.DropIndex(
                name: "IX_ConsumptionNorm_ConsumedUtilityId",
                table: "ConsumptionNorm");

            migrationBuilder.RenameTable(
                name: "Exemption",
                newName: "Exemptions");

            migrationBuilder.RenameTable(
                name: "ConsumptionNorm",
                newName: "ConsumptionNorms");

            migrationBuilder.RenameIndex(
                name: "IX_ConsumptionNorm_OccupantId",
                table: "ConsumptionNorms",
                newName: "IX_ConsumptionNorms_OccupantId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Exemptions",
                table: "Exemptions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ConsumptionNorms",
                table: "ConsumptionNorms",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ConsumptionNorms_Occupants_OccupantId",
                table: "ConsumptionNorms",
                column: "OccupantId",
                principalTable: "Occupants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Occupants_Exemptions_ExemptionId",
                table: "Occupants",
                column: "ExemptionId",
                principalTable: "Exemptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConsumptionNorms_Occupants_OccupantId",
                table: "ConsumptionNorms");

            migrationBuilder.DropForeignKey(
                name: "FK_Occupants_Exemptions_ExemptionId",
                table: "Occupants");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Exemptions",
                table: "Exemptions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ConsumptionNorms",
                table: "ConsumptionNorms");

            migrationBuilder.RenameTable(
                name: "Exemptions",
                newName: "Exemption");

            migrationBuilder.RenameTable(
                name: "ConsumptionNorms",
                newName: "ConsumptionNorm");

            migrationBuilder.RenameIndex(
                name: "IX_ConsumptionNorms_OccupantId",
                table: "ConsumptionNorm",
                newName: "IX_ConsumptionNorm_OccupantId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Exemption",
                table: "Exemption",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ConsumptionNorm",
                table: "ConsumptionNorm",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ConsumptionNorm_ConsumedUtilityId",
                table: "ConsumptionNorm",
                column: "ConsumedUtilityId");

            migrationBuilder.AddForeignKey(
                name: "FK_ConsumptionNorm_ConsumedUtilities_ConsumedUtilityId",
                table: "ConsumptionNorm",
                column: "ConsumedUtilityId",
                principalTable: "ConsumedUtilities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ConsumptionNorm_Occupants_OccupantId",
                table: "ConsumptionNorm",
                column: "OccupantId",
                principalTable: "Occupants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Occupants_Exemption_ExemptionId",
                table: "Occupants",
                column: "ExemptionId",
                principalTable: "Exemption",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
