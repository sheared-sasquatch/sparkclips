using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SparkClips.Migrations
{
    public partial class attempttoaddassociativeentitybetweenappuserandgalleryentry : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GalleryEntry_ApplicationUser",
                columns: table => new
                {
                    GalleryEntryID = table.Column<int>(nullable: false),
                    ApplicationUserID = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GalleryEntry_ApplicationUser", x => new { x.GalleryEntryID, x.ApplicationUserID });
                    table.ForeignKey(
                        name: "FK_GalleryEntry_ApplicationUser_AspNetUsers_ApplicationUserID",
                        column: x => x.ApplicationUserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GalleryEntry_ApplicationUser_GalleryEntries_GalleryEntryID",
                        column: x => x.GalleryEntryID,
                        principalTable: "GalleryEntries",
                        principalColumn: "GalleryEntryID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GalleryEntry_ApplicationUser_ApplicationUserID",
                table: "GalleryEntry_ApplicationUser",
                column: "ApplicationUserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GalleryEntry_ApplicationUser");
        }
    }
}
