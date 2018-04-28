using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace HCS.Data.Migrations
{
    public partial class AddConsumptiontoConsumedUtility : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ConsumptionNorm",
                table: "UtilityBillLines",
                newName: "Amount");

            migrationBuilder.AddColumn<double>(
                name: "Consumption",
                table: "ConsumedUtilities",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Consumption",
                table: "ConsumedUtilities");

            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "UtilityBillLines",
                newName: "ConsumptionNorm");
        }
    }
}
