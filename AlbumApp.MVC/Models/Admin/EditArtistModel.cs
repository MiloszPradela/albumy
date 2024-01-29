using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace AlbumApp.MVC.Models.Admin;

public class EditArtistModel
{
    [HiddenInput]
    public int Id { get; set; }
    [Required]
    [MaxLength(255)]
    public string Name { get; set; }
}
