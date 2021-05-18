using AzureImageStorage.BLL.Models.ViewModels.ApplicationUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureImageStorage.BLL.Services.Abstractions
{
    public interface IApplicationUserService
    {
        Task<bool> CreateAsync(ApplicationUserCreateViewModel model);
        Task<IEnumerable<ApplicationUserViewModel>> GetAllAsync();
        Task<bool> DeleteAsync(int id);
        Task<bool> ExistsAsync(int value);
        Task<ApplicationUserViewModel> GetByIdAsync(int id);
        Task<ApplicationUserEditViewModel> GetByIdForEditAsync(int id);
        Task<bool> UpdateAsync(ApplicationUserEditViewModel model);
        Task<string> GetImageNameByIdAsync(int id);
    }
}
