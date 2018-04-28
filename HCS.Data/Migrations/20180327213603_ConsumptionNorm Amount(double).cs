using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace HCS.Data.Migrations
{
    public partial class ConsumptionNormAmountdouble : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ObligatoryPrice",
                table: "ConsumedUtilities",
                newName: "Subsidy");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "UtilityBillLines",
                nullable: false,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<double>(
                name: "Limit",
                table: "Blocks",
                nullable: false,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Subsidy",
                table: "ConsumedUtilities",
                newName: "ObligatoryPrice");

            migrationBuilder.AlterColumn<double>(
                name: "Price",
                table: "UtilityBillLines",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<int>(
                name: "Limit",
                table: "Blocks",
                nullable: false,
                oldClrType: typeof(double));
        }
    }
}
