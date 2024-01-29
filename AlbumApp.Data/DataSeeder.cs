using AlbumApp.Data.Entities;

namespace AlbumApp.Data;
public class DataSeeder
{
    private readonly AppDbContext _dbContext;
    public DataSeeder(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Seed()
    {
        if (!_dbContext.Artists.Any() && !_dbContext.Albums.Any() && !_dbContext.Songs.Any())
        {
            List<Artist> artists =
            [
                new Artist() { Name = "Ed Sheeran" },
                new Artist() { Name = "Harry Styles" }
            ];

            _dbContext.AddRange(artists);
            _dbContext.SaveChanges();

            List<Album> albums =
                [
                    new Album() { Name = "X", Length = new(0, 50, 23), Ratings = [], ReleaseDate = new(2014, 6, 20), ArtistId = artists[0].Id },
                    new Album() { Name = "Fine Line", Length = new(0, 46, 37), Ratings = [], ReleaseDate = new(2020, 12, 13), ArtistId = artists[1].Id }
                ];

            _dbContext.AddRange(albums);
            _dbContext.SaveChanges();

            List<Song> songs =
                [
                    new Song() { Title = "One", TrackNumber = 1, AlbumId = albums[0].Id },
                    new Song() { Title = "I'm a mess", TrackNumber = 2, AlbumId = albums[0].Id },
                    new Song() { Title = "Sing", TrackNumber = 3, AlbumId = albums[0].Id },
                    new Song() { Title = "Don't", TrackNumber = 4, AlbumId = albums[0].Id },
                    new Song() { Title = "Nina", TrackNumber = 5, AlbumId = albums[0].Id },
                    new Song() { Title = "Photogrpah", TrackNumber = 6, AlbumId = albums[0].Id },
                    new Song() { Title = "Bloodstream", TrackNumber = 7, AlbumId = albums[0].Id },
                    new Song() { Title = "Tenerife Sea", TrackNumber = 8, AlbumId = albums[0].Id },
                    new Song() { Title = "Runaway", TrackNumber = 9, AlbumId = albums[0].Id },
                    new Song() { Title = "The Man", TrackNumber = 10, AlbumId = albums[0].Id },
                    new Song() { Title = "Thinking Out Loud", TrackNumber = 11, AlbumId = albums[0].Id },
                    new Song() { Title = "Afire Love", TrackNumber = 12, AlbumId = albums[0].Id },

                    new Song() { Title = "Lights Up", TrackNumber = 1, AlbumId = albums[1].Id },
                    new Song() { Title = "Adore You", TrackNumber = 2, AlbumId = albums[1].Id },
                    new Song() { Title = "Falling", TrackNumber = 3, AlbumId = albums[1].Id },
                    new Song() { Title = "Watermelon Sugar", TrackNumber = 4, AlbumId = albums[1].Id },
                    new Song() { Title = "Golden", TrackNumber = 5, AlbumId = albums[1].Id },
                    new Song() { Title = "Treat People with Kindness", TrackNumber = 6, AlbumId = albums[1].Id },
                    new Song() { Title = "Fine Line", TrackNumber = 7, AlbumId = albums[1].Id }
                ];

            _dbContext.AddRange(songs);
            _dbContext.SaveChanges();
        }
    }

}
