namespace AlbumApp.Data.Entities;
public class Artist
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<Album> Albums { get; set; }
}
