using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Rentacar.Areas.Admin.ViewModels;
using Rentacar.Models;
using UserViewModel = Rentacar.Areas.Customer.ViewModels.UserViewModel;

namespace Rentacar.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class CustomerUserController : Controller
    {
        private readonly UserManager<User> _userManager;
        
        public CustomerUserController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }
    
        [HttpGet]
        public async  Task<IActionResult> ChangeProfile(string id)
        {
            var user = await _userManager.GetUserAsync(User);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {id} cannot be found";
                return NotFound();
            }

            // GetClaimsAsync retunrs the list of user Claims

            var model = new UserViewModel()
            {
                Id = user.Id,
                Email = user.Email,
                Phonenumber = phoneNumber,
                Firstname = user.Firstname,
                Surname = user.Surname,
                Address = user.Address,
                Residence = user.Residence,
                Postalcode = user.Postalcode,
                Housenumber = user.Housenumber,
            };

            return View(model);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeProfile(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                
                var user = await _userManager.GetUserAsync(User);

                if (user == null)
                {
                    return RedirectToAction("SignIn", "Identity");
                }

                user.PhoneNumber = model.Phonenumber;
                user.Firstname = model.Firstname;
                user.Surname = model.Surname;
                user.Address = model.Address;
                user.Residence = model.Residence;
                user.Postalcode = model.Postalcode;
                user.Housenumber = model.Housenumber;
                
                var result = await _userManager.UpdateAsync(user);
        
                if (result.Succeeded)
                {
                    return RedirectToAction("ChangeProfile");
                }
        
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                
            }
            
            return View(model);
        }
    }
}