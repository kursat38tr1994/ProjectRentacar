using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Rentacar.Areas.Admin.Service;
using Rentacar.Areas.Admin.ViewModels;
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

        [BindProperty] public SignUpViewModel SignUpViewModel { get; set; }

        public IdentityController(UserManager<User> userManager, SignInManager<User> signInManager,
            IEmailSender emailSender, RoleManager<IdentityRole> roleManager, ILogger logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _roleManager = roleManager;
            _logger = logger;
        }

        public IActionResult SignUp(string returnUrl = null)
        {
            // ReturnUrl = returnUrl
            SignUpViewModel = new SignUpViewModel
            {
                RoleList = _roleManager.Roles.Where(u => u.Name != SD.User).Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Name
                })
            };


            return View(SignUpViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignUp(SignUpViewModel signUpViewModel, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                if (await _userManager.FindByEmailAsync(signUpViewModel.Email) == null)
                {
                    var user = new User()
                    {
                        Email = signUpViewModel.Email,
                        UserName = signUpViewModel.Email,
                        Role = signUpViewModel.Role,
                        EmailConfirmed = true,
                    };

                    var result = await _userManager.CreateAsync(user, signUpViewModel.Password);
                    // user = await _userManager.FindByEmailAsync(signUpViewModel.Email);

                    if (result.Succeeded)
                    {
                        _logger.LogInformation("User created met wahtwoord");
                        

                        if (!await _roleManager.RoleExistsAsync(SD.Admin))
                        {
                            await _roleManager.CreateAsync(new IdentityRole(SD.Admin));
                        }

                        if (!await _roleManager.RoleExistsAsync(SD.Employee))
                        {
                            await _roleManager.CreateAsync(new IdentityRole(SD.Employee));
                        }

                        if (!await _roleManager.RoleExistsAsync(SD.User))
                        {
                            await _roleManager.CreateAsync(new IdentityRole(SD.User));
                        }

                        if (user.Role == null)
                        {
                            await _userManager.AddToRoleAsync(user, SD.User);
                        }

                        await _userManager.AddToRoleAsync(user, signUpViewModel.Role);
                        
                        await _signInManager.SignInAsync(user, false);
                           
                        return RedirectToAction("SignIn", "Identity", new {Area = "Admin"});

                    }
                    
                    ModelState.AddModelError("SignUp", result.Errors.Select(x => x.Description).ToString());
                    return View(signUpViewModel);


                    // var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    // var claim = new Claim(ClaimTypes.);
                    //  await _userManager.AddToRoleAsync(user,signUpViewModel.Role);
                    //var confirmationLink = Url.ActionLink("ConfirmEmail", "Identity", new {userId = user.Id, @token = token});

                    // await _emailSender.SendEmailAsync("k.dogan@student.fontys.nl", user.Email, "Confirm Your Email", confirmationLink);
                }
            }

            return View(signUpViewModel);
        }

        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);

            var result = await _userManager.ConfirmEmailAsync(user, token);

            if (result.Succeeded)
            {
                return RedirectToAction("SignIn");
            }

            return new NotFoundResult();
        }

        public IActionResult SignIn()
        {
            var model = new SignInViewModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignIn(SignInViewModel model, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                var result =
                    await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User logged in.");
                 //   var user = await _userManager.FindByEmailAsync(model.Email);

                    return RedirectToAction("SignIn");
                }

                ModelState.AddModelError("Login", "Kan niet inloggen!");
            }

            return View(model);
        }


        public IActionResult AccessDenied()
        {
            return View();
        }

        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("SignIn");
        }
    }
}