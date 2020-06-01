using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Rentacar.DataAccess.Data.Repository.IRepository;
using Rentacar.Models;
using Rentacar.Utility;
using System.Web;
using Rentacar.BusinessLogic.IdentityLogic.Interfaces;

namespace Rentacar.BusinessLogic.IdentityLogic
{
    public class LoginLogic : ILoginLogic
    {
        private readonly SignInManager<User> _signInManager;
        private readonly IUnitOfWork _unitOfWork;

        public LoginLogic(SignInManager<User> signInManager)
        {
            _signInManager = signInManager;
       
        }
        
        
        public async Task<User> Login(User model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.PasswordHash, false, false);

            if (result.Succeeded)
            {
                return model;
            }
            return model;
        }
        
    }
}