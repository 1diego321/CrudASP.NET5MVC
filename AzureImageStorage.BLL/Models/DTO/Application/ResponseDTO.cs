using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureImageStorage.BLL.Models.DTO.Application
{
    public class ResponseDTO
    {
        public bool Success { get; set; }
        public bool HasData { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }

        public ResponseDTO()
        {
            Success = false;
            HasData = false;
        }
    }
}
