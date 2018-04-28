using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace HCS.Data.Migrations
{
    public partial class AddConsumertoUtilityBill : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ConsumerId",
                table: "UtilityBills",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_UtilityBills_ConsumerId",
                table: "UtilityBills",
                column: "ConsumerId");

            migrationBuilder.AddForeignKey(
                name: "FK_UtilityBills_Consumers_ConsumerId",
                table: "UtilityBills",
                column: "ConsumerId",
                principalTable: "Consumers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UtilityBills_Consumers_ConsumerId",
                table: "UtilityBills");

            migrationBuilder.DropIndex(
                name: "IX_UtilityBills_ConsumerId",
                table: "UtilityBills");

            migrationBuilder.DropColumn(
                name: "ConsumerId",
                table: "UtilityBills");
        }
    }
}
