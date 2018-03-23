using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace HCS.Data.Migrations
{
    public partial class AddIsSeasonaltoConsumptionNorm : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsSeasonal",
                table: "ConsumptionNorms",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSeasonal",
                table: "ConsumptionNorms");
        }
    }
}
