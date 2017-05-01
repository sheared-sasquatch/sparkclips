using Microsoft.AspNetCore.Http.Internal;
using SparkClips.Models.HairyDatabase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SparkClips.Services.BlobBob
{
    public interface IFileStorage
    {
        Task<Image> UploadImage(ContainerName container, FormFile formFile);
        Task<Image> UploadOrReplaceImage(ContainerName container, FormFile formFile, Guid blobName);
    }

    public enum ContainerName
    {
        Gallery,
        Log,
    }
}
