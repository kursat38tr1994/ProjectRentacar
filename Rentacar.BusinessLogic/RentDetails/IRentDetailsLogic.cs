using Rentacar.DataAccess.Dto.RentDetailsDto;

namespace Rentacar.BusinessLogic.Rent
{
    public interface IRentDetailsLogic
    {
        RentDetailsDto AddDetail(RentDetailsDto model);
    }
}