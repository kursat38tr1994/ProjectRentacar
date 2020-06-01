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
    public class RentDetailsRepository : Repository<RentDetails>, IRentDetailsRepository
    {
        private readonly ApplicationDbContext _db;


        public RentDetailsRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }


        public void Update(RentDetails obj)
        {
            _db.Update(obj);
        }
    }

}
