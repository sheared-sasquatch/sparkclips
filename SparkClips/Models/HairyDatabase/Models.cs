using System;
using System.Collections.Generic;
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
        // public Container Container { get; set; }

        public List<GalleryEntry> GalleryEntries { get; set; } // Collection navigation property
    }

    public class GalleryEntry
    {
        public int GalleryEntryID { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public int Upvotes { get; set; }
        public int Downvotes { get; set; }

        public List<Image> Images { get; set; } // Collection navigation property
    }

    /// <summary>
    /// Associative entity table for many to many relationship
    /// between Gallery and Image.
    /// </summary>
    public class GalleryEntry_Image
    {
        public int ImageID { get; set; }
        public Image Image { get; set; }

        public int GalleryEntryID { get; set; }
        public GalleryEntry GalleryEntry { get; set; }
    }
}
