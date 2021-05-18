using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureImageStorage.BLL.Models.DTO.Blob
{
    public class BlobInfoDTO
    {
        public Stream Content { get; set; }
        public string ContentType { get; set; }

        public BlobInfoDTO()
        {

        }

        public BlobInfoDTO(Stream content, string contentType)
        {
            Content = content;
            ContentType = contentType;
        }
    }
}
