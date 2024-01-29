namespace AlbumApp.Data.Entities;
public class Song
{
    public int Id { get; set; }
    public int TrackNumber { get; set; }
    public string Title { get; set; }
    public int AlbumId { get; set; }
    public Album Album { get; set; }
}
