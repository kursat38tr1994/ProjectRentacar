using AutoMapper;
using Rentacar.DataAccess.Dto.BrandDto;
using Rentacar.DataAccess.Dto.ShoppingCartDto;
using Rentacar.Models;

namespace Rentacar.DataAccess.Profiles
{
    public class ShoppingCartProfile : Profile
    {
        public ShoppingCartProfile()
        {
            CreateMap<ShoppingCart, ShoppingCartDto>();
            
            CreateMap<ShoppingCartDto, ShoppingCart>();
        }
    }
}