using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Rentacar.DataAccess.Data.Repository.IRepository;
using Rentacar.Models;

namespace Rentacar.DataAccess.Data.Repository
{
    public class FuelRepository : Repository<Fuel>, IFuelRepository
    {
        private readonly ApplicationDbContext _db;


        public FuelRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public IEnumerable<SelectListItem> GetFuelListItemsForDropDown()
        {
            return _db.Fuel.Select(i => new SelectListItem()
            {
                Text = i.Type,
                Value = i.Id.ToString()
            });
        }

        public void Update(Fuel fuel)
        {
            var objFromDb = _db.Fuel.FirstOrDefault(s => s.Id == fuel.Id);
            objFromDb.Type = fuel.Type;

            _db.SaveChanges();
        }
    }
}
