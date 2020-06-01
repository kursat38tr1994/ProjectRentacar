using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Rentacar.Areas.Admin.ViewModels;
using Rentacar.DataAccess.Dto.FuelDto;

namespace Rentacar.Areas.Admin.Profile
{
    public class FuelProfile : AutoMapper.Profile
    {
        public FuelProfile()
        {
            CreateMap<FuelViewModel, FuelDto>();
            CreateMap<FuelDto, FuelViewModel>();
        }
    }
}
