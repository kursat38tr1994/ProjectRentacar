using Rentacar.Areas.Admin.ViewModels;
using Rentacar.Areas.Customer.ViewModels;
using Rentacar.DataAccess.Dto.CarDto;
using Rentacar.DataAccess.Dto.FuelDto;
using Rentacar.Models;

namespace Rentacar.Areas.Admin.Profile
{
    public class ShoppingCartProfile : AutoMapper.Profile
    {
        public ShoppingCartProfile()
        {
            CreateMap<ShoppingCartViewModel, Car>();
            CreateMap<Car, ShoppingCartViewModel>();
            
            CreateMap<ShoppingCartViewModel, ShoppingCart>();
            CreateMap<ShoppingCart, ShoppingCartViewModel>();
        }
    }
}