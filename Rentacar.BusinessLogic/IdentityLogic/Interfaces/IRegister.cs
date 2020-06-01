using System.Threading.Tasks;
using Rentacar.DataAccess.Dto.UserDto;

namespace Rentacar.BusinessLogic.IdentityLogic
{
    public interface IRegister
    {
        Task<UserDto> SignUp(UserDto model);
    }
}