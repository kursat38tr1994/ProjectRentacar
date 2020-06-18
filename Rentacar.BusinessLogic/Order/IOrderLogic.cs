using Rentacar.DataAccess.Dto.RentDetailsDto;
using Rentacar.DataAccess.Dto.RentDto;

namespace Rentacar.BusinessLogic.Order
{
    public interface IOrderLogic
    {
        RentDto GetAllRent(int id);
        RentDetailsDto GettAllRentDetails(int id);
    }
}