using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using SparkClips.Models.HairyDatabase;

namespace SparkClips.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public string HairColor { get; set; }

        public List<GalleryEntry_ApplicationUser> GalleryEntries { get; set; }
        public List<LogEntry> LogEntries { get; set; }
    }
}
