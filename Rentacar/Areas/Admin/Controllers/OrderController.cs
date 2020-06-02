using System.Collections;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rentacar.Areas.Admin.ViewModels;
using Rentacar.DataAccess.Data.Repository.IRepository;
using Rentacar.Models;
using Rentacar.Utility;

namespace Rentacar.Areas.Customer.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        [BindProperty] public OrderViewModel OrderViewModel { get; set; }

        public OrderController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        // GET
        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Details(int id)
        {
            OrderViewModel = new OrderViewModel()
            {
                Rent = _unitOfWork.Rent.GetFirstOrDefault(u => u.Id == id, includeProperties: "User"),
                OrderDetails = _unitOfWork.RentDetails.GetAll(o=> o.Id == id, includeProperties:"Car")
            };
            
            return View(OrderViewModel);
        }
        
        [HttpGet]
        public IActionResult GetOrder()
        {
            var claimsIdentity = (ClaimsIdentity) User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            IEnumerable<Rent> rent;
            
            if (User.IsInRole(Roles.Admin) || User.IsInRole(Roles.Employee))
            {
                rent = _unitOfWork.Rent.GetAll(includeProperties:"User");
            }
            else
            {
                rent = _unitOfWork.Rent.GetAll(u=> u.UserId == claim.Value, includeProperties:"User");    
            }

            return Json(new {data = rent});

        }
        
        
        
    }
}