using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rentacar.Areas.Admin.ViewModels;
using Rentacar.BusinessLogic.Interface;
using Rentacar.DataAccess.Dto.FuelDto;
using Rentacar.Utility;

namespace Rentacar.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = Roles.Admin)]
    public class FuelController : Controller
    {
        private readonly IMapper _iMapper;
        private readonly IFuelLogic _fuelLogic;


        public FuelController(IMapper iMapper, IFuelLogic fuelLogic)
        {
            _iMapper = iMapper;
            _fuelLogic = fuelLogic;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upsert(int? id)
        {
           var viewModel = new FuelViewModel();

           if (id == null)
           {
               return View(viewModel);
           }

           var obj = _fuelLogic.GetById(id);
           if (obj == null)
           {
               return NotFound();
           }

           return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(FuelViewModel viewModel)
        {
            var mapper = _iMapper.Map<FuelDto>(viewModel);
            if (ModelState.IsValid)
            {
                _fuelLogic.Upsert(mapper);
            }
            return View(viewModel);
        }

        public IActionResult GetAll()
        {
            return Json(new {data = _fuelLogic.GetAll()});
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var obj = _fuelLogic.GetById(id);

            if (obj == null)
            {
                return Json(new { success = false, message = "Probleem bij deleten" });
            }

            _fuelLogic.Delete(obj.Id);

            return Json(new { success = true, message = $"Deleten is gelukt {obj}" });
        }
    }
}