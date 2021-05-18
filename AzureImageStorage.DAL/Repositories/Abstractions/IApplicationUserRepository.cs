using AzureImageStorage.DAL.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureImageStorage.DAL.Repositories.Abstractions
{
    public interface IApplicationUserRepository : IGenericRepository<ApplicationUser>
    {
        void Update(ApplicationUser entity);
        Task<string> GetImageNameByIdAsync(int id);
    }
}
