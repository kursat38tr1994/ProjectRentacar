using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rentacar.Areas.Admin.ViewModels;
using Rentacar.Areas.Customer.ViewModels;
using Rentacar.BusinessLogic;
using Rentacar.BusinessLogic.Interface;
using Rentacar.DataAccess.Data.Repository.IRepository;
using Rentacar.DataAccess.Dto.CarDto;
using Rentacar.DataAccess.Dto.ShoppingCartDto;
using Rentacar.Extensions;
using Rentacar.Models;
using Rentacar.Utility;
using CarViewModel = Rentacar.Areas.Customer.ViewModels.CarViewModel;

namespace Rentacar.Controllers
{
    [Area("Customer")]
    public class CarController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICarLogic _carLogic;
        private readonly IMapper _mapper;
        //private readonly IShoppingCartLogic _shoppingCartLogic;

        [BindProperty] public ShoppingCartViewModel ShoppingCartViewModel { get; set; }

        public CarController(IUnitOfWork unitOfWork, ICarLogic carLogic, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _carLogic = carLogic;
            _mapper = mapper;
            //_shoppingCartLogic = shoppingCartLogic;
        }
        
        // GET
        public IActionResult Index()
        {
            IEnumerable<CarDto> cars = _carLogic.GetAll();
            var mapper = _mapper.Map<IEnumerable<CarViewModel>>(cars);
            
            var claims = (ClaimsIdentity) User.Identity;
            var claim = claims.FindFirst(ClaimTypes.NameIdentifier);
            
            if (claim != null)
            {
                var count = _unitOfWork.ShoppingCart.GetAll(c => c.UserId == claim.Value).ToList().Count();
                
                HttpContext.Session.SetInt32(Session.SessionCart, count);
            }
            return View(mapper);
        }
        
        public IActionResult Details(int? id)
        {

            var carFromDb = _unitOfWork.Car.GetFirstOrDefault(u => u.Id == id, includeProperties:"Brand,Fuel");

            ShoppingCartViewModel = new ShoppingCartViewModel()
            {
                Car = carFromDb,
                CarId = carFromDb.Id
            };
            //var carFromDb = _carLogic.GetFirstOfDefault(id);
            //  var mapper = _mapper.Map<ShoppingCartDto>(carFromDb);
            // IEnumerable<CarDto> cars = _carLogic.GetAll();
            // var mapper = _mapper.Map<IEnumerable<CarViewModel>>(cars);
            // return View(mapper);

            return View(_mapper.Map<ShoppingCartViewModel>(ShoppingCartViewModel));
        }
        

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult Details(ShoppingCart CartObject)
        {
            CartObject.Id = 0;
            if (ModelState.IsValid)
            {
                var claims = (ClaimsIdentity) User.Identity;
                var claim = claims.FindFirst(ClaimTypes.NameIdentifier);
                CartObject.UserId = claim.Value;


                var cartFromDb = _unitOfWork.ShoppingCart.GetFirstOrDefault(u =>
                    u.UserId == CartObject.UserId && u.CarId == CartObject.CarId, includeProperties:"Car");

                var mapper = _mapper.Map<ShoppingCart>(cartFromDb);
                ShoppingCart cartViewModel = mapper;

                if (cartViewModel == null)
                {
                    _unitOfWork.ShoppingCart.Add(_mapper.Map<ShoppingCart>(CartObject));
                }
                _unitOfWork.Save();

                var count = _unitOfWork.ShoppingCart.GetAll(c => c.UserId == CartObject.UserId).ToList().Count;
                
                HttpContext.Session.SetInt32(Session.SessionCart, count);
                
                return RedirectToAction(nameof(Index));
            }
            else
            {
                var carFromDb = _unitOfWork.Car.GetFirstOrDefault(u => u.Id == CartObject.CarId, includeProperties:"Brand,Fuel");

                ShoppingCart shoppingCart= new ShoppingCart()
                {
                    Car = carFromDb,
                    CarId = carFromDb.Id
                };

                return View(_mapper.Map<ShoppingCartViewModel>(shoppingCart));
            }
            
        }

    }
}