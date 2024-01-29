using AlbumApp.MVC.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AlbumApp.MVC.Controllers;

public class AlbumController : Controller
{
    private readonly AlbumService _albumService;
    private readonly RatingService _ratingService;

    public AlbumController(AlbumService albumService, RatingService ratingService)
    {
        _albumService = albumService;
        _ratingService = ratingService;
    }

    public IActionResult Index(int? page)
    {
        int pageSize = 5; // liczba albumów na stronie
        int pageNumber = page ?? 1;

        var albums = _albumService.GetAllAlbums();

        var pagedAlbums = albums.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

        ViewBag.CurrentPage = pageNumber;
        ViewBag.TotalPages = (int)Math.Ceiling((double)albums.Count / pageSize);

        return View(pagedAlbums);
    }

    public IActionResult ShowSongs(int id)
    {
        var albumViewModel = _albumService.GetAlbumById(id);

        if (albumViewModel == null)
        {
            return RedirectToAction("Index");
        }

        return View(albumViewModel);
    }

    [HttpPost]
    public IActionResult RateAlbum(int albumId, int rating)
    {
        if (!User.Identity.IsAuthenticated)
        {
            // Handle unauthenticated user
            return RedirectToAction("Index");
        }

        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Convert the user ID to Guid

        // Check if the user has already rated the album
        if (_ratingService.HasUserRatedAlbum(albumId, userId))
        {
            // Handle error or display a message
            return RedirectToAction("Index");
        }

        // Add the new rating
        _ratingService.AddRating(albumId, userId, rating);

        return RedirectToAction("Index");
    }
}

