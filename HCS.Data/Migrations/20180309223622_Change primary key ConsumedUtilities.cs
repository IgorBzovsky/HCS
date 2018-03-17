using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace HCS.Data.Migrations
{
    public partial class ChangeprimarykeyConsumedUtilities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ConsumedUtilities",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ConsumerId = table.Column<int>(nullable: false),
                    ProvidedUtilityId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConsumedUtilities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConsumedUtilities_Consumers_ConsumerId",
                        column: x => x.ConsumerId,
                        principalTable: "Consumers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ConsumedUtilities_ProvidedUtilities_ProvidedUtilityId",
                        column: x => x.ProvidedUtilityId,
                        principalTable: "ProvidedUtilities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ConsumedUtilities_ConsumerId",
                table: "ConsumedUtilities",
                column: "ConsumerId");

            migrationBuilder.CreateIndex(
                name: "IX_ConsumedUtilities_ProvidedUtilityId",
                table: "ConsumedUtilities",
                column: "ProvidedUtilityId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConsumedUtilities");
        }
    }
}
