using AutoMapper;
using Rentacar.DataAccess.Dto.BrandDto;
using Rentacar.Models;

namespace Rentacar.DataAccess.Profiles
{
    public class BrandProfile : Profile
    {
        public BrandProfile()
        {
            CreateMap<Brand, BrandDto>();
            
            CreateMap<BrandDto, Brand>();
        }
    }
}