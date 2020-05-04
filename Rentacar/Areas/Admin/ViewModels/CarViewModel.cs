using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;
using Rentacar.DataAccess.Data.Dto.CarDto;

namespace Rentacar.Areas.Admin.ViewModels
{
    public class CarViewModel
    {
        public CarDto Car { get; set; }
        public IEnumerable<SelectListItem> BrandList { get; set; }
    }
}
