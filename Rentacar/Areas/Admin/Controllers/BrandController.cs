using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Rentacar.BusinessLogic;
using Rentacar.DataAccess.Data.Repository.IRepository;
using Rentacar.Models;

namespace Rentacar.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BrandController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        private IBrandLogic _brandLogic;

        public BrandController(IUnitOfWork unitOfWork, IMapper mapper, IBrandLogic brandLogic)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _brandLogic = brandLogic;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upsert(int? id) 
        {
            Brand brand = new Brand();
            if (id == null)
            {
                return View(brand);
            }
            brand = _unitOfWork.Brand.Get(id.GetValueOrDefault());

            if (brand == null) 
            {
                return NotFound();
            }
            return View(brand);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Brand brand) 
        {
            if (ModelState.IsValid) 
            {
                if (brand.Id == 0)
                {
                    _unitOfWork.Brand.Add(brand);
                }
                else
                {
                    _unitOfWork.Brand.Update(brand);
                }
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(brand);
        }

        #region Calls
        [HttpGet]
        public IActionResult GetAll() 
        {
            
            return Json(new { data = _brandLogic.GettAll() });
            //return Json(new { data = _unitOfWork.Brand.GetAll() });
        }

        [HttpDelete]
        public IActionResult Delete(int id) 
        {
            var ObjFromDb = _unitOfWork.Brand.Get(id);
            if (ObjFromDb == null)
            {
                return Json(new { success = false, message = "Probleem bij deleten" });
            }

            _unitOfWork.Brand.Remove(ObjFromDb);
            _unitOfWork.Save();
            return Json(new { success = true, message = $"Deleten is gelukt {ObjFromDb}" });
        }

        #endregion
    }
}