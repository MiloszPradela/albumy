using AlbumApp.MVC.Models;

namespace AlbumApp.MVC.Models
{
    public class AlbumListViewModel
    {
        public PagingList<AlbumViewModel> PaginatedAlbums { get; set; }
    }
}
