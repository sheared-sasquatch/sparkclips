using Microsoft.AspNetCore.Http.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SparkClips.Services.BlobBob
{
    public interface IFileStorage
    {
        Task<StoredFile> UploadImage(ContainerName container, FormFile formFile);
    }

    public enum ContainerName
    {
        Gallery,
        Log,
    }
}
