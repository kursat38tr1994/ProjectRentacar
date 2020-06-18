using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Rentacar.BusinessLogic.IdentityLogic.Interfaces;
using Rentacar.DataAccess.Dto.UserDto;
using Rentacar.Models;
using Rentacar.Utility;

namespace Rentacar.BusinessLogic.IdentityLogic
{
    public class RegisterLogic : IRegisterLogic
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly SignInManager<User> _signInManager;

        private readonly IRolesLogic _rolesLogic;
        // private readonly IRolesLogic _roles;

        public RegisterLogic(UserManager<User> userManager, IMapper mapper, SignInManager<User> signInManager, IRolesLogic rolesLogic  )
        {
            _userManager = userManager;
            _mapper = mapper;
            _signInManager = signInManager;
            _rolesLogic = rolesLogic;
        }

        public async Task<User> SignUp(User model)
        {

            if (await _userManager.FindByEmailAsync(model.Email) == null)
            {
                var user = new User()
                {
                    Email = model.Email,
                    UserName = model.Email,
                    Role = model.Role,
                    EmailConfirmed = true,
                };

                var result = await _userManager.CreateAsync(user, model.PasswordHash);

                if (result.Succeeded)
                {
                    await _rolesLogic.AddToRole(user);

                    await _signInManager.SignInAsync(user, false);

                    return model;
                }

               var sa =  result.Errors.Select(a => a.Description);

            }
            return model;
        }
    }
}