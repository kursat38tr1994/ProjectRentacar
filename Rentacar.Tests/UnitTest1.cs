using System;
using Microsoft.EntityFrameworkCore;
using Rentacar.DataAccess;
using Xunit;

namespace Rentacar.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: "database_name").Options;

        }
    }
}