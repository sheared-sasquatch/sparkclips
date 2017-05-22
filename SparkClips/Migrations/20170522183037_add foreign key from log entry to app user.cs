using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SparkClips.Migrations
{
    public partial class addforeignkeyfromlogentrytoappuser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserID",
                table: "LogEntries",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_LogEntries_ApplicationUserID",
                table: "LogEntries",
                column: "ApplicationUserID");

            migrationBuilder.AddForeignKey(
                name: "FK_LogEntries_AspNetUsers_ApplicationUserID",
                table: "LogEntries",
                column: "ApplicationUserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LogEntries_AspNetUsers_ApplicationUserID",
                table: "LogEntries");

            migrationBuilder.DropIndex(
                name: "IX_LogEntries_ApplicationUserID",
                table: "LogEntries");

            migrationBuilder.DropColumn(
                name: "ApplicationUserID",
                table: "LogEntries");
        }
    }
}
