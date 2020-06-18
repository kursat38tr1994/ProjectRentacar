using AutoMapper;
using Rentacar.DataAccess.Dto.RentDetailsDto;
using Rentacar.DataAccess.Dto.RentDto;
using Rentacar.Models;

namespace Rentacar.DataAccess.Profiles
{
    public class RentProfile : Profile
    {

        public RentProfile()
        {
            CreateMap<Rent, RentDto>();
            CreateMap<RentDto, Rent>();   
        }
        
    }
}