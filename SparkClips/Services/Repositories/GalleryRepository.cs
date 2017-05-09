using Microsoft.EntityFrameworkCore;
using SparkClips.Data;
using SparkClips.Models.HairyDatabase;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace SparkClips.Services.Repositories
{
    public class GalleryRepository : IGalleryRepository
    {
        private ApplicationDbContext _sparkClipsContext;


        public GalleryRepository(ApplicationDbContext applicationDbContext) {
            _sparkClipsContext = applicationDbContext;
        }

        /// <summary>
        /// Return a list of all of the GalleryEntries
        /// </summary>
        /// <returns></returns>
        public async Task<List<GalleryEntry>> GetGalleryEntries()
        {
            List<GalleryEntry> galleryEntries = await _sparkClipsContext.GalleryEntries
                .Include(galleryEntry => galleryEntry.Images)
                    .ThenInclude(image => image.Image)
                .ToListAsync();

            return galleryEntries;
        }

        /// <summary>
        /// Return a specific GalleryEntry
        /// </summary>
        /// <param name="galleryEntryID">The primary key of the gallery entry you want</param>
        /// <returns>
        /// GalleryEntry or null if a GalleryEntry with that pk doesn't exit
        /// </returns>
        public async Task<GalleryEntry> GetGalleryEntryByID(int galleryEntryID)
        {
            GalleryEntry galleryEntry = await _sparkClipsContext.GalleryEntries
                .Include(ge => ge.Images)
                    .ThenInclude(image => image.Image)
                .Include(ge => ge.Tags)
                    .ThenInclude(tag => tag.Tag)
                .SingleOrDefaultAsync(g => g.GalleryEntryID == galleryEntryID);
            return galleryEntry;
        }

        /// <summary>
        /// Compute the number of likes a particular gallery entry has
        /// </summary>
        /// <param name="galleryEntry">The primary key of the gallery entry</param>
        /// <returns>The number of likes on the gallery entry</returns>
        public async Task<int> ComputeNLikes(GalleryEntry galleryEntry)
        {
            var likes = await _sparkClipsContext.GalleryEntry_ApplicationUser
                .Include(ge_au => ge_au.GalleryEntry)
                .Include(ge_au => ge_au.ApplicationUser)
                .Where(ge_au => ge_au.GalleryEntry == galleryEntry)
                .ToListAsync();

            return likes.Count();
        }

        /// <summary>
        /// Compute the thumbnail url for a particular gallery entry
        /// </summary>
        /// <param name="galleryEntry">The gallery entry whose thumbnail you want to compute
        /// Note: The collection navigation property "Images" needs to be set on this GalleryEntry instance
        /// </param>
        /// <returns>A string which is the thumbnail as a URL</returns>
        public string ComputeThumbnail(GalleryEntry galleryEntry)
        {
            if (galleryEntry.Images.Count() == 0)
            {
                // if this gallery entry has no images defined, return a random stock photo
                return "https://unsplash.it/g/200/300/?random";
            }
            else
            {
                // get first related entry in the associative many2many table
                GalleryEntry_Image firstImage = galleryEntry.Images.First();
                return firstImage.Image.Url;
            }
        }
    }
}
