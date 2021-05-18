using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureImageStorage.BLL.Models.ViewModels.ApplicationUser
{
    public class ApplicationUserCreateViewModel
    {
        [Required(ErrorMessage = "El campo es obligatorio.")]
        [StringLength(50)]
        [Display(Name = "Nombres")]
        public string Name { get; set; }

        [Required(ErrorMessage = "El campo es obligatorio.")]
        [StringLength(50)]
        [Display(Name = "Apellidos")]
        public string LastName { get; set; }

        //[Required]
        [StringLength(100)]
        public string ImageName { get; set; }

        [Required(ErrorMessage = "El campo es obligatorio.")]
        [Display(Name = "Imagen")]
        public IFormFile Image { get; set; }
    }
}
