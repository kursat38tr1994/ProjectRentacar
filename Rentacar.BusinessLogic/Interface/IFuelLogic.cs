using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Rentacar.DataAccess.Dto.FuelDto;

namespace Rentacar.BusinessLogic.Interface
{
    public interface IFuelLogic
    {
        FuelDto Upsert(FuelDto createFuel);
        FuelDto GetById(int? id);
        IEnumerable<FuelDto> GetAll();
        IEnumerable<SelectListItem> GetDropDown();
    }
}