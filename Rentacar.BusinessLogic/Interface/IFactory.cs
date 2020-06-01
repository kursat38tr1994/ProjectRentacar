using Rentacar.BusinessLogic.IdentityLogic.Interfaces;

namespace Rentacar.BusinessLogic
{
    public interface IFactory
    {
        IRegisterLogic Register { get; }
        ILoginLogic Login { get; }
    }
}