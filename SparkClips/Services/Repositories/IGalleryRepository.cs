using SparkClips.Models.HairyDatabase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SparkClips.Services.Repositories
{
    public interface IGalleryRepository
    {
        Task<IEnumerable<GalleryEntry>> GetGalleryEntries(List<int> tags);
        Task<GalleryEntry> GetGalleryEntryByID(int galleryEntryID);
        Task<int> ComputeNLikes(GalleryEntry galleryEntry);
        string ComputeThumbnail(GalleryEntry galleryEntry);
        bool isFavorited(int galleryEntryId, string userId);
    }
}
