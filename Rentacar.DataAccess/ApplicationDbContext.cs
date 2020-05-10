﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Rentacar.Models;

namespace Rentacar.DataAccess
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

       public DbSet<Brand> Brand { get; set; }

       public DbSet<Car> Car { get; set; }

       public DbSet<Fuel> Fuel { get; set; }
    }
}
