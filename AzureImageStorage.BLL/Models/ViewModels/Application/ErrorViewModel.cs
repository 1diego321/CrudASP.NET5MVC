using System;

namespace AzureImageStorage.BLL.Models.ViewModels.Application
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
