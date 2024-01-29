using AlbumApp.MVC.Models.Admin;
using AlbumApp.MVC.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AlbumApp.MVC.Controllers;
[Authorize]
public class AdminController : Controller
{
    private readonly AdminService _adminService;
    private readonly AlbumService _albumService;
    private readonly UserManager<IdentityUser> _userManager;

    public AdminController(AdminService adminService, UserManager<IdentityUser> userManager, AlbumService albumService)
    {
        _adminService = adminService;
        _userManager = userManager;
        _albumService = albumService;
    }
    public async Task<IActionResult> Index()
    {
        return await Authorize() ? View() : View("Unauthorized");
    }

    public async Task<IActionResult> AddArtist()
    {
        return await Authorize() ? View(new AddArtistModel()) : View("Unauthorized");
    }

    [HttpPost]
    public async Task<IActionResult> AddArtist(string name)
    {
        var authorized = await Authorize();
        if (!authorized) return View("Unauthorized");
        try
        {
            _adminService.AddArtist(name);
            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, "Error adding artist: " + ex.Message);
            return View();
        }
    }

    public async Task<IActionResult> EditArtist()
    {
        var authorized = await Authorize();
        if (!authorized) return View("Unauthorized");

        var artists = _adminService.GetAllArtists();
        var artistItems = artists.Select(a => new SelectListItem { Value = a.Id.ToString(), Text = $"{a.Id}. {a.Name}" }).ToList();
        ViewBag.Artists = artistItems;

        return View(new EditArtistModel());
    }

    [HttpPost]
    public async Task<IActionResult> EditArtist(int id, string name)
    {
        var authorized = await Authorize();
        if (!authorized) return View("Unauthorized");

        if (ModelState.IsValid)
        {
            _adminService.EditArtist(id, name);
        }

        return RedirectToAction("Index");
    }

    public async Task<IActionResult> DeleteArtist()
    {
        var authorized = await Authorize();
        if (!authorized) return View("Unauthorized");

        var artists = _adminService.GetAllArtists();
        var artistItems = artists.Select(a => new SelectListItem { Value = a.Id.ToString(), Text = $"{a.Id}. {a.Name}" }).ToList();
        ViewBag.Artists = artistItems;

        return View(new DeleteArtistModel());
    }

    [HttpPost]
    public async Task<IActionResult> DeleteArtist(int id)
    {
        var authorized = await Authorize();
        if (!authorized) return View("Unauthorized");

        if (ModelState.IsValid)
        {
            try
            {
                _adminService.DeleteArtist(id);
            }
            catch
            {
                return View("Error");
            }
        }

        return RedirectToAction("Index");
    }

    public async Task<IActionResult> DeleteAlbum()
    {
        var authorized = await Authorize();
        if (!authorized) return View("Unauthorized");

        var albums = _albumService.GetAllAlbums();
        var albumItems = albums.Select(a => new SelectListItem { Value = a.Id.ToString(), Text = $"{a.Id}. {a.Name}" }).ToList();
        ViewBag.Albums = albumItems;

        return View(new DeleteAlbumModel());
    }

    [HttpPost]
    public async Task<IActionResult> DeleteAlbum(int id)
    {
        var authorized = await Authorize();
        if (!authorized) return View("Unauthorized");

        if (ModelState.IsValid)
        {
            try
            {
                _adminService.DeleteAlbum(id);
            }
            catch
            {
                return View("Error");
            }
        }

        return RedirectToAction("Index", "Album");
    }

    public async Task<IActionResult> AddAlbum()
    {
        var authorized = await Authorize();
        if (!authorized) return View("Unauthorized");
        var artists = _adminService.GetAllArtists();
        var artistItems = artists.Select(a => new SelectListItem { Value = a.Id.ToString(), Text = $"{a.Id}. {a.Name}" }).ToList();
        ViewBag.Artists = artistItems;
        return View(new AddAlbumModel());
    }
    [HttpPost]
    public async Task<IActionResult> AddAlbum(string name, int artistId, DateTime releaseDate, TimeSpan length)
    {
        var authorized = await Authorize();
        if (!authorized) return View("Unauthorized");
        try
        {
            _adminService.AddAlbum(name, artistId, releaseDate, length);
            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, "Error adding album: " + ex.Message);
            return View();
        }
    }

    public async Task<IActionResult> EditAlbum()
    {
        var authorized = await Authorize();
        if (!authorized) return View("Unauthorized");

        var albums = _albumService.GetAllAlbums();
        var albumItems = albums.Select(a => new SelectListItem { Value = a.Id.ToString(), Text = $"{a.Id}. {a.Name}" }).ToList();
        ViewBag.Albums = albumItems;

        var artists = _adminService.GetAllArtists();
        var artistItems = artists.Select(a => new SelectListItem { Value = a.Id.ToString(), Text = $"{a.Id}. {a.Name}" }).ToList();
        ViewBag.Artists = artistItems;

        return View(new EditAlbumModel());
    }

    [HttpPost]
    public async Task<IActionResult> EditAlbum(int albumId, string name, int artistId, DateTime releaseDate, TimeSpan length)
    {
        var authorized = await Authorize();
        if (!authorized) return View("Unauthorized");
        try
        {
            _adminService.EditAlbum(albumId, name, artistId, releaseDate, length);
            return RedirectToAction("Index", "Album");
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, "Error adding album: " + ex.Message);
            return View();
        }
    }

    public async Task<IActionResult> AddSong()
    {
        var authorized = await Authorize();
        if (!authorized) return View("Unauthorized");

        var albums = _albumService.GetAllAlbums();
        var albumItems = albums.Select(a => new SelectListItem { Value = a.Id.ToString(), Text = $"{a.Id}. {a.Name}" }).ToList();
        ViewBag.Albums = albumItems;

        return View(new AddSongModel());
    }
    [HttpPost]
    public async Task<IActionResult> AddSong(int albumId, int trackNumber, string title)
    {
        var authorized = await Authorize();
        if (!authorized) return View("Unauthorized");
        try
        {
            _adminService.AddSong(albumId, trackNumber, title);
            return RedirectToAction("Index", "Album");
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, "Error adding song: " + ex.Message);
            return View();
        }
    }

    public async Task<IActionResult> EditSong()
    {
        var authorized = await Authorize();
        if (!authorized) return View("Unauthorized");

        var songs = _adminService.GetAllSongs();
        var songItems = songs.Select(a => new SelectListItem { Value = a.Id.ToString(), Text = $"{a.Id}. {a.Title}" }).ToList();
        ViewBag.Songs = songItems;

        var albums = _albumService.GetAllAlbums();
        var albumItems = albums.Select(a => new SelectListItem { Value = a.Id.ToString(), Text = $"{a.Id}. {a.Name}" }).ToList();
        ViewBag.Albums = albumItems;

        return View(new EditSongModel());
    }
    [HttpPost]
    public async Task<IActionResult> EditSong(int id, int albumId, int trackNumber, string title)
    {
        var authorized = await Authorize();
        if (!authorized) return View("Unauthorized");
        try
        {
            _adminService.EditSong(id, albumId, trackNumber, title);
            return RedirectToAction("Index", "Album");
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, "Error editing song: " + ex.Message);
            return View();
        }
    }

    public async Task<IActionResult> DeleteSong()
    {
        var authorized = await Authorize();
        if (!authorized) return View("Unauthorized");

        var songs = _adminService.GetAllSongs();
        var songItems = songs.Select(a => new SelectListItem { Value = a.Id.ToString(), Text = $"{a.Id}. {a.Title}" }).ToList();
        ViewBag.Songs = songItems;

        return View(new DeleteSongModel());
    }

    [HttpPost]
    public async Task<IActionResult> DeleteSong(int id)
    {
        var authorized = await Authorize();
        if (!authorized) return View("Unauthorized");

        if (ModelState.IsValid)
        {
            try
            {
                _adminService.DeleteSong(id);
            }
            catch
            {
                return View("Error");
            }
        }

        return RedirectToAction("Index", "Album");
    }

    private async Task<bool> Authorize()
    {
        var user = await _userManager.GetUserAsync(User);
        var isInRole = await _userManager.IsInRoleAsync(user, "Admin");
        if (!isInRole) return false;
        return true;
    }

}
