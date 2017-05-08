using SparkClips.Services.BlobBob;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SparkClips.Models.HairyDatabase
{
    /// <summary>
    /// This table represents an image that is stored in Azure Blob Storage
    /// </summary>
    public class Image
    {
        public int ImageID { get; set; }
        public string Filename { get; set; }
        public Guid Guid { get; set; }
        public string Url { get; set; }
        public ContainerName ContainerName { get; set; }

        public List<GalleryEntry_Image> GalleryEntries { get; set; } // gallery entries that have this image
        public List<LogEntry_Image> LogEntries { get; set; } // log entries that have this image
    }

    /// <summary>
    /// This table represents a gallery entry
    /// </summary>
    public class GalleryEntry
    {
        public int GalleryEntryID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Instructions { get; set; }
        // TODO: Add a computed column for nLikes

        
        /// <summary>
        /// This is a getter method for retrieving a thumbnail URL for this gallery entry.
        /// If this gallery entry has related images, this getter will arbitrarily return the first one
        /// If this gallery entry doesn't have any related images yet, this getter will return a random grayscale
        /// image URL from the unsplash.it placeholder image API (we can tweak this later)
        /// 
        /// You can use this read-only property on a GalleryEntry object as if it were a string field
        /// Example: 
        ///     string thumbnail = galleryEntry.GetThumbnail;
        /// </summary>
        public string GetThumbnail
        {
            get
            {
                if (this.Images.Count() == 0)
                {
                    // if this gallery entry has no images defined, return a random stock photo
                    return "https://unsplash.it/g/200/300/?random";
                } else
                {
                    // get first related entry in the associate many2many table or null
                    GalleryEntry_Image firstImage = this.Images.FirstOrDefault(); 
                    return firstImage.Image.Url;
                }

            }
        }
        public List<GalleryEntry_Image> Images { get; set; } // Images for this gallery entry
        public List<GalleryEntry_Tag> Tags { get; set; } // Tags for this gallery entry
        public List<GalleryEntry_ApplicationUser> ApplicationUsers { get; set; } // Users that have favorited this gallery entry
    }

    /// <summary>
    /// Associative entity table for many to many relationship
    /// between Gallery and Image.
    /// 
    /// </summary>
    public class GalleryEntry_Image
    {
        public int GalleryEntryID { get; set; }
        public GalleryEntry GalleryEntry { get; set; }

        public int ImageID { get; set; }
        public Image Image { get; set; }


    }

    /// <summary>
    /// This table represents a tag that will be applied to gallery entries
    /// And used for filtering in the UI (categorization of gallery entries)
    /// </summary>
    public class Tag
    {
        public int TagID { get; set; }
        public string Name { get; set; }

        public List<GalleryEntry_Tag> GalleryEntries { get; set; } // Gallery entries that have this tag
    }

    /// <summary>
    /// Associative entity table for many to many relationship
    /// between GalleryEntry and Tags.
    /// </summary>
    public class GalleryEntry_Tag
    {
        public int GalleryEntryID { get; set; }
        public GalleryEntry GalleryEntry { get; set; }

        public int TagID { get; set; }
        public Tag Tag { get; set; }
    }

    /// <summary>
    /// This table represents log entries
    /// </summary>
    public class LogEntry
    {
        public int LogEntryID { get; set; }
        public string Description { get; set; }
        public double Cost { get; set; }
        public DateTime Timestamp { get; set; }
        public string Location { get; set; }
        public string Barbers { get; set; }

        public List<LogEntry_Image> Images { get; set; } // Images for this log entry
    }

    /// <summary>
    /// Associative entity table for many to many relationship
    /// between Log Entry and Image.
    /// </summary>
    public class LogEntry_Image
    {
        public int LogEntryID { get; set; }
        public LogEntry LogEntry { get; set; }

        public int ImageID { get; set; }
        public Image Image { get; set; }
    }

    /// <summary>
    /// Associative entity table for many to many relationship
    /// between Gallery entry and ApplicationUser.
    /// 
    /// The rows of this table will represents "likes" by users on gallery entries
    /// Use this table to fetch the user's "saved favorites"
    /// </summary>
    public class GalleryEntry_ApplicationUser
    {
        public int GalleryEntryID { get; set; }
        public GalleryEntry GalleryEntry { get; set; }

        public string ApplicationUserID { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}
