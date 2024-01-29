using AlbumApp.Data;
using AlbumApp.MVC.Models;
using Microsoft.EntityFrameworkCore;

namespace AlbumApp.MVC.Services;

public class AlbumService
{
    private readonly AppDbContext _context;

    public AlbumService(AppDbContext context)
    {
        _context = context;
    }

    public List<AlbumViewModel> GetAllAlbums()
    {
        return _context.Albums
            .Include(a => a.Artist)
            .Select(a => new AlbumViewModel
            {
                Id = a.Id,
                Name = a.Name,
                ArtistName = a.Artist.Name,
                Ratings = a.Ratings,
                ReleaseDate = a.ReleaseDate,
                Length = a.Length
            })
            .ToList();
    }

    public AlbumViewModel GetAlbumById(int id)
    {
        var album = _context.Albums
            .Include(a => a.Artist)
            .Include(a => a.Songs)
            .FirstOrDefault(a => a.Id == id);

        if (album == null)
        {
            return null;
        }

        return new AlbumViewModel
        {
            Id = album.Id,
            Name = album.Name,
            ArtistName = album.Artist.Name,
            Ratings = album.Ratings,
            Songs = album.Songs,
            ReleaseDate = album.ReleaseDate,
            Length = album.Length
        };
    }




}
