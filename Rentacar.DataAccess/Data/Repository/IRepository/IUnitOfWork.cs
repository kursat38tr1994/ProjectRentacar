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

        void Save();
    }
}
