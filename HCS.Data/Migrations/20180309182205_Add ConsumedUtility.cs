using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace HCS.Data.Migrations
{
    public partial class AddConsumedUtility : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ConsumedUtilities",
                columns: table => new
                {
                    ProvidedUtilityId = table.Column<int>(nullable: false),
                    ConsumerId = table.Column<int>(nullable: false),
                    ProvidedUtilityProviderId = table.Column<int>(nullable: true),
                    ProvidedUtilityUtilityId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConsumedUtilities", x => new { x.ProvidedUtilityId, x.ConsumerId });
                    table.ForeignKey(
                        name: "FK_ConsumedUtilities_Consumers_ConsumerId",
                        column: x => x.ConsumerId,
                        principalTable: "Consumers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ConsumedUtilities_ProvidedUtilities_ProvidedUtilityProviderId_ProvidedUtilityUtilityId",
                        columns: x => new { x.ProvidedUtilityProviderId, x.ProvidedUtilityUtilityId },
                        principalTable: "ProvidedUtilities",
                        principalColumns: new[] { "ProviderId", "UtilityId" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ConsumedUtilities_ConsumerId",
                table: "ConsumedUtilities",
                column: "ConsumerId");

            migrationBuilder.CreateIndex(
                name: "IX_ConsumedUtilities_ProvidedUtilityProviderId_ProvidedUtilityUtilityId",
                table: "ConsumedUtilities",
                columns: new[] { "ProvidedUtilityProviderId", "ProvidedUtilityUtilityId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConsumedUtilities");
        }
    }
}
