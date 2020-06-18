using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Rentacar.Areas.Customer.ViewModels;
using Rentacar.BusinessLogic;
using Rentacar.BusinessLogic.Rent;
using Rentacar.DataAccess.Data.Repository.IRepository;
using Rentacar.DataAccess.Dto.CarDto;
using Rentacar.DataAccess.Dto.RentDetailsDto;
using Rentacar.DataAccess.Dto.RentDto;
using Rentacar.DataAccess.Dto.UserDto;
using Rentacar.Extensions;
using Rentacar.Models;
using Rentacar.Utility;

namespace Rentacar.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class CartController : Controller
    {
        private readonly ICarLogic _carLogic;
        private readonly IMapper _mapper;
        private readonly IUserDetails _userDetails;
        private readonly IRentDetailsLogic _rentDetailsLogic;
        private readonly IRentLogic _rentLogic;

        public CartController( ICarLogic carLogic, IUnitOfWork unitOfWork,
            IMapper mapper, IUserDetails userDetails, IRentDetailsLogic rentDetailsLogic, IRentLogic rentLogic)
        {
       
            _carLogic = carLogic;
            _mapper = mapper;
            _userDetails = userDetails;
            _rentDetailsLogic = rentDetailsLogic;
            _rentLogic = rentLogic;

            ShoppingCartViewModel = new ShoppingCartViewModel
            {
                ListCar = new List<CarDto>(),
                Rent = new RentDto()
            };
        }

        [BindProperty] private ShoppingCartViewModel ShoppingCartViewModel { get; set; }


        public List<int> HttpSession()
        {
            var sessions = HttpContext.Session.GetObject<List<int>>(Session.SessionCart);
            if (HttpContext.Session.GetObject<List<int>>(Session.SessionCart) != null)
            {
                foreach (var carId in sessions)
                {
                    var getCarFromDb = _carLogic.GetFirstOfDefault(carId);
                    var mapperToCar = _mapper.Map<CarDto>(getCarFromDb);

                    ShoppingCartViewModel.ListCar.Add(mapperToCar);
                }
            }

            return sessions;
        }

        public Claim GetUserClaim()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            

            return claim;
        }

        public IActionResult Index()
        {


            HttpSession();

            return View(ShoppingCartViewModel);
        }

        public IActionResult Remove(int cartId)
        {
            var sessions = HttpContext.Session.GetObject<List<int>>(Session.SessionCart);
            sessions.Remove(cartId);
            HttpContext.Session.SetObject(Session.SessionCart, sessions);

            return RedirectToAction(nameof(Index));
        }
        

        [Authorize]
        public IActionResult Summary()
        {
            var userClaim = GetUserClaim();
            ShoppingCartViewModel.User = _userDetails.GetFirstOfDefault(userClaim.Value);

            HttpSession();

            return View(ShoppingCartViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Summary(ShoppingCartViewModel model)
        {
            ShoppingCartViewModel = new ShoppingCartViewModel
            {
                ListCar = new List<CarDto>(),
                Rent = new RentDto(),
                
            };
            
            HttpSession();

            if (ModelState.IsValid)
            {
                foreach (var car in ShoppingCartViewModel.ListCar)
                {
                    ShoppingCartViewModel.Rent.Total += car.CurrentPrice;
                }

                ShoppingCartViewModel.Rent.RentDate = model.RentDate;
                ShoppingCartViewModel.Rent.ReturnDate = model.ReturnDate;
                ShoppingCartViewModel.Rent.UserId = GetUserClaim().Value;
                
                _rentLogic.AddRent(ShoppingCartViewModel.Rent);

                foreach (var car in ShoppingCartViewModel.ListCar)
                {
                    ShoppingCartViewModel.Rent.Total += car.CurrentPrice;
                    
                    var rentDetails = new RentDetailsDto
                    {
                        CarId = car.Id,
                        RentId = ShoppingCartViewModel.Rent.Id,
                        Price = car.CurrentPrice
                    };

                    _rentDetailsLogic.AddDetail(rentDetails);

                    var carDetails = new CarDto
                    {
                        Id = car.Id,
                        Brand = car.Brand,
                        BrandId = car.BrandId,
                        CurrentPrice = car.CurrentPrice,
                        Description = car.Description,
                        Fuel = car.Fuel,
                        FuelId = car.FuelId,
                        ImageUrl = car.ImageUrl,
                        LicensePlate = car.LicensePlate,
                        Title = car.Title,
                        Availability = false
                    };

                    _carLogic.ChangeAvailability(carDetails);
                    
                }
                return RedirectToAction("OrderConfirmation", "Cart", new { id = ShoppingCartViewModel.Rent.Id });
            }
            return View(ShoppingCartViewModel);
        }

        public IActionResult OrderConfirmation(int id)
        {
            return View(id);
        }
    }
}