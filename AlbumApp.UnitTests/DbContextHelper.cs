using AlbumApp.Data;
using Microsoft.EntityFrameworkCore;

namespace AlbumApp.UnitTests;
public class DbContextHelper
{
    public AppDbContext GetInMemoryDbContext()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "InMemoryDatabase")
            .Options;

        return new AppDbContext(options);
    }
}