using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace HCS.Data.Migrations
{
    public partial class AddBuildingandAppartmenttoLocation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Appartment",
                table: "Locations",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Building",
                table: "Locations",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Appartment",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "Building",
                table: "Locations");
        }
    }
}
