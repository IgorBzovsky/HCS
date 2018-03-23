using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace HCS.Data.Migrations
{
    public partial class OptionalTariffId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "TariffId",
                table: "ConsumedUtilities",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.CreateIndex(
                name: "IX_Tariffs_ConsumerTypeId",
                table: "Tariffs",
                column: "ConsumerTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tariffs_ConsumerTypes_ConsumerTypeId",
                table: "Tariffs",
                column: "ConsumerTypeId",
                principalTable: "ConsumerTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tariffs_ConsumerTypes_ConsumerTypeId",
                table: "Tariffs");

            migrationBuilder.DropIndex(
                name: "IX_Tariffs_ConsumerTypeId",
                table: "Tariffs");

            migrationBuilder.AlterColumn<int>(
                name: "TariffId",
                table: "ConsumedUtilities",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}
