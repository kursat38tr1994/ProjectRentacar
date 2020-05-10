using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Rentacar.DataAccess.Dto.FuelDto;
using Rentacar.Models;

namespace Rentacar.DataAccess.Profiles
{
    public class FuelProfile : Profile
    {
        public FuelProfile()
        {
            CreateMap<Fuel, FuelDto>();
            CreateMap<FuelDto, Fuel>();
        }
        
    }
}
