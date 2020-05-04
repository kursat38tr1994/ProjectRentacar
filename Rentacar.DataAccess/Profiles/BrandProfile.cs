using AutoMapper;
using Rentacar.DataAccess.Data.Dto.BrandDto;
using Rentacar.Models;

namespace Rentacar.DataAccess.Data.Profiles
{
    public class BrandProfile : Profile
    {
        public BrandProfile()
        {
            CreateMap<Brand, ReadBrandDto>();
            
            CreateMap<ReadBrandDto, Brand>();
        }
    }
}