﻿using Microsoft.AspNetCore.Mvc.Rendering;
using Rentacar.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rentacar.DataAccess.Data.Repository.IRepository
{
    public interface IBrandRepository : IRepository<Brand>
    {
        IEnumerable<SelectListItem> GetBrandListItemsForDropDown();

        void Update(Brand brand);
    }
}
