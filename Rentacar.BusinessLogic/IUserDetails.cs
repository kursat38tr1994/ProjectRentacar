using Rentacar.DataAccess.Dto.UserDto;

namespace Rentacar.BusinessLogic
{
    public interface IUserDetails
    {
        UserDto GetFirstOfDefault(string id);
    }
}