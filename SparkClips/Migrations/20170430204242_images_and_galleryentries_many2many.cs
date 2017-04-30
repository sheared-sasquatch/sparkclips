using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SparkClips.Migrations
{
    public partial class images_and_galleryentries_many2many : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GalleryEntries",
                columns: table => new
                {
                    GalleryEntryID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Body = table.Column<string>(nullable: true),
                    Downvotes = table.Column<int>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Upvotes = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GalleryEntries", x => x.GalleryEntryID);
                });

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    ImageID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Filename = table.Column<string>(nullable: true),
                    Guid = table.Column<Guid>(nullable: false),
                    Url = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.ImageID);
                });

            migrationBuilder.CreateTable(
                name: "GalleryEntry_Image",
                columns: table => new
                {
                    ImageID = table.Column<int>(nullable: false),
                    GalleryEntryID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GalleryEntry_Image", x => new { x.ImageID, x.GalleryEntryID });
                    table.ForeignKey(
                        name: "FK_GalleryEntry_Image_GalleryEntries_GalleryEntryID",
                        column: x => x.GalleryEntryID,
                        principalTable: "GalleryEntries",
                        principalColumn: "GalleryEntryID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GalleryEntry_Image_Images_ImageID",
                        column: x => x.ImageID,
                        principalTable: "Images",
                        principalColumn: "ImageID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GalleryEntry_Image_GalleryEntryID",
                table: "GalleryEntry_Image",
                column: "GalleryEntryID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GalleryEntry_Image");

            migrationBuilder.DropTable(
                name: "GalleryEntries");

            migrationBuilder.DropTable(
                name: "Images");
        }
    }
}
