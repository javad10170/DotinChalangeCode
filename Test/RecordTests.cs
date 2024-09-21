using Domain;

using FluentAssertions;

using Infrastructure;

using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    public class MyDataTests
    {
        private AppDbContext CreateInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            return new AppDbContext(options);
        }

        [Fact]
        public async Task AddAsync_Should_Add_User_To_Database()
        {
            // Arrange
            var dbContext = CreateInMemoryDbContext();

            var mydata = new MyData
            {
                Data = "Javad"
            };

            // Act
            await dbContext.MyData.AddAsync(mydata);
            await dbContext.SaveChangesAsync();

            // Assert
            var usersInDb = await dbContext.MyData.ToListAsync();
            usersInDb.Should().ContainSingle(u => u.Data == "Javad");
        }
    }
}
