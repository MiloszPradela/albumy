namespace AlbumApp.Data.Entities;
public class Album
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int ArtistId { get; set; }
    public Artist Artist { get; set; }
    public List<Song> Songs { get; set; }
    public List<Rating> Ratings { get; set; }
    public DateTime ReleaseDate { get; set; }
    public TimeSpan Length { get; set; }
}