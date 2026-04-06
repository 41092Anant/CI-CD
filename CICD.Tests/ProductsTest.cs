using CICD.Data;
using CICD.Models;
using Microsoft.EntityFrameworkCore;

namespace CICD.Tests;

public class ProductsTest
{
    private AppDbContext GetInMemoryDb()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        return new AppDbContext(options);
    }

    [Fact]
    public async Task CanAddAndRetrieveProduct()
    {
        var db = GetInMemoryDb();
        var product = new Product { Name = "Test Product", Price = 99.99m, Stock = 10 };
        db.Products.Add(product);
        await db.SaveChangesAsync();
        var result = await db.Products.ToListAsync();
        Assert.Single(result);
        Assert.Equal("Test Product", result[0].Name);
    }

}
