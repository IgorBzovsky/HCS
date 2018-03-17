using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace HCS.Data.Migrations
{
    public partial class AddExemptions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ExemptionId",
                table: "Occupants",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Exemption",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Percent = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exemption", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Occupants_ExemptionId",
                table: "Occupants",
                column: "ExemptionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Occupants_Exemption_ExemptionId",
                table: "Occupants",
                column: "ExemptionId",
                principalTable: "Exemption",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Occupants_Exemption_ExemptionId",
                table: "Occupants");

            migrationBuilder.DropTable(
                name: "Exemption");

            migrationBuilder.DropIndex(
                name: "IX_Occupants_ExemptionId",
                table: "Occupants");

            migrationBuilder.DropColumn(
                name: "ExemptionId",
                table: "Occupants");
        }
    }
}
