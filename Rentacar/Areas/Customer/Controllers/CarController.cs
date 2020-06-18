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

        private readonly IBrandLogic _brandLogic;
        //private readonly IShoppingCartLogic _shoppingCartLogic;

        [BindProperty] private ShoppingCartViewModel ShoppingCartViewModel { get; set; }

        public CarController(IUnitOfWork unitOfWork, ICarLogic carLogic, IMapper mapper, IBrandLogic brandLogic)
        {
            _unitOfWork = unitOfWork;
            _carLogic = carLogic;
            _mapper = mapper;
            _brandLogic = brandLogic;
            //_shoppingCartLogic = shoppingCartLogic;
        }
        
        // GET
        public IActionResult Index()
        {
            
            var model = new CarViewModel()
            {
                Cars = _carLogic.GetAll(),
                Brands = _brandLogic.GettAll()
            };
            
            return View(model);
        }
        
        public IActionResult Details(int? id)
        {
            var carFromDb = _carLogic.GetFirstOfDefault(id);
            return View(_mapper.Map<CarViewModel>(carFromDb));
        }

        public IActionResult AddToCart(int carId)
        {
            var sessionList = new List<int>();

            if (string.IsNullOrEmpty(HttpContext.Session.GetString(Session.SessionCart)))
            {
                sessionList.Add(carId);
                HttpContext.Session.SetObject(Session.SessionCart, sessionList);
            }
            else
            {
                sessionList = HttpContext.Session.GetObject<List<int>>(Session.SessionCart);
                if (!sessionList.Contains(carId))
                {
                    sessionList.Add(carId);
                    HttpContext.Session.SetObject(Session.SessionCart, sessionList);
                }
            }
            return RedirectToAction(nameof(Index));
        }

    }
}