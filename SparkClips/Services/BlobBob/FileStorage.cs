﻿using Microsoft.AspNetCore.Http.Internal;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using SparkClips.Models.HairyDatabase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SparkClips.Services.BlobBob
{
    public class FileStorage : IFileStorage
    {
        private CloudBlobContainer _GalleryContainer;
        private CloudBlobContainer _LogContainer;


        public FileStorage()
        {
            CloudStorageAccount storageAccount = new CloudStorageAccount(
               new Microsoft.WindowsAzure.Storage.Auth.StorageCredentials(
               "smartclipsblobstorage",
               "4ALtefhgdfdUJ9pEOEgziSnE0Q+RnnDrmmyPysHViDaSiajZrJsy9UKWR0jZlm6BmgCVnpjBSRC2AFf33NuyOA=="), true);

            // Create a blob client.
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            // Get a reference to containers
            _GalleryContainer = blobClient.GetContainerReference("gallery");
            _GalleryContainer.CreateIfNotExistsAsync();
            _LogContainer = blobClient.GetContainerReference("log");
            _LogContainer.CreateIfNotExistsAsync();
        }

        /// <summary>
        /// Upload an image to blob storage
        /// </summary>
        /// <param name="container">The container to upload the blob to.
        /// Container is an enum.</param>
        /// <param name="formFile">The FormFile object which contains the byte data and meta-data
        /// for the file being uploaded.</param>
        /// <returns>A Image object which contains the guid filename given to the blob in blob storage.
        /// This object also contains the URL that you can use to access the blob file from online.</returns>
        public async Task<Image> UploadImage(ContainerName container, FormFile formFile) {
            Guid blobName = Guid.NewGuid();
            return await UploadOrReplaceImage(container, formFile, blobName);
        }

        /// <summary>
        /// Upload an image to blob storage
        /// OR
        /// Replace it if a file with the provided guid already exists
        /// </summary>
        /// <param name="container">The container to upload the blob to.
        /// Container is an enum.</param>
        /// <param name="formFile">The FormFile object which contains the byte data and meta-data
        /// for the file being uploaded.</param>
        /// <param name="blobName">The guid which will become the blob filename in azure storage.
        /// </param>
        /// <returns>A Image object which contains the guid filename given to the blob in blob storage.
        /// This object also contains the URL that you can use to access the blob file from online.</returns>
        public async Task<Image> UploadOrReplaceImage(ContainerName container, FormFile formFile, Guid blobName)
        {

            CloudBlockBlob blockBlob;
            if (container == ContainerName.Gallery)
            { // check which container to upload the blob to
                blockBlob = _GalleryContainer.GetBlockBlobReference(blobName.ToString());
            }
            else if (container == ContainerName.Log)
            {
                blockBlob = _LogContainer.GetBlockBlobReference(blobName.ToString());
            }
            else
            {
                throw new ArgumentException("Invalid enum ContainerName value", container.ToString());
            }


            blockBlob.Properties.ContentType = formFile.ContentType; // set the new blob's mime type
            // Create or overwrite the blob with the contents of formFile stream
            await blockBlob.UploadFromStreamAsync(formFile.OpenReadStream());

            Image image = new Image
            {
                Url = blockBlob.StorageUri.PrimaryUri.ToString(),
                Guid = blobName,
                Filename = formFile.FileName,
                ContainerName = container
            };

            return image;
        }

    }
}
