using AzureImageStorage.BLL.Helps;
using AzureImageStorage.BLL.Services;
using AzureImageStorage.BLL.Services.Abstractions;
using AzureImageStorage.DAL.Data;
using AzureImageStorage.DAL.Data.Initializer;
using AzureImageStorage.DAL.Data.Initializer.Abstractions;
using AzureImageStorage.DAL.Repositories;
using AzureImageStorage.DAL.Repositories.Abstractions;
using AzureImageStorage.DAL.UnitOfWork;
using AzureImageStorage.DAL.UnitOfWork.Abstractions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AzureImageStorage.Setups
{
    public static class Dependencies
    {
        public static void SetDependencies(this IServiceCollection services, IConfiguration configuration, bool IsDevelopment)
        {
            //ConnectionStrings
            string SqlConnectionString = IsDevelopment 
                ? configuration.GetConnectionString("DevSql") 
                : configuration.GetConnectionString("ProdSql");
            
            string AzureBlobConnectionString = IsDevelopment
                ? configuration.GetConnectionString("DevAzureBlobStorage")
                : configuration.GetConnectionString("ProdAzureBlobStorage");

            //DbContext
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(SqlConnectionString));

            //AzureBlobStorage
            services.AddAzureClients(builder =>
            {
                builder.AddBlobServiceClient(AzureBlobConnectionString);
            });

            //AutoMapper
            services.AddAutoMapper(typeof(ApplicationMapper));

            //Services
            services.AddScoped<IApplicationUserService, ApplicationUserService>();
            services.AddScoped<IBlobService, BlobService>();

            //Repositories
            services.AddScoped<IApplicationUserRepository, ApplicationUserRepository>();

            //UnitOfWork
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            //Initializer
            services.AddScoped<IDbInitializer, DbInitializer>();
        }
    }
}
