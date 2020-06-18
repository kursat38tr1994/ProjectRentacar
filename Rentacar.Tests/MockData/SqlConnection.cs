using System;
using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using Rentacar.DataAccess;

namespace Rentacar.Tests.MockData
{
    public class SqlConnection
    {
        public void SqlConnectionString()
        {
            var dbConnect = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlServer("Data Source=tcp:rentacardbserver.database.windows.net,1433;Initial Catalog=Rentacar_db;User Id=kursatadmin@rentacardbserver;Password=Kikker38!");
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .EnableSensitiveDataLogging()                
                .Options;
            
            
        }
    }
}