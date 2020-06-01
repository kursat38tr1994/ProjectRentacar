using System.Collections.Generic;
using Rentacar.DataAccess.Dto.CarDto;

namespace Rentacar.BusinessLogic
{
    public interface ICarLogic
    {
        IEnumerable<CarDto> GetAll();

        CarDto GetId(int? id);

        CarDto Upsert(CarDto carDto);

        CarDto Delete(int? id);
        CarDto GetFirstOfDefault(int? id);
    }
}