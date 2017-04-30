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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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
        }

        public DbSet<SparkClips.Models.HairyDatabase.GalleryEntry_Image> GalleryEntry_Image { get; set; }
    }
}
