using SparkClips.Services.BlobBob;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [Required]
        public Guid Guid { get; set; }
        [Required]
        public string Url { get; set; }
        [Required]
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
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Instructions { get; set; }

        public List<GalleryEntry_Image> Images { get; set; } // Images for this gallery entry
        public List<GalleryEntry_Tag> Tags { get; set; } // Tags for this gallery entry
        public List<GalleryEntry_ApplicationUser> ApplicationUsers { get; set; } // Users that have favorited this gallery entry

        /// <summary>
        /// This field is excluded from the schema and is only being used to store the computed thumbnail
        /// for specific gallery entries (the logic for setting this field should be in the controller).
        /// 
        /// After it is set, you can use this read-only property on a GalleryEntry object as if it were a string field
        /// Example: 
        ///     string thumbnail = galleryEntry.Thumbnail;
        /// </summary>
        [NotMapped]
        public string Thumbnail { get; set; }

        /// <summary>
        /// This field is excluded from the schema and is only being used to store the number of
        /// computed likes for specific gallery entries
        /// The logic for setting this field should be in the controller.
        /// 
        /// After it is set, you can use this read-only property on a GalleryEntry object as if it were an int field
        /// Example: 
        ///     int nLikes = galleryEntry.Likes;
        /// </summary>
        [NotMapped]
        public int Likes { get; set; }

        [NotMapped]
        public Boolean Faved { get; set; }

        [NotMapped]
        public Boolean Filter { get; set; }

        public override string ToString()
        {
            return Title;
        }
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
        [Required]
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

        [DataType(DataType.Currency)]
        public string Cost { get; set; } // make decimal a nullable type so that ef core doesn't make it required by convention
        [Display(Name = "Haircut Date")]
        [DataType(DataType.Date)]

        public DateTime DateTimeCreated { get; set; } // Gets auto set on first save
        public string Location { get; set; }
        public string Barbers { get; set; }


        public string ApplicationUserID { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        public List<LogEntry_Image> Images { get; set; } // Images for this log entry

        [NotMapped]
        public string Thumbnail { get; set; }

        public override string ToString()
        {
            return Description;
        }

        public string AbsoluteUrl {
            get {
                return "/Log/Detail/" + LogEntryID;
            }
        }
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
