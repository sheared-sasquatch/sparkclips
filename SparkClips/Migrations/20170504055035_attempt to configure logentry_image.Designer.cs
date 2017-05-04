using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using SparkClips.Data;
using SparkClips.Services.BlobBob;

namespace SparkClips.Migrations
{
    [DbContext(typeof(SparkClipsContext))]
    [Migration("20170504055035_attempt to configure logentry_image")]
    partial class attempttoconfigurelogentry_image
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

                    b.Property<string>("Description");

                    b.Property<string>("Instructions");

                    b.Property<string>("Title");

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

            modelBuilder.Entity("SparkClips.Models.HairyDatabase.GalleryEntry_Tag", b =>
                {
                    b.Property<int>("TagID");

                    b.Property<int>("GalleryEntryID");

                    b.HasKey("TagID", "GalleryEntryID");

                    b.HasIndex("GalleryEntryID");

                    b.ToTable("GalleryEntry_Tag");
                });

            modelBuilder.Entity("SparkClips.Models.HairyDatabase.Image", b =>
                {
                    b.Property<int>("ImageID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ContainerName");

                    b.Property<string>("Filename");

                    b.Property<Guid>("Guid");

                    b.Property<string>("Url");

                    b.HasKey("ImageID");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("SparkClips.Models.HairyDatabase.LogEntry", b =>
                {
                    b.Property<int>("LogEntryID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Barbers");

                    b.Property<double>("Cost");

                    b.Property<string>("Description");

                    b.Property<string>("Location");

                    b.Property<DateTime>("Timestamp");

                    b.HasKey("LogEntryID");

                    b.ToTable("LogEntries");
                });

            modelBuilder.Entity("SparkClips.Models.HairyDatabase.LogEntry_Image", b =>
                {
                    b.Property<int>("LogEntryID");

                    b.Property<int>("ImageID");

                    b.HasKey("LogEntryID", "ImageID");

                    b.HasIndex("ImageID");

                    b.ToTable("LogEntry_Image");
                });

            modelBuilder.Entity("SparkClips.Models.HairyDatabase.Tag", b =>
                {
                    b.Property<int>("TagID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("TagID");

                    b.ToTable("Tags");
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

            modelBuilder.Entity("SparkClips.Models.HairyDatabase.GalleryEntry_Tag", b =>
                {
                    b.HasOne("SparkClips.Models.HairyDatabase.GalleryEntry", "GalleryEntry")
                        .WithMany("GalleryEntryTags")
                        .HasForeignKey("GalleryEntryID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SparkClips.Models.HairyDatabase.Tag", "Tag")
                        .WithMany("GalleryEntryTags")
                        .HasForeignKey("TagID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SparkClips.Models.HairyDatabase.LogEntry_Image", b =>
                {
                    b.HasOne("SparkClips.Models.HairyDatabase.Image", "Image")
                        .WithMany("LogEntries")
                        .HasForeignKey("ImageID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SparkClips.Models.HairyDatabase.LogEntry", "LogEntry")
                        .WithMany("Images")
                        .HasForeignKey("LogEntryID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
