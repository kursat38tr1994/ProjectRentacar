using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Rentacar.BusinessLogic.IdentityLogic.Interfaces;
using Rentacar.DataAccess.Dto.UserDto;
using Rentacar.Models;
using Rentacar.Utility;

namespace Rentacar.BusinessLogic.IdentityLogic
{
    public class RolesLogic : IRolesLogic
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<User> _userManager;

        public RolesLogic(RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }
        
        public void RolesCheck()
        {

            if (_roleManager.RoleExistsAsync(Roles.Admin) != null)
            {
                 _roleManager.CreateAsync(new IdentityRole(Roles.Admin));
            }

            if ( _roleManager.RoleExistsAsync(Roles.Employee) != null)
            {
                 _roleManager.CreateAsync(new IdentityRole(Roles.Employee));
            }

            if ( _roleManager.RoleExistsAsync(Roles.User) != null)
            { 
                _roleManager.CreateAsync(new IdentityRole(Roles.User));
            }
            
        }


        public async Task<User> AddToRole(User user)
        {
            if (user.Role == null)
            {
                await _userManager.AddToRoleAsync(user, Roles.User);
            }
            else
            {
                await _userManager.AddToRoleAsync(user, user.Role);
            }

            return user;
        }
    }
}