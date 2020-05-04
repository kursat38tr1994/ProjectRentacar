using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Rentacar.BusinessLogic;
using Rentacar.DataAccess.Data.Dto.BrandDto;
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
            var brand = new ReadBrandDto();
           // Brand brand = new Brand();
            if (id == null)
            {
                return View(brand);
            }

            brand = _brandLogic.GetId(id);
        
            if (brand == null) 
            {
                return NotFound();
            }
            return View(brand);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(ReadBrandDto brand) 
        {
            if (ModelState.IsValid)
            {
                _brandLogic.Upsert(brand);
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