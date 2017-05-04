using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SparkClips.Migrations
{
    public partial class initialhalfbakedschemadumpfrommeeting : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Downvotes",
                table: "GalleryEntries");

            migrationBuilder.DropColumn(
                name: "Upvotes",
                table: "GalleryEntries");

            migrationBuilder.RenameColumn(
                name: "Body",
                table: "GalleryEntries",
                newName: "Instructions");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "GalleryEntries",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "GalleryEntries");

            migrationBuilder.RenameColumn(
                name: "Instructions",
                table: "GalleryEntries",
                newName: "Body");

            migrationBuilder.AddColumn<int>(
                name: "Downvotes",
                table: "GalleryEntries",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Upvotes",
                table: "GalleryEntries",
                nullable: false,
                defaultValue: 0);
        }
    }
}
