using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Rentacar.Areas.Admin.ViewModels
{
    public class SignUpViewModel
    {
        [Required]
        [DataType(DataType.EmailAddress, ErrorMessage = "Email is niet ingevuld")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password, ErrorMessage = "Incorrent or Missing password")]
        public string Password { get; set; }
        
        public string Role { get; set; }

        public IEnumerable<SelectListItem> RoleList { get; set; }
    }
}
