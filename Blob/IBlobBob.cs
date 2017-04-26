using sparkclips.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sparkclips.Blob
{
    public interface IBlobBob
    {
        Task<List<Image>> FetchGalleryImages();
    }
}
