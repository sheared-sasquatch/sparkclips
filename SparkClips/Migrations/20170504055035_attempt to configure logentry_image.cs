using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SparkClips.Migrations
{
    public partial class attempttoconfigurelogentry_image : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LogEntry_Image",
                columns: table => new
                {
                    LogEntryID = table.Column<int>(nullable: false),
                    ImageID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogEntry_Image", x => new { x.LogEntryID, x.ImageID });
                    table.ForeignKey(
                        name: "FK_LogEntry_Image_Images_ImageID",
                        column: x => x.ImageID,
                        principalTable: "Images",
                        principalColumn: "ImageID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LogEntry_Image_LogEntries_LogEntryID",
                        column: x => x.LogEntryID,
                        principalTable: "LogEntries",
                        principalColumn: "LogEntryID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LogEntry_Image_ImageID",
                table: "LogEntry_Image",
                column: "ImageID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LogEntry_Image");
        }
    }
}
