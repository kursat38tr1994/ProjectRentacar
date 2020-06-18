using System.Collections.Generic;
using Rentacar.DataAccess.Dto.RentDetailsDto;
using Rentacar.DataAccess.Dto.RentDto;

namespace Rentacar.BusinessLogic.Order
{
    public interface IOrderLogic
    {
        RentDto GetAllRent(int id);
        IEnumerable<RentDetailsDto> GettAllRentDetails(int id);
        IEnumerable<RentDto> UserRentData(string id);

        IEnumerable<RentDto> AdminRentData();
    }
}