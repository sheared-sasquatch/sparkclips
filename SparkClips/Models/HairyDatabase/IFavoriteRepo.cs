using System.Collections.Generic;

namespace SparkClips.Models
{
    public interface IFavoriteRepo
    {
        void Add(GalleryEntry_ApplicationUser item);
        IEnumerable<GalleryEntry_ApplicationUser> GetAll();
        GalleryEntry_ApplicationUser Find(long key);
        void Remove(long key);

    }
}
