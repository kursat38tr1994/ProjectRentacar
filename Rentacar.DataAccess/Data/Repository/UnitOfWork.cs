﻿using Rentacar.DataAccess.Data.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rentacar.DataAccess.Data.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
           
            Brand = new BrandRepository(_db);
            Car = new CarRepository(_db);
            Fuel = new FuelRepository(_db);
            User = new UserRepository(_db);
        }

        public IBrandRepository Brand { get; private set; }

        public ICarRepository Car { get; private set; }

        public IFuelRepository Fuel { get; }
        public IUserRepository User { get; }

        public void Dispose()
        {
            _db.Dispose();
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
