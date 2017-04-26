using sparkclips.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using LogLevel = Microsoft.Extensions.Logging.LogLevel;

namespace sparkclips.Blob
{
    public class BlobBob  : IBlobBob
    {
        private CloudBlobContainer _GalleryContainer;
        private CloudBlobContainer _LogContainer;

        public BlobBob()
        {
            CloudStorageAccount storageAccount = new CloudStorageAccount(
                 new Microsoft.WindowsAzure.Storage.Auth.StorageCredentials(
                 "smartclipsblobstorage",
                 "4ALtefhgdfdUJ9pEOEgziSnE0Q+RnnDrmmyPysHViDaSiajZrJsy9UKWR0jZlm6BmgCVnpjBSRC2AFf33NuyOA=="), true);

            // Create a blob client.
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            // Get a reference to a container named "mycontainer."
            _GalleryContainer = blobClient.GetContainerReference("gallery");
            _LogContainer = blobClient.GetContainerReference("log");

            _GalleryContainer.CreateIfNotExistsAsync();
            _LogContainer.CreateIfNotExistsAsync();

        }

        public object CloudConfigurationManager { get; private set; }

        public async Task<List<Image>> FetchGalleryImages()
        {
            List<Image> results = new List<Image>();
  
            BlobContinuationToken token = null;
            do
            {
                BlobResultSegment resultSegment = await _GalleryContainer.ListBlobsSegmentedAsync(token);
                token = resultSegment.ContinuationToken;

                foreach (IListBlobItem item in resultSegment.Results)
                {
                    if (item.GetType() == typeof(CloudBlockBlob))
                    {
                        CloudBlockBlob blob = (CloudBlockBlob)item;
                        Image image = new Image();
                        image.Url = blob.Uri.AbsoluteUri;
                        results.Add(image);
                    }
                }
            } while (token != null);

            return results;
        }
    }
}
