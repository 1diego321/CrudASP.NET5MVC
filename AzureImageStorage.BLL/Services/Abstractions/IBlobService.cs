using AzureImageStorage.BLL.Models.DTO.Blob;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureImageStorage.BLL.Services.Abstractions
{
    public interface IBlobService
    {
        Task<BlobInfoDTO> GetBlobAsync(string fileName);
        Task<BlobCreateResultDTO> CreateBlobAsync(IFormFile file);
        Task<bool> DeleteBlobAsync(string fileName);
    }
}
