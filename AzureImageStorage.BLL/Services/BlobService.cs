using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using AzureImageStorage.BLL.Models.DTO.Blob;
using AzureImageStorage.BLL.Services.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureImageStorage.BLL.Services
{
    public class BlobService : IBlobService
    {
        private readonly BlobServiceClient _blobService;
        private readonly IConfiguration _configuration;
        private readonly string _containerName;

        public BlobService(BlobServiceClient blobService, IConfiguration configuration)
        {
            _blobService = blobService;
            _configuration = configuration;
            _containerName = _configuration.GetSection("AppSettings:ImagesBlobContainer").Value;
        }

        public async Task<BlobInfoDTO> GetBlobAsync(string fileName)
        {
            var containerClient = _blobService.GetBlobContainerClient(_containerName);
            var blobClient = containerClient.GetBlobClient(fileName);

            var blobDownloadInfo = await blobClient.DownloadAsync();

            return new BlobInfoDTO(blobDownloadInfo.Value.Content, blobDownloadInfo.Value.ContentType);
        }

        public async Task<BlobCreateResultDTO> CreateBlobAsync(IFormFile file)
        {
            BlobCreateResultDTO oResult = new();

            if (!(file.ContentType == "image/jpeg" || file.ContentType == "image/png"))
            {
                return oResult;
            }

            string newFileName = $"user_{Guid.NewGuid().ToString()}{Path.GetExtension(file.FileName)}";

            var containerClient = _blobService.GetBlobContainerClient(_containerName);
            var blobClient = containerClient.GetBlobClient(newFileName);

            using (var stream = file.OpenReadStream())
            {
                await blobClient.UploadAsync(stream, new BlobHttpHeaders
                {
                    ContentType = file.ContentType
                });
            }

            oResult.Success = true;
            oResult.NewFileName = newFileName;

            return oResult;
        }

        public async Task<bool> DeleteBlobAsync(string fileName)
        {
            var containerClient = _blobService.GetBlobContainerClient(_containerName);
            var blobClient = containerClient.GetBlobClient(fileName);

            return await blobClient.DeleteIfExistsAsync();
        }
    }
}
