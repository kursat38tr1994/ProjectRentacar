using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rentacar.DataAccess;
using Rentacar.DataAccess.Data.Repository.IRepository;
using Rentacar.Utility;

namespace Rentacar.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Admin)]
    public class UserController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ApplicationDbContext _db;

        public UserController(IUnitOfWork unitOfWork, ApplicationDbContext db)
        {
            _unitOfWork = unitOfWork;
            _db = db;
        }
  
        public IActionResult Index()
        {
            var claimsIdentity = (ClaimsIdentity) User.Identity;
            var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            var userList = _unitOfWork.User.GetAll().ToList();
            var userRole = _db.UserRoles.ToList();
            var roles = _db.Roles.ToList();

            foreach (var user in userList)
            {
                var roleId = userRole.FirstOrDefault(u => u.UserId == user.Id)?.RoleId;
                user.Role = roles.FirstOrDefault(u => u.Id == roleId)?.Name;
                
            }
            
            return View(userList);
        }

        
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