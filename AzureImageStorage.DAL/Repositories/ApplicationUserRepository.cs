using AzureImageStorage.DAL.Data;
using AzureImageStorage.DAL.Data.Entities;
using AzureImageStorage.DAL.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureImageStorage.DAL.Repositories
{
    public class ApplicationUserRepository : GenericRepository<ApplicationUser>, IApplicationUserRepository
    {
        private readonly ApplicationDbContext _context;

        public ApplicationUserRepository(ApplicationDbContext context)
            : base(context)
        {
            _context = context;
        }

        public async Task<string> GetImageNameByIdAsync(int id)
        {
            return await _context.ApplicationUser
                .Where(x => x.Id == id)
                .Select(x => x.ImageName)
                .FirstOrDefaultAsync();
        }

        public void Update(ApplicationUser entity)
        {
            _context.ApplicationUser.Update(entity);
        }
    }
}
