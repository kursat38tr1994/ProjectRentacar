using Rentacar.Areas.Admin.ViewModels;
using Rentacar.DataAccess.Dto.BrandDto;

namespace Rentacar.Areas.Admin.Profile
{
    public class BrandProfile : AutoMapper.Profile
    {
        public BrandProfile()
        {
            CreateMap<BrandViewModel, BrandDto>();
            CreateMap<BrandDto, BrandViewModel>();
        }
    }
}
