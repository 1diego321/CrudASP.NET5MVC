using AzureImageStorage.BLL.Helps;
using AzureImageStorage.BLL.Services;
using AzureImageStorage.BLL.Services.Abstractions;
using AzureImageStorage.DAL.Data;
using AzureImageStorage.DAL.Repositories;
using AzureImageStorage.DAL.Repositories.Abstractions;
using AzureImageStorage.DAL.UnitOfWork;
using AzureImageStorage.DAL.UnitOfWork.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AzureImageStorage.Setups
{
    public static class Dependencies
    {
        public static void SetDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            //DbContext
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultSQL")));

            //AutoMapper
            services.AddAutoMapper(typeof(ApplicationMapper));

            //Services
            services.AddScoped<IApplicationUserService, ApplicationUserService>();
            services.AddScoped<IBlobService, BlobService>();

            //Repositories
            services.AddScoped<IApplicationUserRepository, ApplicationUserRepository>();

            //UnitOfWork
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            
        }
    }
}
