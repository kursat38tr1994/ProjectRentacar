﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Rentacar.DataAccess.Data.Repository.IRepository;
using Rentacar.Models;

namespace Rentacar.DataAccess.Data.Repository
{
    public class RentRepository : Repository<Rent>, IRentRepository
    {
        private readonly ApplicationDbContext _db;


        public RentRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }


        public void Update(Rent obj)
        {
            _db.Update(obj);
        }

        public int ReturnLast()
        {
            //Use your context or DbSet here to return the data.
            //You are using .Last() but I'm not sure if that gets what you want.
            var LastRent = dbSet.ToList().Last();
            return LastRent.Id;
        }
    }
}