using AlbumApp.Data;
using AlbumApp.Data.Entities;

namespace AlbumApp.MVC.Services;

public class AdminService
{
    private readonly AppDbContext _context;

    public AdminService(AppDbContext context)
    {
        _context = context;
    }
    public void AddArtist(string name)
    {
        var artist = new Artist() { Name = name };
        _context.Artists.Add(artist);
        _context.SaveChanges();
    }
    public void EditArtist(int id, string newName)
    {
        var artist = _context.Artists.Find(id);
        artist.Name = newName;
        _context.Update(artist);
        _context.SaveChanges();
    }
    public void DeleteArtist(int id)
    {
        var artist = _context.Artists.Find(id);
        try
        {
            _context.Remove(artist);
            _context.SaveChanges();
        }
        catch
        {
            throw new Exception();
        }
    }
    public void AddAlbum(string name, int artistId, DateTime releaseDate, TimeSpan length)
    {
        var artist = _context.Artists.Find(artistId);
        if (artist == null)
        {
            throw new ArgumentException("Artist with the specified ID does not exist.", nameof(artistId));
        }

        var newAlbum = new Album
        {
            Name = name,
            ArtistId = artistId,
            ReleaseDate = releaseDate,
            Length = length
        };

        _context.Albums.Add(newAlbum);
        _context.SaveChanges();
    }
    public void EditAlbum(int id, string title, int artistId, DateTime releaseDate, TimeSpan length)
    {
        var artist = _context.Artists.Find(artistId);
        if (artist == null)
        {
            throw new ArgumentException("Artist with the specified ID does not exist.", nameof(artistId));
        }
        var album = _context.Albums.Find(id);
        if (album != null)
        {
            album.Name = title;
            album.ArtistId = artistId;
            album.ReleaseDate = releaseDate;
            album.Length = length;
        }

        _context.Albums.Update(album);
        _context.SaveChanges();
    }
    public void DeleteAlbum(int id)
    {
        var artist = _context.Albums.Find(id);
        try
        {
            _context.Remove(artist);
            _context.SaveChanges();
        }
        catch
        {
            throw new Exception();
        }
    }
    public void AddSong(int albumId, int trackNumber, string title)
    {
        var song = new Song() { AlbumId = albumId, TrackNumber = trackNumber, Title = title };
        _context.Add(song);
        _context.SaveChanges();
    }
    public void EditSong(int id, int albumId, int trackNumber, string title)
    {
        var song = _context.Songs.Find(id);
        song.Title = title;
        song.AlbumId = albumId;
        song.TrackNumber = trackNumber;
        _context.Update(song);
        _context.SaveChanges();
    }
    public void DeleteSong(int id)
    {
        var song = _context.Songs.Find(id);
        _context.Remove(song);
        _context.SaveChanges();
    }
    public List<Artist> GetAllArtists()
    {
        return _context.Artists.ToList();
    }
    public List<Song> GetAllSongs()
    {
        return _context.Songs.ToList();
    }

}
