using AutoMapper;
using AzureImageStorage.BLL.Models.ViewModels.ApplicationUser;
using AzureImageStorage.DAL.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureImageStorage.BLL.Helps
{
    public class ApplicationMapper : Profile
    {
        public ApplicationMapper()
        {
            CreateMap<ApplicationUserCreateViewModel, ApplicationUser>().ReverseMap();
            CreateMap<ApplicationUserViewModel, ApplicationUser>().ReverseMap();
            CreateMap<ApplicationUserEditViewModel, ApplicationUser>().ReverseMap();
        }
    }
}
