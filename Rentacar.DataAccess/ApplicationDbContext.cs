using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
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
       
       public DbSet<User> User { get; set; }

        public DbSet<ShoppingCart> ShoppingCart { get; set; }
        public DbSet<Rent> Rent { get; set; }
        public DbSet<RentDetails> RentDetail { get; set; }
        
    }
}
