using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Rentacar.Areas.Admin.ViewModels;
using Rentacar.BusinessLogic;
using Rentacar.BusinessLogic.Interface;
using Rentacar.DataAccess.Data.Repository.IRepository;
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
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IUnitOfWork _unitOfWork;

        [BindProperty] public CarViewModel CarViewModel { get; set; }

        public CarController(ICarLogic carLogic, IBrandLogic brandLogic, IMapper mapper, IFuelLogic fuelLogic,
            IWebHostEnvironment webHostEnvironment, IUnitOfWork unitOfWork)
        {
            _carLogic = carLogic;
            _brandLogic = brandLogic;
            _mapper = mapper;
            _fuelLogic = fuelLogic;
            _webHostEnvironment = webHostEnvironment;
            _unitOfWork = unitOfWork;
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
                FuelList = _fuelLogic.GetDropDown()
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
            if (ModelState.IsValid)
            {
                string webRootPath = _webHostEnvironment.WebRootPath;
                var files = HttpContext.Request.Form.Files;
                if (files.Count > 0)
                {
                    string filename = Guid.NewGuid().ToString();
                    string uploadDirectory = Path.Combine(webRootPath, @"images\cars");
                    var extension = Path.GetExtension(files[0].FileName);

                    if (carViewModel.ImageUrl != null)
                    {
                        var imagePath = Path.Combine(webRootPath, carViewModel.ImageUrl.TrimStart('\\'));

                        if (System.IO.File.Exists(imagePath))
                        {
                            System.IO.File.Delete(imagePath);
                        }
                    }

                    using (var filesStream = new FileStream(Path.Combine(uploadDirectory, filename + extension), FileMode.Create))
                    {
                        files[0].CopyTo(filesStream);
                    }

                    carViewModel.ImageUrl = @"\images\cars\"+filename+extension;
                }
                else
                {
                    if (carViewModel.Id != 0)
                    {
                        var objFromDb = _unitOfWork.Car.Get(carViewModel.Id);
                        carViewModel.ImageUrl = objFromDb.ImageUrl;
                    }
                }

                
                _carLogic.Upsert(_mapper.Map<CarDto>(carViewModel)); 
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