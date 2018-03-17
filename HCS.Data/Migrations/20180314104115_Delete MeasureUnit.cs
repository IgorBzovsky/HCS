using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace HCS.Data.Migrations
{
    public partial class DeleteMeasureUnit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Utilities_MeasureUnits_MeasureUnitId",
                table: "Utilities");

            migrationBuilder.DropTable(
                name: "MeasureUnits");

            migrationBuilder.DropIndex(
                name: "IX_Utilities_MeasureUnitId",
                table: "Utilities");

            migrationBuilder.DropColumn(
                name: "MeasureUnitId",
                table: "Utilities");

            migrationBuilder.AddColumn<string>(
                name: "MeasureUnit",
                table: "Utilities",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MeasureUnit",
                table: "Utilities");

            migrationBuilder.AddColumn<int>(
                name: "MeasureUnitId",
                table: "Utilities",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "MeasureUnits",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeasureUnits", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Utilities_MeasureUnitId",
                table: "Utilities",
                column: "MeasureUnitId");

            migrationBuilder.AddForeignKey(
                name: "FK_Utilities_MeasureUnits_MeasureUnitId",
                table: "Utilities",
                column: "MeasureUnitId",
                principalTable: "MeasureUnits",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
