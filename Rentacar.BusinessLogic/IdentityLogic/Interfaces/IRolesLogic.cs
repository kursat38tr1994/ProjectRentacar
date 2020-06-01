using System.Threading.Tasks;
using Rentacar.DataAccess.Dto.UserDto;
using Rentacar.Models;

namespace Rentacar.BusinessLogic.IdentityLogic.Interfaces
{
    public interface IRolesLogic
    {
        void RolesCheck();

        Task<User> AddToRole(User user);
    }
}