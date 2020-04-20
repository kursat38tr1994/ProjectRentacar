using Microsoft.AspNetCore.Mvc.Rendering;
using Rentacar.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rentacar.DataAccess.Data.Repository.IRepository
{
    public interface ICarRepository : IRepository<Car>
    {
        void Update(Car car);
    }
}
