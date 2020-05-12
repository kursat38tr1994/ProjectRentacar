using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rentacar.Areas.Admin.ViewModels;
using Rentacar.BusinessLogic;
using Rentacar.DataAccess.Data.Repository.IRepository;
using Rentacar.DataAccess.Dto.BrandDto;
using Rentacar.Models;
using Rentacar.Utility;

namespace Rentacar.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Admin)]
    public class BrandController : Controller
    {
        private IBrandLogic _brandLogic;
        private readonly IMapper _mapper;

        public BrandController(IBrandLogic brandLogic, IMapper mapper)
        {
            _brandLogic = brandLogic;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upsert(int? id)
        {
            var brand = new BrandViewModel();
            if (id == null)
            {
                return View(brand);
            }

            var obj = _brandLogic.GetId(id);
        
            if (obj == null) 
            {
                return NotFound();
            }
            return View(brand);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(BrandViewModel brand) 
        {
            if (ModelState.IsValid)
            {
                var mapper = _mapper.Map<BrandDto>(brand);
                _brandLogic.Upsert(mapper);
            }
            return View(brand);
        }

        #region Calls
        [HttpGet]
        public ActionResult GetAll()
        {
            return Json(new {data = _brandLogic.GettAll()});
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var obj =_brandLogic.GetId(id);

            if (obj == null)
            {
                return Json(new { success = false, message = "Probleem bij deleten" });
            }
            
            _brandLogic.Delete(obj.Id);

            return Json(new {success = true, message = $"Deleten is gelukt {obj}"});
        }
        #endregion
    }
}