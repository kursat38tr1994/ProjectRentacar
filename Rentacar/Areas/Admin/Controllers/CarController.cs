using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Rentacar.Areas.Admin.ViewModels;
using Rentacar.BusinessLogic;
using Rentacar.DataAccess.Dto.CarDto;


namespace Rentacar.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CarController : Controller
    {
        private readonly ICarLogic _carLogic;
        private readonly IBrandLogic _brandLogic;
        private readonly IMapper _mapper;

        [BindProperty] 
        public CarViewModel CarViewModel { get; set; }

        public CarController(ICarLogic carLogic, IBrandLogic brandLogic, IMapper mapper)
        {
            _carLogic = carLogic;
            _brandLogic = brandLogic;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upsert(int? id)
        {
            var obj = _carLogic.GetId(id);
            var objMapper = _mapper.Map<CarViewModel>(obj);
            CarViewModel = new CarViewModel()
            {
                Car = new CarDto(),
                BrandList = _brandLogic.GetDropDown()
            };

            if (id != null)
            {
                CarViewModel = objMapper;
            }

            return View(CarViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert()
        {
            if (ModelState.IsValid)
            {
                var mapper = _mapper.Map<CarDto>(CarViewModel);
                _carLogic.Upsert(mapper);
                return RedirectToAction(nameof(Index));
            }

            CarViewModel.BrandList = _brandLogic.GetDropDown();
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