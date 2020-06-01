using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Rentacar.Areas.Admin.ViewModels;
using Rentacar.DataAccess.Dto.CarDto;
using Rentacar.Models;

namespace Rentacar.Areas.Admin.Profile
{
    public class CarProfile : AutoMapper.Profile
    {
        public CarProfile()
        {
            //Admin Area
            CreateMap<CarViewModel, CarDto>();
            CreateMap<CarDto, CarViewModel>();
            
            //Customer Area
            CreateMap<Customer.ViewModels.CarViewModel, CarDto>();
            CreateMap<CarDto, Customer.ViewModels.CarViewModel>();
            
        }
    }
}
