using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Rentacar.Models;
using Rentacar.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rentacar.DataAccess.DbInitializer
{
    public class DbInitializer : IDbInitializer
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DbInitializer(ApplicationDbContext db, UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public void Initializer()
        {
            try
            {
                if (_db.Database.GetPendingMigrations().Count() > 1)
                {
                    _db.Database.Migrate();
                }
            }

            catch (Exception e)
            {
                // ignored
            }

            if (_db.Roles.Any(roles => roles.Name == Roles.Admin))
            {
                return;
            }


            _roleManager.CreateAsync(new IdentityRole(Roles.Admin)).GetAwaiter().GetResult();
            _roleManager.CreateAsync(new IdentityRole(Roles.Employee)).GetAwaiter().GetResult();
            _roleManager.CreateAsync(new IdentityRole(Roles.User)).GetAwaiter().GetResult();


            _userManager.CreateAsync(new User
            {
                UserName = "admin@admin.com",
                Email = "admin@admin.com",
                EmailConfirmed = true,
                Firstname = "Admin"
            }, "Admin123!").GetAwaiter().GetResult();


            _userManager.CreateAsync(new User
            {
                UserName = "medewerker@medewerker.com",
                Email = "medewerker@medewerker.com",
                EmailConfirmed = true,
                Firstname = "Medewerker"
            }, "Medewerker123!").GetAwaiter().GetResult();


            _userManager.CreateAsync(new User
            {
                UserName = "user@user.com",
                Email = "user@user.com",
                EmailConfirmed = true,
                Firstname = "User"
            }, "User123!").GetAwaiter().GetResult();


            User createAdmin = _db.User.FirstOrDefault(user => user.Email == "admin@admin.com");
            User createMedewerker = _db.User.FirstOrDefault(user => user.Email == "medewerker@medewerker.com");
            User createUser = _db.User.FirstOrDefault(user => user.Email == "user@user.com");


            _userManager.AddToRoleAsync(createAdmin, Roles.Admin).GetAwaiter().GetResult();
            _userManager.AddToRoleAsync(createMedewerker, Roles.Employee).GetAwaiter().GetResult();
            _userManager.AddToRoleAsync(createUser, Roles.User).GetAwaiter().GetResult();
        }
    }
}