using System.Collections.Generic;
using Rentacar.DataAccess.Data.Dto.CarDto;

namespace Rentacar.BusinessLogic
{
    public interface ICarLogic
    {
        IEnumerable<CarDto> GetAll();

        CarDto GetId(int? id);

        CarDto Upsert(CarDto carDto);

        CarDto Delete(int? id);
    }
}