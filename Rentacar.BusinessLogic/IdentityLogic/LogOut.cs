using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Rentacar.Models;
using Rentacar.Utility;

namespace Rentacar.BusinessLogic.IdentityLogic
{
    public class LogOut : ILogOut
    {
        private readonly SignInManager<User> _signInManager;

        public LogOut(SignInManager<User> signInManager)
        {
            _signInManager = signInManager;
        }

        public void LogOutUser()
        { 
            _signInManager.SignOutAsync();
        }
    }
}
