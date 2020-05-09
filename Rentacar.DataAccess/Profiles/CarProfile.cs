using AutoMapper;
using Rentacar.DataAccess.Dto.CarDto;
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