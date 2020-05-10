using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;
using Rentacar.Models;

namespace Rentacar.DataAccess.Data.Repository.IRepository
{
    public interface IFuelRepository : IRepository<Fuel>
    {
        IEnumerable<SelectListItem> GetFuelListItemsForDropDown();

        void Update(Fuel fuel);
    }
}
