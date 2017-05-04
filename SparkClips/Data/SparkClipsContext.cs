using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SparkClips.Models.HairyDatabase;

namespace SparkClips.Data
{
    public class SparkClipsContext : DbContext
    {
        public SparkClipsContext(DbContextOptions<SparkClipsContext> options) 
            : base(options)
        { }

        public DbSet<Image> Images { get; set; }
        public DbSet<GalleryEntry> GalleryEntries { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<LogEntry> LogEntries { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure GalleryEntry_Image
            modelBuilder.Entity<GalleryEntry_Image>()
                .HasKey(t => new { t.ImageID, t.GalleryEntryID });

            modelBuilder.Entity<GalleryEntry_Image>()
                .HasOne(gei => gei.GalleryEntry)
                .WithMany(ge => ge.GalleryEntryImages)
                .HasForeignKey(gei => gei.GalleryEntryID);

            modelBuilder.Entity<GalleryEntry_Image>()
                .HasOne(gei => gei.Image)
                .WithMany(i => i.GalleryEntryImages)
                .HasForeignKey(gei => gei.ImageID);

            // Configure GalleryEntry_Tag
            modelBuilder.Entity<GalleryEntry_Tag>()
                .HasKey(t => new { t.TagID, t.GalleryEntryID });

            modelBuilder.Entity<GalleryEntry_Tag>()
                .HasOne(get => get.GalleryEntry)
                .WithMany(ge => ge.GalleryEntryTags)
                .HasForeignKey(get => get.GalleryEntryID);

            modelBuilder.Entity<GalleryEntry_Tag>()
                  .HasOne(get => get.Tag)
                  .WithMany(ge => ge.GalleryEntryTags)
                  .HasForeignKey(get => get.TagID);
        }

        // I'm not totally sure that this line should be here
        public DbSet<GalleryEntry_Image> GalleryEntry_Image { get; set; }
        public DbSet<GalleryEntry_Tag> GalleryEntry_Tag { get; set; }
        //public DbSet<LogEntry_Image> LogEntry_Image { get; set; }
        //public DbSet<GalleryEntry_ApplicationUser> GalleryEntry_ApplicationUser { get; set; }
    }
}
