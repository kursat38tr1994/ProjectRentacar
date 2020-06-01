using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Rentacar.DataAccess.Dto.UserDto;
using Rentacar.Models;

namespace Rentacar.Areas.Admin.ViewModels
{
    public class SignUpViewModel : IdentityUser
    {
        [Required]
        [DataType(DataType.EmailAddress, ErrorMessage = "Email is niet ingevuld")]
        public override string Email { get; set; }

        //[Required(ErrorMessage = "Password is required")]
        //[StringLength(255, ErrorMessage = "Must be between 5 and 255 characters", MinimumLength = 5)]
        //[DataType(DataType.Password)]
        //public string Password { get; set; }

        // [Required(ErrorMessage = "Confirm Password is required")]
        // [StringLength(255, ErrorMessage = "Must be between 5 and 255 characters", MinimumLength = 5)]
        // [DataType(DataType.Password)]
        // [Compare("Password")]
        // public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(255, ErrorMessage = "Must be between 5 and 255 characters", MinimumLength = 5)]
        [DataType(DataType.Password)]
        public override string PasswordHash { get; set; }

        public string Role { get; set; }

        public IEnumerable<SelectListItem> RoleList { get; set; }
    }
}
