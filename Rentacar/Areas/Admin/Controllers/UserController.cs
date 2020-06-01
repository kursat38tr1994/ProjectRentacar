using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Rentacar.Areas.Admin.ViewModels;
using Rentacar.DataAccess;
using Rentacar.DataAccess.Data.Repository.IRepository;
using Rentacar.Models;
using Rentacar.Utility;

namespace Rentacar.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Admin)]
    public class UserController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ApplicationDbContext _db;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IMapper _mapper;


        [BindProperty] public UserViewModel UserViewModel { get; set; }
        
        public UserController(IUnitOfWork unitOfWork, ApplicationDbContext db, UserManager<User> userManager,
            SignInManager<User> signInManager, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _db = db;
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
        }


        public IActionResult Index()
        {
            var users = _userManager.Users;
            return View(users);
        }

        [HttpGet]
        public async Task<IActionResult> EditProfile(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {id} cannot be found";
                return NotFound();
            }

            // GetClaimsAsync retunrs the list of user Claims
            var userClaims = await _userManager.GetClaimsAsync(user);
            // GetRolesAsync returns the list of user Roles
            var userRoles = await _userManager.GetRolesAsync(user);

            var model = new UserViewModel()
            {
                Id = user.Id,
                Email = user.Email,
                Phonenumber = user.PhoneNumber,
                Firstname = user.Firstname,
                Surname = user.Surname,
                Address = user.Address,
                Residence = user.Residence,
                Postalcode = user.Postalcode,
                Housenumber = user.Housenumber,
                Claims = userClaims.Select(c => c.Value).ToList(),
                Roles = userRoles
            };

            return View(model);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProfile(UserViewModel userViewModel)
        {
            var user = await _userManager.FindByIdAsync(userViewModel.Id);

            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {userViewModel.Id} cannot be found";
                return NotFound();
            }

            user.Firstname = userViewModel.Firstname;
            user.Surname = userViewModel.Surname;
            user.Address = userViewModel.Address;
            user.Residence = userViewModel.Residence;
            user.Postalcode = userViewModel.Postalcode;
            user.Housenumber = userViewModel.Housenumber;
                
            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }



            // GetClaimsAsyc retunrs the list of user Claims
            var userClaims = await _userManager.GetClaimsAsync(user);
            // GetRolesAsync returns the list of user Roles
            var userRoles = await _userManager.GetRolesAsync(user);

            var model = new UserViewModel()
            {
                Id = user.Id,
                Email = user.Email,
                Firstname = user.Firstname,
                Surname = user.Surname,
                Address = user.Address,
                Residence = user.Residence,
                Postalcode = user.Postalcode,
                Housenumber = user.Housenumber,
                Claims = userClaims.Select(c => c.Value).ToList(),
                Roles = userRoles
            };

            return View(model);
        }

        public async Task<IActionResult> DeleteProfile(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {id} cannot be found";
                return NotFound();
            }

            var result = await _userManager.DeleteAsync(user);

            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View("Index");
        }
        // public IActionResult Index()
        // {
        //     var claimsIdentity = (ClaimsIdentity) User.Identity;
        //     var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
        //
        //     var userList = _unitOfWork.User.GetAll().ToList();
        //     var userRole = _db.UserRoles.ToList();
        //     var roles = _db.Roles.ToList();
        //
        //     foreach (var user in userList)
        //     {
        //         var roleId = userRole.FirstOrDefault(u => u.UserId == user.Id)?.RoleId;
        //         user.Role = roles.FirstOrDefault(u => u.Id == roleId)?.Name;
        //     }
        //     
        //     return View(userList);
        // }

        
        public IActionResult Lock(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            _unitOfWork.User.LockedUser(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Unlock(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            _unitOfWork.User.UnlockUser(id);
            return RedirectToAction(nameof(Index));
        }


        // public async Task<IActionResult> UpdateUser(string id)
        // {
        //     var user = _userManager.FindByIdAsync(id);
        //
        //     if (user == null)
        //     {
        //         return NotFound();
        //     }
        //     
        //     return View();
        // }
        // public async Task<IActionResult> Update(string Id)
        // {
        //     
        //     
        //     var user = _userManager.FindByIdAsync(Id);
        //
        //     
        //     user. = model.Firstname;
        //         // Firstname = user.Firstname,
        //         // Surname = user.Surname,
        //         // Address = user.Address,
        //         // Residence = user.Residence,
        //         // Postalcode = user.Postalcode,
        //         // Housenumber = user.Housenumber
        //
        //         return View(model);
        // }
        
        // public async Task<IActionResult> Update(string id)
        // {
        //     
        //     
        //     var user =  await  _userManager.GetUserAsync(User);
        //     if (user == null)
        //     {
        //         return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
        //     }
        //
        //     var hasPassword = await _userManager.HasPasswordAsync(user);
        //     if (!hasPassword)
        //     {
        //         return RedirectToPage("./SetPassword");
        //     }
        //
        //     return View();
        //     
        //     //var user = _unitOfWork.User.Get(id);
        //
        //     if (user != null)
        //     {
        //         _unitOfWork.User.
        //     }
        //     
        //     return 
        // }
        

        // [HttpPost]
        // public IActionResult GetAll()
        // {
        //     var userList = _unitOfWork.User.GetAll().ToList();
        //     var userRole = _db.UserRoles.ToList();
        //     var roles = _db.Roles.ToList();
        //
        //     foreach (var user in userList)
        //     {
        //         var roleId = userRole.FirstOrDefault(u => u.UserId == user.Id)?.RoleId;
        //         user.Role = roles.FirstOrDefault(u => u.Id == roleId)?.Name;
        //         
        //     }
        //
        //     return View(userList);
        // }
    }
}