using AutoMapper;
using Rentacar.DataAccess.Dto.RentDetailsDto;
using Rentacar.Models;

namespace Rentacar.DataAccess.Profiles
{
    public class RentDetailsProfile : Profile
    {
        public RentDetailsProfile()
        {
            CreateMap<RentDetailsDto, RentDetails>();
            CreateMap<RentDetails, RentDetailsDto>();
        }
    }
}