using System.Threading.Tasks;
using Rentacar.Models;

namespace Rentacar.BusinessLogic.IdentityLogic.Interfaces
{
    public interface ILoginLogic
    {
        Task<bool> Login(User model);
    }
}