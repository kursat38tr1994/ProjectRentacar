using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Rentacar.Areas.Customer.ViewModels;
using Rentacar.DataAccess.Data.Repository.IRepository;
using Rentacar.Models;
using Rentacar.Utility;

namespace Rentacar.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class CartController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<User> _userManager;

        [BindProperty] 
        public ShoppingCartViewModel ShoppingCartViewModel { get; set; }

        public CartController(IUnitOfWork unitOfWork, UserManager<User> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var claimsIdentity = (ClaimsIdentity) User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            
            ShoppingCartViewModel = new ShoppingCartViewModel()
            {
               Rent = new Rent(),
               ListCart = _unitOfWork.ShoppingCart.GetAll(u=> u.UserId == claim.Value, includeProperties:"Car"),
            };
            
            ShoppingCartViewModel.Rent.Total = 0;
            ShoppingCartViewModel.Rent.User = _unitOfWork.User.GetFirstOrDefault(u => u.Id == claim.Value);

            foreach (var list in ShoppingCartViewModel.ListCart)
            {
                list.Price = list.Car.CurrentPrice;
                ShoppingCartViewModel.Rent.Total += list.Price;
            }
            
            return View(ShoppingCartViewModel);
        }

        public IActionResult Remove(int cartId)
        {
            var cart = _unitOfWork.ShoppingCart.GetFirstOrDefault(c => c.Id == cartId, includeProperties: "Car");

            var cnt = _unitOfWork.ShoppingCart.GetAll(u => u.UserId == cart.UserId).ToList().Count;
            
            _unitOfWork.ShoppingCart.Remove(cart);
            _unitOfWork.Save();
            HttpContext.Session.SetInt32(SD.SessionCart, cnt - 1);

            return RedirectToAction(nameof(Index));
        }
        
        public IActionResult Summary()
        {

            var claimsIdentity = (ClaimsIdentity) User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            ShoppingCartViewModel = new ShoppingCartViewModel()
            {
                Rent = new Rent(),
                ListCart = _unitOfWork.ShoppingCart.GetAll(c=> c.UserId == claim.Value, includeProperties:"Car")
            };
            
            ShoppingCartViewModel.Rent.User = _unitOfWork.User.GetFirstOrDefault(c => c.Id == claim.Value);
            
            foreach (var list in ShoppingCartViewModel.ListCart)
            {
                list.Price = list.Car.CurrentPrice;
                ShoppingCartViewModel.Rent.Total += list.Price;
            }

            return View(ShoppingCartViewModel);
        }
        
        [HttpPost]
        [ActionName("Summary")]
        [ValidateAntiForgeryToken]
        public IActionResult SummaryPost()
        {
            var claimsIdentity = (ClaimsIdentity) User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            
            ShoppingCartViewModel.Rent.User = _unitOfWork.User.GetFirstOrDefault(c => c.Id == claim.Value);

            ShoppingCartViewModel.ListCart = _unitOfWork.ShoppingCart.GetAll(c => c.UserId == claim.Value, includeProperties:"Car");

            ShoppingCartViewModel.UserId = claim.Value;
            //ShoppingCartViewModel.Rent.Total = ShoppingCartViewModel.Rent.Total;
            
            _unitOfWork.Rent.Add(ShoppingCartViewModel.Rent);
            _unitOfWork.Save();
            
            foreach (var item in ShoppingCartViewModel.ListCart)
            {
                RentDetails rent = new RentDetails()
                {
                    CarId = item.CarId,
                    RentId = ShoppingCartViewModel.Rent.Id,
                    Price = item.Price
                };
                ShoppingCartViewModel.Rent.Total += rent.Price;
                _unitOfWork.RentDetails.Add(rent);
                _unitOfWork.Save();
            }

            _unitOfWork.ShoppingCart.RemoveRange(ShoppingCartViewModel.ListCart);
            _unitOfWork.Save();
            
            HttpContext.Session.SetInt32(SD.SessionCart, 0);

            return RedirectToAction("OrderConfirmation", "Cart", new {id = ShoppingCartViewModel.Rent.Id});
        }

        public IActionResult OrderConfirmation(int id)
        {
            return View(id);
        }
        
    }
}