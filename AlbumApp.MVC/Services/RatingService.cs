using AlbumApp.Data;
using AlbumApp.Data.Entities;

namespace AlbumApp.MVC.Services;

public class RatingService
{
    private readonly AppDbContext _context;

    public RatingService(AppDbContext context)
    {
        _context = context;
    }

    public bool HasUserRatedAlbum(int albumId, string userId)
    {
        return _context.Ratings.Any(r => r.AlbumId == albumId && r.IdentityUserId == userId);
    }

    public int GetUserRating(int albumId, string userId)
    {
        var rating = _context.Ratings
            .FirstOrDefault(r => r.AlbumId == albumId && r.IdentityUserId == userId);

        return rating?.Value ?? 0;
    }

    public void AddRating(int albumId, string userId, int value)
    {
        var rating = new Rating
        {
            AlbumId = albumId,
            IdentityUserId = userId,
            Value = value
        };

        _context.Ratings.Add(rating);
        _context.SaveChanges();
    }
}
