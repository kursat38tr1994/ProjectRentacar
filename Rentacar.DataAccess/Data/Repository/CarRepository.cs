using Microsoft.AspNetCore.Mvc.Rendering;
using Rentacar.DataAccess.Data.Repository.IRepository;
using Rentacar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rentacar.DataAccess.Data.Repository
{
    public class CarRepository : Repository<Car>, ICarRepository
    {
        private readonly ApplicationDbContext _db;

        public CarRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Car car)
        {
            var objFromDb = _db.Car.FirstOrDefault(s => s.Id == car.Id);
            objFromDb.LicensePlate = car.LicensePlate;
            objFromDb.Fuel = car.Fuel;
            objFromDb.CurrentPrice = car.CurrentPrice;
            objFromDb.Availability = car.Availability;
            objFromDb.BrandId = car.BrandId;

            _db.SaveChanges();
        }
    }
}
