using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;
using Rentacar.DataAccess.Dto.CarDto;

namespace Rentacar.Areas.Admin.ViewModels
{
    public class CarViewModel
    {
        public int Id { get; set; }
        
        public int BrandId { get; set; }

        public string LicensePlate { get; set; }

        public int FuelId { get; set; }
        
        public double CurrentPrice { get; set; }
        
        public bool Availability { get; set; }
        public CarDto Car { get; set; }
        public IEnumerable<SelectListItem> BrandList { get; set; }
        public IEnumerable<SelectListItem> FuelList { get; set; }
    }
}
