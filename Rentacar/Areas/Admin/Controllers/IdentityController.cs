using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Rentacar.Areas.Admin.Service;
using Rentacar.Areas.Admin.ViewModels;
using Rentacar.BusinessLogic;
using Rentacar.BusinessLogic.IdentityLogic;
using Rentacar.BusinessLogic.IdentityLogic.Interfaces;
using Rentacar.DataAccess.Data.Repository.IRepository;
using Rentacar.DataAccess.Dto.UserDto;
using Rentacar.Extensions;
using Rentacar.Models;
using Rentacar.Utility;

namespace Rentacar.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class IdentityController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger _logger;

        private readonly IUnitOfWork _unitOfWork;

        //private readonly IRegister _register;
        private readonly IMapper _mapper;
        private readonly IFactory _factory;
        private readonly IRegisterLogic _registerLogic;


        public IdentityController(UserManager<User> userManager, SignInManager<User> signInManager,
            IEmailSender emailSender, RoleManager<IdentityRole> roleManager, ILogger logger, IUnitOfWork unitOfWork,
            IMapper mapper, IFactory factory, IRegisterLogic registerLogic)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _roleManager = roleManager;
            _logger = logger;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _factory = factory;
            _registerLogic = registerLogic;
        }

        public IActionResult SignUp()
        {
            // ReturnUrl = returnUrl
            var model = new SignUpViewModel
            {
                RoleList = _roleManager.Roles.Where(u => u.Name != Roles.User).Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Name
                })
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignUp(SignUpViewModel signUpViewModel)
        {
            if (ModelState.IsValid)
            {
                var mapper = _mapper.Map<User>(signUpViewModel);

                await _factory.Register.SignUp(mapper);
            }


            return RedirectToAction("SignIn");
        }

        // public async Task<IActionResult> ConfirmEmail(string userId, string token)
        // {
        //     var user = await _userManager.FindByIdAsync(userId);
        //
        //     var result = await _userManager.ConfirmEmailAsync(user, token);
        //
        //     if (result.Succeeded)
        //     {
        //         return RedirectToAction("SignIn");
        //     }
        //
        //     return new NotFoundResult();
        // }

        public IActionResult SignIn()
        {
            var model = new SignInViewModel();
            if (_signInManager.IsSignedIn(User))
            {
                return RedirectToAction("Index", "Car", new {area = "Customer"});
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignIn(SignInViewModel model)
        {
            if (ModelState.IsValid)
            {
                var mapper = _mapper.Map<User>(model);
                var result = await _factory.Login.Login(mapper);

                if (result == false)
                {
                    ModelState.AddModelError("Login", "Email of Wachtwoord in incorrect / Email bestaat niet!");
                    return View(model);
                }
                return RedirectToAction("Index", "Car", new {area = "Customer"});
            }
            return View(model);
        }


        public IActionResult AccessDenied()
        {
            return View();
        }

        public async Task<IActionResult> LogOut()
        {
            _factory.LogOut.LogOutUser();
            return RedirectToAction("SignIn");
        }
    }
}