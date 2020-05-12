using Microsoft.AspNetCore.Mvc.Rendering;
using Rentacar.DataAccess.Data.Repository.IRepository;
using Rentacar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rentacar.DataAccess.Data.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly ApplicationDbContext _db;

        public UserRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }


        public void LockedUser(string id)
        {
            var userFromdb = _db.User.FirstOrDefault(u => u.Id == id);
            userFromdb.LockoutEnd = DateTime.Now.AddYears(1000);
            _db.SaveChanges();
        }

        public void UnlockUser(string id)
        {
            var userFromdb = _db.User.FirstOrDefault(u => u.Id == id);
            userFromdb.LockoutEnd = DateTime.Now;
            _db.SaveChanges();
        }
    }
}
