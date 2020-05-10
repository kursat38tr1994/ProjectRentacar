using System.Collections.Generic;
using Rentacar.DataAccess.Dto.FuelDto;

namespace Rentacar.BusinessLogic.Interface
{
    public interface IFuelLogic
    {
        FuelDto Upsert(FuelDto createFuel);
        FuelDto GetById(int? id);
        IEnumerable<FuelDto> GetAll();
    }
}