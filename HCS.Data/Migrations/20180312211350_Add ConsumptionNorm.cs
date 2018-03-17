using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace HCS.Data.Migrations
{
    public partial class AddConsumptionNorm : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ConsumptionNorm",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Amount = table.Column<double>(nullable: false),
                    ConsumedUtilityId = table.Column<int>(nullable: false),
                    OccupantId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConsumptionNorm", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConsumptionNorm_ConsumedUtilities_ConsumedUtilityId",
                        column: x => x.ConsumedUtilityId,
                        principalTable: "ConsumedUtilities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ConsumptionNorm_Occupants_OccupantId",
                        column: x => x.OccupantId,
                        principalTable: "Occupants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ConsumptionNorm_ConsumedUtilityId",
                table: "ConsumptionNorm",
                column: "ConsumedUtilityId");

            migrationBuilder.CreateIndex(
                name: "IX_ConsumptionNorm_OccupantId",
                table: "ConsumptionNorm",
                column: "OccupantId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConsumptionNorm");
        }
    }
}
