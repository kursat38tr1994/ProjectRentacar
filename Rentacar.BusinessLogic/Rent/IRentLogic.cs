using Rentacar.DataAccess.Dto.RentDto;

namespace Rentacar.BusinessLogic.Rent
{
    public interface IRentLogic
    {
        RentDto AddRent(RentDto model);
        RentDto GetFirstOfDefault(RentDto model);
    }
}