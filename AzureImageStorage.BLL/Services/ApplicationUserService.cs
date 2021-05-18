using AutoMapper;
using AzureImageStorage.BLL.Models.ViewModels.ApplicationUser;
using AzureImageStorage.BLL.Services.Abstractions;
using AzureImageStorage.DAL.Data.Entities;
using AzureImageStorage.DAL.UnitOfWork.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureImageStorage.BLL.Services
{
    public class ApplicationUserService : IApplicationUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ApplicationUserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> CreateAsync(ApplicationUserCreateViewModel model)
        {
            await _unitOfWork.ApplicationUser.CreateAsync(_mapper.Map<ApplicationUser>(model));

            return await _unitOfWork.SaveAsync() > 0;
        }

        public async Task<IEnumerable<ApplicationUserViewModel>> GetAllAsync()
        {
            return (await _unitOfWork.ApplicationUser.GetAllAsync())
                .Select(x => _mapper.Map<ApplicationUserViewModel>(x));
        }

        public async Task<bool> DeleteAsync(int id)
        {
            await _unitOfWork.ApplicationUser.Delete(id);

            return await _unitOfWork.SaveAsync() > 0;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _unitOfWork.ApplicationUser.ExistsAsync(id);
        }

        public async Task<ApplicationUserViewModel> GetByIdAsync(int id)
        {
            return _mapper.Map<ApplicationUserViewModel>(await _unitOfWork.ApplicationUser.GetByIdAsync(id));
        }

        public async Task<ApplicationUserEditViewModel> GetByIdForEditAsync(int id)
        {
            return _mapper.Map<ApplicationUserEditViewModel>(await _unitOfWork.ApplicationUser.GetByIdAsync(id));
        }

        public async Task<bool> UpdateAsync(ApplicationUserEditViewModel model)
        {
            var oUser = await _unitOfWork.ApplicationUser.GetByIdAsync(model.Id);

            if (oUser == null) return false;

            if(model.Image != null)
            {
                oUser.ImageName = model.ImageName;
            }

            oUser.Name = model.Name;
            oUser.LastName = model.LastName;

            _unitOfWork.ApplicationUser.Update(oUser);

            return await _unitOfWork.SaveAsync() > 0;
        }

        public async Task<string> GetImageNameByIdAsync(int id)
        {
            return await _unitOfWork.ApplicationUser.GetImageNameByIdAsync(id);
        }
    }
}
