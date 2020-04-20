using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Rentacar.Models.ViewModels
{
    public class CarViewModel
    {
        public Car car { get; set; }
        public IEnumerable<SelectListItem> BrandList { get; set; }
    }
}
