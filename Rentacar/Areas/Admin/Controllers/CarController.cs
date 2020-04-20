using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Rentacar.DataAccess.Data.Repository.IRepository;
using Rentacar.Models.ViewModels;


namespace Rentacar.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CarController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        [BindProperty]
        public CarViewModel CarViewModel { get; set; }

        public CarController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upsert(int? id)
        {
            CarViewModel = new CarViewModel()
            {
                car = new Models.Car(),
                BrandList = _unitOfWork.Brand.GetBrandListItemsForDropDown()
            };

            if (id != null)
            {
                CarViewModel.car = _unitOfWork.Car.Get(id.GetValueOrDefault());
            }

            return View(CarViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert()
        {
            if (ModelState.IsValid)
            {
                if (CarViewModel.car.Id == 0)
                {
                    _unitOfWork.Car.Add(CarViewModel.car);
                }
                else
                {
                    _unitOfWork.Car.Update(CarViewModel.car);
                }
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }

            CarViewModel.BrandList = _unitOfWork.Brand.GetBrandListItemsForDropDown();
            return View(CarViewModel);

        }

        #region Calls
        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(new { data = _unitOfWork.Car.GetAll(includeProperties: "Brand" ) });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var carFromDb = _unitOfWork.Car.Get(id);
            if (carFromDb == null)
            {
                return Json(new { success = false, message = "Probleem bij deleten" });
            }

            _unitOfWork.Brand.Remove(id);
            _unitOfWork.Save();
            return Json(new { success = true, message = $"Deleten is gelukt {id}" });
        }

        #endregion
    }
}
