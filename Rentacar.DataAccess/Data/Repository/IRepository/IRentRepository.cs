using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;
using Rentacar.Models;

namespace Rentacar.DataAccess.Data.Repository.IRepository
{
    public interface IRentRepository : IRepository<Rent>
    {
     
        void Update(Rent obj);

        public int ReturnLast();
    }
}
