using System;
using System.Collections.Generic;
using System.Linq;

namespace AlbumApp.MVC.Models
{
    public class PagingList<T>
    {
        private object listaAlbumow;
        private object currentPage;
        private object pageSize;

        public IEnumerable<T> Data { get; set; }
        public int Page { get; }
        public int Size { get; }
        public int TotalItems { get; }
        public int TotalPages { get; }
        public bool IsPrevious { get; }
        public bool IsNext { get; }

        private PagingList(IEnumerable<T> data, int page, int size, int totalItems)
        {
            Data = data;
            Page = page;
            Size = size;
            TotalItems = totalItems;
            TotalPages = (int)Math.Ceiling(totalItems / (double)size);
            IsPrevious = Page > 1;
            IsNext = Page < TotalPages;
        }

        public PagingList(object listaAlbumow, object currentPage, object pageSize)
        {
            this.listaAlbumow = listaAlbumow;
            this.currentPage = currentPage;
            this.pageSize = pageSize;
        }

        private static int ClipPage(int page, int size, int totalItems)
        {
            int totalPages = (int)Math.Ceiling(totalItems / (double)size);
            if (page <= 0)
            {
                page = 1;
            }
            if (page > totalPages)
            {
                return totalPages;
            }
            return page;
        }

        public static PagingList<T> Create(Func<int, int, IEnumerable<T>> dataGenerator, int page, int size, int totalItems)
        {
            page = ClipPage(page, size, totalItems);

            // Ogranicz liczbę elementów do maksymalnie 5
            int itemsToSkip = (page - 1) * size;
            IEnumerable<T> data = dataGenerator.Invoke(page, size).ToList();

            return new PagingList<T>(
                data,
                page,
                size,
                totalItems
            );
        }
    }
}
