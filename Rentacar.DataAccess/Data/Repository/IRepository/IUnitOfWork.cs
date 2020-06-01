using System;
using System.Collections.Generic;
using System.Text;

namespace Rentacar.DataAccess.Data.Repository.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        IBrandRepository Brand { get;}
        ICarRepository Car { get;}
        IFuelRepository Fuel { get;}
        IUserRepository User { get;}
        IRentRepository Rent { get;}
        IRentDetailsRepository RentDetails { get;}
        IShoppingCartRepository ShoppingCart { get;}

        void Save();
    }
}
