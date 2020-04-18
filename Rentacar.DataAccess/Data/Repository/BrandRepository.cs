using Microsoft.AspNetCore.Mvc.Rendering;
using Rentacar.DataAccess.Data.Repository.IRepository;
using Rentacar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rentacar.DataAccess.Data.Repository
{
    public class BrandRepository : Repository<Brand>, IBrandRepository
    {
        private readonly ApplicationDbContext _db;

        public BrandRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public IEnumerable<SelectListItem> GetBrandListItemsForDropDown()
        {
            return _db.Brand.Select(i => new SelectListItem()
            {
                Text = i.Type,
                Value = i.Id.ToString()
            });
            
        }

        public void Update(Brand brand)
        {
            var objFromDb = _db.Brand.FirstOrDefault(s => s.Id == brand.Id);
            objFromDb.Type = objFromDb.Type;

            _db.SaveChanges();
        }
    }
}
