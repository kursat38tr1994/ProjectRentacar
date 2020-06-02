using Rentacar.BusinessLogic.IdentityLogic;
using Rentacar.BusinessLogic.IdentityLogic.Interfaces;
using Rentacar.Models;

namespace Rentacar.BusinessLogic
{
    public interface IFactory
    {
        IRegisterLogic Register { get; }
        ILoginLogic Login { get; }
        ILogOut LogOut { get; }
    }
}