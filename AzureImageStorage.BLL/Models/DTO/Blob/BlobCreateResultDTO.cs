using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureImageStorage.BLL.Models.DTO.Blob
{
    public class BlobCreateResultDTO
    {
        public bool Success { get; set; }
        public string NewFileName { get; set; }

        public BlobCreateResultDTO()
        {
            Success = false;
        }
    }
}
