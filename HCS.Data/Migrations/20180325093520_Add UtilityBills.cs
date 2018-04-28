using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace HCS.Data.Migrations
{
    public partial class AddUtilityBills : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UtilityBills",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateEnd = table.Column<DateTime>(nullable: false),
                    DateStart = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UtilityBills", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UtilityBillLines",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ConsumedUtilityId = table.Column<int>(nullable: false),
                    ConsumptionNorm = table.Column<double>(nullable: false),
                    MeterReadingEnd = table.Column<double>(nullable: false),
                    MeterReadingStart = table.Column<double>(nullable: false),
                    Price = table.Column<double>(nullable: false),
                    UtilityBillId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UtilityBillLines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UtilityBillLines_ConsumedUtilities_ConsumedUtilityId",
                        column: x => x.ConsumedUtilityId,
                        principalTable: "ConsumedUtilities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UtilityBillLines_UtilityBills_UtilityBillId",
                        column: x => x.UtilityBillId,
                        principalTable: "UtilityBills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UtilityBillLines_ConsumedUtilityId",
                table: "UtilityBillLines",
                column: "ConsumedUtilityId");

            migrationBuilder.CreateIndex(
                name: "IX_UtilityBillLines_UtilityBillId",
                table: "UtilityBillLines",
                column: "UtilityBillId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UtilityBillLines");

            migrationBuilder.DropTable(
                name: "UtilityBills");
        }
    }
}
