using Microsoft.EntityFrameworkCore;
using SparkClips.Data;
using SparkClips.Models.HairyDatabase;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace SparkClips.Services.Repositories
{
    public class GalleryRepository : IGalleryRepository, IDisposable
    {
        private ApplicationDbContext _sparkClipsContext;

        public GalleryRepository(ApplicationDbContext applicationDbContext) {
            _sparkClipsContext = applicationDbContext;
        }

        /// <summary>
        /// Return a list of all of the GalleryEntries
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<GalleryEntry>> GetGalleryEntries(List<int> tags)
        {
            IEnumerable<GalleryEntry> galleryEntries = await _sparkClipsContext.GalleryEntries
                .Include(galleryEntry => galleryEntry.Images)
                    .ThenInclude(image => image.Image)
                .Include(galleryEntry => galleryEntry.Tags)
                    .ThenInclude(tag => tag.Tag)
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
            List<GalleryEntry_ApplicationUser> likes = await _sparkClipsContext.GalleryEntry_ApplicationUser
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

        // Queries database to see if given user has favorited given gallery entry
        public bool isFavorited(int galleryEntryId, string userId) {
            return _sparkClipsContext.GalleryEntry_ApplicationUser
                    .Any(e => e.GalleryEntryID == galleryEntryId && e.ApplicationUserID == userId);
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // dispose managed state (managed objects).
                    _sparkClipsContext.Dispose();
                }
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
