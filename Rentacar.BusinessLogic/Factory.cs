using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Rentacar.BusinessLogic.IdentityLogic;
using Rentacar.BusinessLogic.IdentityLogic.Interfaces;
using Rentacar.DataAccess.Data.Repository.IRepository;
using Rentacar.DataAccess.Dto.UserDto;
using Rentacar.Models;

namespace Rentacar.BusinessLogic
{
    public class Factory : IFactory
    {
        private readonly SignInManager<User> _userInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly IRolesLogic _rolesLogic;

        public Factory( SignInManager<User> signInManager, RoleManager<IdentityRole> roleManager, 
                                                IMapper mapper, UserManager<User> userManager, IRolesLogic rolesLogic) 
        {
      
            _userInManager = signInManager;
            _roleManager = roleManager;
            _mapper = mapper;
            _userManager = userManager;
            _rolesLogic = rolesLogic;

            Register = new RegisterLogic( _userManager, _mapper, _userInManager, _rolesLogic);
            Login = new LoginLogic(_userInManager);
            LogOut = new LogOut(_userInManager);
        }

        public IRegisterLogic Register { get; private set; }
        public ILoginLogic Login { get; private set; }
        public ILogOut LogOut { get; private set; }

    }
}
