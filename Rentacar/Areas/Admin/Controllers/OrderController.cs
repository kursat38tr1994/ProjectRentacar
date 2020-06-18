using System.Collections;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rentacar.Areas.Admin.ViewModels;
using Rentacar.BusinessLogic;
using Rentacar.BusinessLogic.Order;
using Rentacar.DataAccess.Data.Repository.IRepository;
using Rentacar.DataAccess.Dto.RentDto;
using Rentacar.Models;
using Rentacar.Utility;

namespace Rentacar.Areas.Customer.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IOrderLogic _orderLogic;
        private readonly IUserDetails _userDetails;

        [BindProperty] private OrderViewModel OrderViewModel { get; set; }

        public OrderController(IOrderLogic orderLogic , IUserDetails userDetails)
        {
            _orderLogic = orderLogic;
            _userDetails = userDetails;
        }
        
        
        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Details(int id)
        {
            OrderViewModel = new OrderViewModel()
            {
                Rent = _orderLogic.GetAllRent(id),
                RentDetails = _orderLogic.GettAllRentDetails(id)
            };
            
            return View(OrderViewModel);
        }
        
        [HttpGet]
        public IActionResult GetOrder()
        {
            var claimsIdentity = (ClaimsIdentity) User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            
            IEnumerable<RentDto> rent;
            
            if (User.IsInRole(Roles.Admin) || User.IsInRole(Roles.Employee))
            {
                rent = _orderLogic.AdminRentData();
            }
            else
            {
                rent = _orderLogic.UserRentData(claim.Value);
            }

            return Json(new {data = rent});
        }
    }
}
