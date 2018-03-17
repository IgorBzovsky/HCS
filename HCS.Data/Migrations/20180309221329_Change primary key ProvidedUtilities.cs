using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace HCS.Data.Migrations
{
    public partial class ChangeprimarykeyProvidedUtilities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConsumedUtilities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProvidedUtilities",
                table: "ProvidedUtilities");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ProvidedUtilities",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProvidedUtilities",
                table: "ProvidedUtilities",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ProvidedUtilities_ProviderId",
                table: "ProvidedUtilities",
                column: "ProviderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ProvidedUtilities",
                table: "ProvidedUtilities");

            migrationBuilder.DropIndex(
                name: "IX_ProvidedUtilities_ProviderId",
                table: "ProvidedUtilities");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ProvidedUtilities");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProvidedUtilities",
                table: "ProvidedUtilities",
                columns: new[] { "ProviderId", "UtilityId" });

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
    }
}
