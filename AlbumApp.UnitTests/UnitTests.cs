using AlbumApp.MVC.Services;

namespace AlbumApp.UnitTests
{
    public class UnitTests
    {
        [Fact]
        public void GetAllAlbums_Should_ReturnAllAlbums()
        {
            var dbHelper = new DbContextHelper();

            // Arrange
            using var dbContext = dbHelper.GetInMemoryDbContext();
            dbContext.Database.EnsureCreated();

            var adminService = new AdminService(dbContext);
            var albumService = new AlbumService(dbContext);

            // Add an artist
            adminService.AddArtist("Artist1");

            // Add albums
            adminService.AddAlbum("Album1", 1, DateTime.Now, TimeSpan.FromMinutes(30));
            adminService.AddAlbum("Album2", 1, DateTime.Now, TimeSpan.FromMinutes(40));

            // Act
            var allAlbums = albumService.GetAllAlbums();

            // Assert
            Assert.Equal(2, allAlbums.Count);

            dbContext.Database.EnsureDeleted();
        }

        [Fact]
        public void GetAlbumById_Should_ReturnCorrectAlbum()
        {
            var dbHelper = new DbContextHelper();

            // Arrange
            using var dbContext = dbHelper.GetInMemoryDbContext();
            dbContext.Database.EnsureCreated();

            var adminService = new AdminService(dbContext);
            var albumService = new AlbumService(dbContext);

            // Add an artist
            adminService.AddArtist("Artist1");

            // Add an album
            adminService.AddAlbum("Album1", 1, DateTime.Now, TimeSpan.FromMinutes(30));

            // Act
            var album = albumService.GetAlbumById(1);

            // Assert
            Assert.NotNull(album);
            Assert.Equal("Album1", album.Name);
            Assert.Equal("Artist1", album.ArtistName);

            dbContext.Database.EnsureDeleted();
        }

        [Fact]
        public void EditArtist_Should_UpdateArtistName()
        {
            var dbHelper = new DbContextHelper();

            // Arrange
            using var dbContext = dbHelper.GetInMemoryDbContext();
            dbContext.Database.EnsureCreated();

            var adminService = new AdminService(dbContext);

            const string originalName = "OriginalName";
            adminService.AddArtist(originalName);

            // Act
            const string updatedName = "UpdatedName";
            var artist = dbContext.Artists.First();
            adminService.EditArtist(artist.Id, updatedName);

            // Assert
            var updatedArtist = dbContext.Artists.Find(artist.Id);
            Assert.Equal(updatedName, updatedArtist.Name);

            dbContext.Database.EnsureDeleted();
        }

        [Fact]
        public void GetAllArtists_Should_ReturnAllArtists()
        {
            var dbHelper = new DbContextHelper();

            // Arrange
            using var dbContext = dbHelper.GetInMemoryDbContext();
            dbContext.Database.EnsureCreated();
            var adminService = new AdminService(dbContext);

            adminService.AddArtist("Artist1");
            adminService.AddArtist("Artist2");

            // Act
            var allArtists = adminService.GetAllArtists();

            // Assert
            Assert.Equal(2, allArtists.Count);

            dbContext.Database.EnsureDeleted();
        }

        [Fact]
        public void DeleteArtist_Should_RemoveArtist()
        {
            var dbHelper = new DbContextHelper();

            // Arrange
            using var dbContext = dbHelper.GetInMemoryDbContext();
            dbContext.Database.EnsureCreated();

            var adminService = new AdminService(dbContext);

            const string artistName = "ArtistToDelete";
            adminService.AddArtist(artistName);
            var artist = dbContext.Artists.First();

            // Act
            adminService.DeleteArtist(artist.Id);

            // Assert
            Assert.Empty(dbContext.Artists);

            dbContext.Database.EnsureDeleted();
        }

        [Fact]
        public void HasUserRatedAlbum_Should_ReturnFalse_When_UserHasNotRated()
        {
            var dbHelper = new DbContextHelper();

            // Arrange
            using var dbContext = dbHelper.GetInMemoryDbContext();
            dbContext.Database.EnsureCreated();
            var ratingService = new RatingService(dbContext);

            ratingService.AddRating(1, "user1", 4);

            // Act
            var result = ratingService.HasUserRatedAlbum(2, "user1");

            // Assert
            Assert.False(result);
            dbContext.Database.EnsureDeleted();
        }

        [Fact]
        public void AddRating_Should_AddRatingSuccessfully()
        {
            var dbHelper = new DbContextHelper();

            // Arrange
            using var dbContext = dbHelper.GetInMemoryDbContext();
            dbContext.Database.EnsureCreated();
            var ratingService = new RatingService(dbContext);

            // Act
            ratingService.AddRating(1, "user1", 4);

            // Assert
            Assert.True(ratingService.HasUserRatedAlbum(1, "user1"));
            Assert.Equal(4, ratingService.GetUserRating(1, "user1"));
            dbContext.Database.EnsureDeleted();
        }

    }
}
