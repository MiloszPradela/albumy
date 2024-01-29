using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace AlbumApp.MVC.Models.Admin;

public class EditAlbumModel
{
    [HiddenInput]
    [Required]
    public int AlbumId { get; set; }
    [Required(ErrorMessage = "Name is required.")]
    [MaxLength(255)]
    public string Name { get; set; }

    [Required(ErrorMessage = "Artist is required.")]
    public int ArtistId { get; set; }

    [Required(ErrorMessage = "Release date is required.")]
    [DataType(DataType.Date)]
    public DateTime ReleaseDate { get; set; }

    [Required(ErrorMessage = "Length is required.")]
    [DataType(DataType.Time)]
    public TimeSpan Length { get; set; }
}
