using AzureImageStorage.DAL.Data;
using AzureImageStorage.DAL.Repositories.Abstractions;
using AzureImageStorage.DAL.UnitOfWork.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureImageStorage.DAL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public IApplicationUserRepository ApplicationUser { get; private set; }

        public UnitOfWork(IApplicationUserRepository applicationUserRepository, ApplicationDbContext context)
        {
            ApplicationUser = applicationUserRepository;
            _context = context;
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
