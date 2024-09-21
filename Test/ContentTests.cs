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
    public class ContentTests
    {
        private ApplicationDbContext CreateInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            return new ApplicationDbContext(options);
        }

        [Fact]
        public async Task AddAsync_Should_Add_User_To_Database()
        {
            // Arrange
            var dbContext = CreateInMemoryDbContext();

            var content = new Content
            {
                Data = "Javad"
            };

            // Act
            await dbContext.Contents.AddAsync(content);
            await dbContext.SaveChangesAsync();

            // Assert
            var usersInDb = await dbContext.Contents.ToListAsync();
            usersInDb.Should().ContainSingle(u => u.Data == "Javad");
        }
    }
}
