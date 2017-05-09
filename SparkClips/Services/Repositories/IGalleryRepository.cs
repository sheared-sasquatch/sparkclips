using SparkClips.Models.HairyDatabase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SparkClips.Services.Repositories
{
    public interface IGalleryRepository
    {
        Task<List<GalleryEntry>> GetGalleryEntries();
        Task<GalleryEntry> GetGalleryEntryByID(int galleryEntryID);
        Task<int> ComputeNLikes(GalleryEntry galleryEntry);
        string ComputeThumbnail(GalleryEntry galleryEntry);
    }
}
