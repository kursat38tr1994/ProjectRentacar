using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Rentacar.Models;

namespace Rentacar.Areas.Admin.ViewModels
{
    public class SignInViewModel : IdentityUser
    {
        [Required(ErrorMessage = "Email moet ingevuld worden!")]
        [DataType(DataType.EmailAddress)]
        public override string Email { get; set; }

        [Required(ErrorMessage = "Wachtwoord moet ingevuld worden!")]
        [DataType(DataType.Password)]
        public override string PasswordHash { get; set; }

        public bool RememberMe { get; set; }
    }
}
