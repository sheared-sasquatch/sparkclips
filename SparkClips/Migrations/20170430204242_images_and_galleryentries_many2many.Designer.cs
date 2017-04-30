using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using SparkClips.Data;

namespace SparkClips.Migrations
{
    [DbContext(typeof(SparkClipsContext))]
    [Migration("20170430204242_images_and_galleryentries_many2many")]
    partial class images_and_galleryentries_many2many
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SparkClips.Models.HairyDatabase.GalleryEntry", b =>
                {
                    b.Property<int>("GalleryEntryID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Body");

                    b.Property<int>("Downvotes");

                    b.Property<string>("Title");

                    b.Property<int>("Upvotes");

                    b.HasKey("GalleryEntryID");

                    b.ToTable("GalleryEntries");
                });

            modelBuilder.Entity("SparkClips.Models.HairyDatabase.GalleryEntry_Image", b =>
                {
                    b.Property<int>("ImageID");

                    b.Property<int>("GalleryEntryID");

                    b.HasKey("ImageID", "GalleryEntryID");

                    b.HasIndex("GalleryEntryID");

                    b.ToTable("GalleryEntry_Image");
                });

            modelBuilder.Entity("SparkClips.Models.HairyDatabase.Image", b =>
                {
                    b.Property<int>("ImageID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Filename");

                    b.Property<Guid>("Guid");

                    b.Property<string>("Url");

                    b.HasKey("ImageID");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("SparkClips.Models.HairyDatabase.GalleryEntry_Image", b =>
                {
                    b.HasOne("SparkClips.Models.HairyDatabase.GalleryEntry", "GalleryEntry")
                        .WithMany("GalleryEntryImages")
                        .HasForeignKey("GalleryEntryID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SparkClips.Models.HairyDatabase.Image", "Image")
                        .WithMany("GalleryEntryImages")
                        .HasForeignKey("ImageID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
