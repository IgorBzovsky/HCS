using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace HCS.Data.Migrations
{
    public partial class AddApplicationUsertoConsumer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Consumers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Consumers_ApplicationUserId",
                table: "Consumers",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Consumers_AspNetUsers_ApplicationUserId",
                table: "Consumers",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Consumers_AspNetUsers_ApplicationUserId",
                table: "Consumers");

            migrationBuilder.DropIndex(
                name: "IX_Consumers_ApplicationUserId",
                table: "Consumers");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Consumers");
        }
    }
}
