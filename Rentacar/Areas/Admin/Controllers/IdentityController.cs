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

        [BindProperty] public SignUpViewModel SignUpViewModel { get; set; }

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

        public async Task<IActionResult> SignUp()
        {
            // ReturnUrl = returnUrl
            SignUpViewModel = new SignUpViewModel
            {
                RoleList =  _roleManager.Roles.Where(u => u.Name != SD.User).Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Name
                })
            };
            
             return View(SignUpViewModel);
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
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignIn(SignInViewModel model)
        {
            if (ModelState.IsValid)
            {
                var mapper = _mapper.Map<User>(model);
                await _factory.Login.Login(mapper);

                // var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
                //
                // if (result.Succeeded)
                // {
                //     var user = _unitOfWork.User.GetFirstOrDefault(u => u.Email == model.Email);
                //
                //     var count = _unitOfWork.ShoppingCart.GetAll(c => c.UserId == user.Id).Count();
                //
                //     HttpContext.Session.SetInt32(SD.SessionCart, count);
                //
                //     _logger.LogInformation("User logged in.");
                //
                return RedirectToAction("SignIn");
                // }
                //
                // ModelState.AddModelError("Login", "Kan niet inloggen! Email of Wachtwoord is incorrect");
            }

            return View(model);
        }


        public IActionResult AccessDenied()
        {
            return View();
        }

        public async Task<IActionResult> LogOut()
        {
            HttpContext.Session.SetInt32(SD.SessionCart, 0);
            await _signInManager.SignOutAsync();
            return RedirectToAction("SignIn");
        }
    }
}