using SparkClips.Services.BlobBob;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SparkClips.Models.HairyDatabase
{
    public class Image
    {
        public int ImageID { get; set; }
        public string Filename { get; set; }
        public Guid Guid { get; set; }
        public string Url { get; set; }
        public ContainerName ContainerName { get; set; }

        public List<GalleryEntry_Image> GalleryEntryImages { get; set; } // Collection navigation property
        public List<LogEntry_Image> LogEntries { get; set; }
    }

    public class GalleryEntry
    {
        public int GalleryEntryID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Instructions { get; set; }
        // TODO: Add a computed column for nLikes

        //public int Upvotes { get; set; }
        //public int Downvotes { get; set; }

        public List<GalleryEntry_Image> GalleryEntryImages { get; set; } // Collection navigation property
        public List<GalleryEntry_Tag> GalleryEntryTags { get; set; }
        public List<GalleryEntry_ApplicationUser> ApplicationUsers { get; set; }
    }

    /// <summary>
    /// Associative entity table for many to many relationship
    /// between Gallery and Image.
    /// </summary>
    public class GalleryEntry_Image
    {
        public int GalleryEntryID { get; set; }
        public GalleryEntry GalleryEntry { get; set; }

        public int ImageID { get; set; }
        public Image Image { get; set; }


    }

    public class Tag
    {
        public int TagID { get; set; }
        public string Name { get; set; }

        public List<GalleryEntry_Tag> GalleryEntryTags { get; set; }
    }

    public class GalleryEntry_Tag
    {
        public int GalleryEntryID { get; set; }
        public GalleryEntry GalleryEntry { get; set; }

        public int TagID { get; set; }
        public Tag Tag { get; set; }
    }

    public class LogEntry
    {
        public int LogEntryID { get; set; }
        public string Description { get; set; }
        public double Cost { get; set; }
        public DateTime Timestamp { get; set; }
        public string Location { get; set; }
        public string Barbers { get; set; }

        public List<LogEntry_Image> Images { get; set; }
    }

    public class LogEntry_Image
    {
        public int LogEntryID { get; set; }
        public LogEntry LogEntry { get; set; }

        public int ImageID { get; set; }
        public Image Image { get; set; }
    }
    
    public class GalleryEntry_ApplicationUser
    {
        public int GalleryEntryID { get; set; }
        public GalleryEntry GalleryEntry { get; set; }

        public string ApplicationUserID { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}
