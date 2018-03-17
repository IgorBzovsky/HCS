using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace HCS.Data.Migrations
{
    public partial class RenametoConsumerCategories : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConsumerCategory_ConsumerType_ConsumerTypeId",
                table: "ConsumerCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_Consumers_ConsumerCategory_ConsumerCategoryId",
                table: "Consumers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ConsumerCategory",
                table: "ConsumerCategory");

            migrationBuilder.RenameTable(
                name: "ConsumerCategory",
                newName: "ConsumerCategories");

            migrationBuilder.RenameIndex(
                name: "IX_ConsumerCategory_ConsumerTypeId",
                table: "ConsumerCategories",
                newName: "IX_ConsumerCategories_ConsumerTypeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ConsumerCategories",
                table: "ConsumerCategories",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ConsumerCategories_ConsumerType_ConsumerTypeId",
                table: "ConsumerCategories",
                column: "ConsumerTypeId",
                principalTable: "ConsumerType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Consumers_ConsumerCategories_ConsumerCategoryId",
                table: "Consumers",
                column: "ConsumerCategoryId",
                principalTable: "ConsumerCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConsumerCategories_ConsumerType_ConsumerTypeId",
                table: "ConsumerCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_Consumers_ConsumerCategories_ConsumerCategoryId",
                table: "Consumers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ConsumerCategories",
                table: "ConsumerCategories");

            migrationBuilder.RenameTable(
                name: "ConsumerCategories",
                newName: "ConsumerCategory");

            migrationBuilder.RenameIndex(
                name: "IX_ConsumerCategories_ConsumerTypeId",
                table: "ConsumerCategory",
                newName: "IX_ConsumerCategory_ConsumerTypeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ConsumerCategory",
                table: "ConsumerCategory",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ConsumerCategory_ConsumerType_ConsumerTypeId",
                table: "ConsumerCategory",
                column: "ConsumerTypeId",
                principalTable: "ConsumerType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Consumers_ConsumerCategory_ConsumerCategoryId",
                table: "Consumers",
                column: "ConsumerCategoryId",
                principalTable: "ConsumerCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
