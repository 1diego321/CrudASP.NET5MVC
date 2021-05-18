using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureImageStorage.BLL.Models.ViewModels.Application
{
    public class MessageViewModel
    {
        public string Message { get; set; }
        public string CssClass { get; set; }

        public MessageViewModel(string message, string cssClass)
        {
            Message = message;
            CssClass = cssClass;
        }
    }
}
