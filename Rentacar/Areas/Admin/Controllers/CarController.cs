using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rentacar.Areas.Admin.ViewModels;
using Rentacar.BusinessLogic;
using Rentacar.BusinessLogic.Interface;
using Rentacar.DataAccess.Dto.CarDto;
using Rentacar.Utility;


namespace Rentacar.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = Roles.Admin)]
    public class CarController : Controller
    {
        private readonly ICarLogic _carLogic;
        private readonly IBrandLogic _brandLogic;
        private readonly IMapper _mapper;
        private readonly IFuelLogic _fuelLogic;

        [BindProperty] 
        public CarViewModel CarViewModel { get; set; }

        public CarController(ICarLogic carLogic, IBrandLogic brandLogic, IMapper mapper, IFuelLogic fuelLogic)
        {
            _carLogic = carLogic;
            _brandLogic = brandLogic;
            _mapper = mapper;
            _fuelLogic = fuelLogic;
        }

        public IActionResult Index()
        {
            var model = new CarViewModel();
            
            return View(model);
        }

        public IActionResult Upsert(int? id)
        {
            CarViewModel = new CarViewModel
            {
                Car = new CarDto(),
                BrandList = _brandLogic.GetDropDown(),
                FuelList = _fuelLogic.GetDropDown(),
            };

            if (id != null)
            {
                CarViewModel.Car = _carLogic.GetId(id);
            }

            return View(CarViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(CarViewModel carViewModel)
        {
         //   CarViewModel = _mapper.Map<CarViewModel>(CarViewModel);
         if (ModelState.IsValid)
         {
             _carLogic.Upsert(_mapper.Map<CarDto>(CarViewModel));
             return RedirectToAction(nameof(Index));
         }

         CarViewModel.BrandList = _brandLogic.GetDropDown();
         CarViewModel.FuelList = _brandLogic.GetDropDown();

         return View(CarViewModel);
        }

        #region Calls

        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(new {data = _carLogic.GetAll()});
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var carFromDb = _carLogic.GetId(id);
            if (carFromDb == null)
            {
                return Json(new {success = false, message = "Probleem bij deleten"});
            }

            _carLogic.Delete(carFromDb.Id);
            return Json(new {success = true, message = $"Deleten is gelukt {id}"});
        }

        #endregion
    }
}