using System.ComponentModel.DataAnnotations;

namespace AlbumApp.MVC.Models.Admin;

public class AddSongModel
{
    [Required]
    public int AlbumId { get; set; }
    [Required]
    [Range(1, 100)]
    public int TrackNumber { get; set; }
    [Required]
    [MaxLength(255)]
    public string Title { get; set; }
}
