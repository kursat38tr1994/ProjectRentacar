
using System.Collections.Generic;
using Microsoft.VisualBasic;
using Rentacar.DataAccess.Dto.BrandDto;
using Rentacar.DataAccess.Dto.CarDto;
using Rentacar.Models;

namespace Rentacar.Areas.Customer.ViewModels
{
    public class CarViewModel
    {
        public IEnumerable<CarDto> Cars { get; set; }

        public IEnumerable<BrandDto> Brands { get; set; }

        public int Id { get; set; }
        
        public string Title { get; set; }
        
        public string Description { get; set; }
        
        public int BrandId { get; set; }

        public Brand Brand { get; set; }
        
        public string LicensePlate { get; set; }
        
        public int FuelId { get; set; }
        
        public double CurrentPrice { get; set; }
        
        public bool Availability { get; set; }
        
        public string ImageUrl { get; set; }

        public DateAndTime StartDate { get; set; }

        public DateAndTime EndDate { get; set; }

    }
}