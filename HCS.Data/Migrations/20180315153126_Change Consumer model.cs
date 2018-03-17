using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace HCS.Data.Migrations
{
    public partial class ChangeConsumermodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Consumers_HouseholdCategory_HouseholdCategoryId",
                table: "Consumers");

            migrationBuilder.DropForeignKey(
                name: "FK_Consumers_OrganizationCategory_OrganizationCategoryId",
                table: "Consumers");

            migrationBuilder.DropForeignKey(
                name: "FK_Occupants_Consumers_HouseholdId",
                table: "Occupants");

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
                name: "Discriminator",
                table: "Consumers");

            migrationBuilder.DropColumn(
                name: "HouseholdCategoryId",
                table: "Consumers");

            migrationBuilder.DropColumn(
                name: "OrganizationCategoryId",
                table: "Consumers");

            migrationBuilder.RenameColumn(
                name: "HouseholdId",
                table: "Occupants",
                newName: "ConsumerId");

            migrationBuilder.RenameIndex(
                name: "IX_Occupants_HouseholdId",
                table: "Occupants",
                newName: "IX_Occupants_ConsumerId");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Consumers",
                newName: "OrganizationName");

            migrationBuilder.AlterColumn<bool>(
                name: "HasTowelRail",
                table: "Consumers",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "HasSubsidy",
                table: "Consumers",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "HasElectricHotplates",
                table: "Consumers",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "HasElectricHeating",
                table: "Consumers",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "HasCentralGasSupply",
                table: "Consumers",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ConsumerCategoryId",
                table: "Consumers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsDraft",
                table: "Consumers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "ConsumerType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConsumerType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ConsumerCategory",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ConsumerTypeId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConsumerCategory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConsumerCategory_ConsumerType_ConsumerTypeId",
                        column: x => x.ConsumerTypeId,
                        principalTable: "ConsumerType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Consumers_ConsumerCategoryId",
                table: "Consumers",
                column: "ConsumerCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ConsumerCategory_ConsumerTypeId",
                table: "ConsumerCategory",
                column: "ConsumerTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Consumers_ConsumerCategory_ConsumerCategoryId",
                table: "Consumers",
                column: "ConsumerCategoryId",
                principalTable: "ConsumerCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Occupants_Consumers_ConsumerId",
                table: "Occupants",
                column: "ConsumerId",
                principalTable: "Consumers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Consumers_ConsumerCategory_ConsumerCategoryId",
                table: "Consumers");

            migrationBuilder.DropForeignKey(
                name: "FK_Occupants_Consumers_ConsumerId",
                table: "Occupants");

            migrationBuilder.DropTable(
                name: "ConsumerCategory");

            migrationBuilder.DropTable(
                name: "ConsumerType");

            migrationBuilder.DropIndex(
                name: "IX_Consumers_ConsumerCategoryId",
                table: "Consumers");

            migrationBuilder.DropColumn(
                name: "ConsumerCategoryId",
                table: "Consumers");

            migrationBuilder.DropColumn(
                name: "IsDraft",
                table: "Consumers");

            migrationBuilder.RenameColumn(
                name: "ConsumerId",
                table: "Occupants",
                newName: "HouseholdId");

            migrationBuilder.RenameIndex(
                name: "IX_Occupants_ConsumerId",
                table: "Occupants",
                newName: "IX_Occupants_HouseholdId");

            migrationBuilder.RenameColumn(
                name: "OrganizationName",
                table: "Consumers",
                newName: "Name");

            migrationBuilder.AlterColumn<bool>(
                name: "HasTowelRail",
                table: "Consumers",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<bool>(
                name: "HasSubsidy",
                table: "Consumers",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<bool>(
                name: "HasElectricHotplates",
                table: "Consumers",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<bool>(
                name: "HasElectricHeating",
                table: "Consumers",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<bool>(
                name: "HasCentralGasSupply",
                table: "Consumers",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Consumers",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "HouseholdCategoryId",
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

            migrationBuilder.AddForeignKey(
                name: "FK_Occupants_Consumers_HouseholdId",
                table: "Occupants",
                column: "HouseholdId",
                principalTable: "Consumers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
