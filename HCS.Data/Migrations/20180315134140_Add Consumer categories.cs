using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace HCS.Data.Migrations
{
    public partial class AddConsumercategories : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HouseholdCategoryId",
                table: "Consumers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Consumers",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrganizationCategoryId",
                table: "Consumers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "HouseholdCategory",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HouseholdCategory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrganizationCategory",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganizationCategory", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Consumers_HouseholdCategoryId",
                table: "Consumers",
                column: "HouseholdCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Consumers_OrganizationCategoryId",
                table: "Consumers",
                column: "OrganizationCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Consumers_HouseholdCategory_HouseholdCategoryId",
                table: "Consumers",
                column: "HouseholdCategoryId",
                principalTable: "HouseholdCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Consumers_OrganizationCategory_OrganizationCategoryId",
                table: "Consumers",
                column: "OrganizationCategoryId",
                principalTable: "OrganizationCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Consumers_HouseholdCategory_HouseholdCategoryId",
                table: "Consumers");

            migrationBuilder.DropForeignKey(
                name: "FK_Consumers_OrganizationCategory_OrganizationCategoryId",
                table: "Consumers");

            migrationBuilder.DropTable(
                name: "HouseholdCategory");

            migrationBuilder.DropTable(
                name: "OrganizationCategory");

            migrationBuilder.DropIndex(
                name: "IX_Consumers_HouseholdCategoryId",
                table: "Consumers");

            migrationBuilder.DropIndex(
                name: "IX_Consumers_OrganizationCategoryId",
                table: "Consumers");

            migrationBuilder.DropColumn(
                name: "HouseholdCategoryId",
                table: "Consumers");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Consumers");

            migrationBuilder.DropColumn(
                name: "OrganizationCategoryId",
                table: "Consumers");
        }
    }
}
