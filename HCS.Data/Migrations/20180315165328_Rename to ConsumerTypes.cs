using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace HCS.Data.Migrations
{
    public partial class RenametoConsumerTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConsumerCategories_ConsumerType_ConsumerTypeId",
                table: "ConsumerCategories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ConsumerType",
                table: "ConsumerType");

            migrationBuilder.RenameTable(
                name: "ConsumerType",
                newName: "ConsumerTypes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ConsumerTypes",
                table: "ConsumerTypes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ConsumerCategories_ConsumerTypes_ConsumerTypeId",
                table: "ConsumerCategories",
                column: "ConsumerTypeId",
                principalTable: "ConsumerTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConsumerCategories_ConsumerTypes_ConsumerTypeId",
                table: "ConsumerCategories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ConsumerTypes",
                table: "ConsumerTypes");

            migrationBuilder.RenameTable(
                name: "ConsumerTypes",
                newName: "ConsumerType");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ConsumerType",
                table: "ConsumerType",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ConsumerCategories_ConsumerType_ConsumerTypeId",
                table: "ConsumerCategories",
                column: "ConsumerTypeId",
                principalTable: "ConsumerType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
