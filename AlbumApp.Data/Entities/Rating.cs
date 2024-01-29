using Microsoft.AspNetCore.Identity;

namespace AlbumApp.Data.Entities;
public class Rating
{
    public int Id { get; set; }
    public int AlbumId { get; set; }
    public Album Album { get; set; }
    public int Value { get; set; }
    public string IdentityUserId { get; set; }
    public IdentityUser IdentityUser { get; set; }
}
