using AzureImageStorage.BLL.Models.ViewModels.Application;
using AzureImageStorage.BLL.Models.ViewModels.ApplicationUser;
using AzureImageStorage.BLL.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AzureImageStorage.Extensions;
using AzureImageStorage.BLL.Models.DTO.Application;
using Microsoft.AspNetCore.Http;

namespace AzureImageStorage.Controllers
{
    public class ApplicationUserController : Controller
    {
        private readonly IApplicationUserService _appUserService;
        private readonly IBlobService _blobService;

        public ApplicationUserController(IApplicationUserService appUserService, IBlobService blobService)
        {
            _appUserService = appUserService;
            _blobService = blobService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var listUsers = await _appUserService.GetAllAsync();

            return View(listUsers);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new ApplicationUserCreateViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(ApplicationUserCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await _blobService.CreateBlobAsync(model.Image);

            if (result.Success)
            {
                model.ImageName = result.NewFileName;

                if (await _appUserService.CreateAsync(model))
                {
                    var oMessage = new MessageViewModel($"El usuario <a href='#' class='alert-link'>{model.Name}</a> ha sido creado exitosamente!", "alert-success");

                    TempData.PutObject("Message", oMessage);
                }
                else
                {
                    var oMessage = new MessageViewModel("Hubo un problema y no se pudo crear el recurso.", "alert-danger");

                    TempData.PutObject("Message", oMessage);
                }

                return RedirectToAction(nameof(Index));
            }
            else
            {
                ModelState.AddModelError(nameof(model.Image), "Sólo se admiten tipos de archivos PNG o JPG.");

                return View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (!id.HasValue) return NotFound();

            var oUser = await _appUserService.GetByIdForEditAsync(id.Value);

            if (oUser == null) return NotFound();

            return View(oUser);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ApplicationUserEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (model.Image != null)
            {
                string fileName = await _appUserService.GetImageNameByIdAsync(model.Id);

                await _blobService.DeleteBlobAsync(fileName);

                var result = await _blobService.CreateBlobAsync(model.Image);

                if (result.Success)
                {
                    model.ImageName = result.NewFileName;
                }
                else
                {
                    ModelState.AddModelError(nameof(model.Image), "Sólo se admiten tipos de archivos PNG o JPG.");

                    return View(model);
                }
            }

            if (await _appUserService.UpdateAsync(model))
            {
                var oMessage = new MessageViewModel($"El usuario <a href='#' class='alert-link'>{model.Name}</a> ha sido modificado exitosamente!", "alert-success");

                TempData.PutObject("Message", oMessage);
            }
            else
            {
                var oMessage = new MessageViewModel("Hubo un problema y no se modificó el recurso.", "alert-danger");

                TempData.PutObject("Message", oMessage);
            }

            return RedirectToAction(nameof(Index));
        }

        #region API CALLS
        [HttpGet]
        public async Task<IActionResult> GetImage(string n)
        {
            if (n == string.Empty)
            {
                return BadRequest();
            }

            var oImage = await _blobService.GetBlobAsync(n);

            return File(oImage.Content, oImage.ContentType);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int? id)
        {
            ResponseDTO oResponse = new();

            if (id == null)
            {
                oResponse.Message = "Se debe indicar el identificador del recurso.";

                return BadRequest(oResponse);
            }

            ApplicationUserViewModel oUser = await _appUserService.GetByIdAsync(id.Value);

            if(oUser != null)
            {
                string fileName = oUser.ImageName;

                if (await _appUserService.DeleteAsync(id.Value))
                {
                    oResponse.Message = "El recurso se ha eliminado exitosamente!";
                    oResponse.Success = true;

                    try
                    {
                        await _blobService.DeleteBlobAsync(fileName);
                    }
                    catch (Exception ex)
                    {
                        oResponse.Message = "El recurso se ha eliminado exitosamente, aunque tuvimos un problema al intentar eliminar la imagen del sistema.";
                    }

                    return Ok(oResponse);
                }
                else
                {
                    oResponse.Message = "Hubo un problema, no se pudo eliminar el registro.";

                    return StatusCode(StatusCodes.Status500InternalServerError, oResponse);
                }
            }
            else
            {
                oResponse.Message = "No se ha encontrado el recurso solicitado.";

                return NotFound(oResponse);
            }
        }

        [HttpGet]
        public async Task <IActionResult> RefreshTable()
        {
            return PartialView("_IndexTablePartial", await _appUserService.GetAllAsync());
        }
        #endregion
    }
}
