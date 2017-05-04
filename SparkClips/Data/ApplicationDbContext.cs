using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SparkClips.Models;
using SparkClips.Models.HairyDatabase;

namespace SparkClips.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<GalleryEntry> GalleryEntries { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<LogEntry> LogEntries { get; set; }

        public DbSet<GalleryEntry_Image> GalleryEntry_Image { get; set; }
        public DbSet<GalleryEntry_Tag> GalleryEntry_Tag { get; set; }
        public DbSet<LogEntry_Image> LogEntry_Image { get; set; }
        public DbSet<GalleryEntry_ApplicationUser> GalleryEntry_ApplicationUser { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);


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

            // Configure LogEntry_Image
            modelBuilder.Entity<LogEntry_Image>()
                .HasKey(t => new { t.LogEntryID, t.ImageID });

            modelBuilder.Entity<LogEntry_Image>()
                .HasOne(lei => lei.LogEntry)
                .WithMany(le => le.Images)
                .HasForeignKey(lei => lei.LogEntryID);

            modelBuilder.Entity<LogEntry_Image>()
                .HasOne(lei => lei.Image)
                .WithMany(i => i.LogEntries)
                .HasForeignKey(lei => lei.ImageID);

            // Configure GalleryEntry_Tag
            modelBuilder.Entity<GalleryEntry_ApplicationUser>()
                .HasKey(t => new { t.GalleryEntryID, t.ApplicationUserID });

            modelBuilder.Entity<GalleryEntry_ApplicationUser>()
                .HasOne(get => get.GalleryEntry)
                .WithMany(ge => ge.ApplicationUsers)
                .HasForeignKey(get => get.GalleryEntryID);

            modelBuilder.Entity<GalleryEntry_ApplicationUser>()
                .HasOne(get => get.ApplicationUser)
                .WithMany(au => au.GalleryEntries)
                .HasForeignKey(get => get.ApplicationUserID);
        }
    }
}
