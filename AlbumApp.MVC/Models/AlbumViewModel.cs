using AlbumApp.Data.Entities;

namespace AlbumApp.MVC.Models;

public class AlbumViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string ArtistName { get; set; }
    public List<Rating> Ratings { get; set; }
    public List<Song> Songs { get; set; }
    public DateTime ReleaseDate { get; set; }
    public TimeSpan Length { get; set; }
}
