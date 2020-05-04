using AutoMapper;
using Rentacar.DataAccess.Data.Dto.BrandDto;
using Rentacar.DataAccess.Data.Dto.CarDto;
using Rentacar.Models;

namespace Rentacar.DataAccess.Data.Profiles
{
    public class CarProfile : Profile
    {
        public CarProfile()
        {
            CreateMap<Car, CarDto>();

            CreateMap<CarDto, Car>();
        }
    }
}