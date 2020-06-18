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
    [Authorize(Roles = Roles.Admin)]
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
                //Claims = userClaims.Select(c => c.Value).ToList(),
                //Roles = userRoles
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
                //Claims = userClaims.Select(c => c.Value).ToList(),
                //Roles = userRoles

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
    }
}