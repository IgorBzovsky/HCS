using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace HCS.Data.Migrations
{
    public partial class AddIsMetersReadingandMonthtoUtilityBill : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateEnd",
                table: "UtilityBills");

            migrationBuilder.DropColumn(
                name: "DateStart",
                table: "UtilityBills");

            migrationBuilder.AddColumn<bool>(
                name: "IsMetersReading",
                table: "UtilityBills",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Month",
                table: "UtilityBills",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsMetersReading",
                table: "UtilityBills");

            migrationBuilder.DropColumn(
                name: "Month",
                table: "UtilityBills");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateEnd",
                table: "UtilityBills",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateStart",
                table: "UtilityBills",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
