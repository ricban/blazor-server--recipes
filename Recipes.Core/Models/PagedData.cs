using System;
using System.Collections.Generic;
using System.Linq;

namespace Recipes.Core.Models
{
    public class PagedData<T> where T : class
    {
        public PagedData()
        {
        }

        public PagedData(List<T> items, int pageNumber, int pageSize)
        {
            Items = items;
            PageSize = pageSize;
            CurrentPage = pageNumber;

            if (TotalCount > 0 && pageSize > 0 )
            {
                TotalPages = (int)Math.Ceiling(TotalCount / (double)pageSize);
            }
            else
            {
                TotalPages = 1;
            }
        }

        public List<T> Items { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int TotalCount => Items?.Count ?? 0;
        public bool PreviousPage => CurrentPage > 1;
        public bool NextPage => CurrentPage < TotalPages;

        public static PagedData<T> Create(IEnumerable<T> source, int pageNumber, int pageSize)
        {
            var pagenumber = pageNumber < 1 ? 1 : pageNumber;
            var pagesize = pageSize < 1 ? 0 : pageSize;
            var skipCount = (pagenumber - 1) * pagesize;
            var takeCount = pagesize;
            var query = source.AsQueryable();

            if (skipCount > 0)
            {
                query = query.Skip(skipCount);
            }

            if (takeCount > 0)
            {
                query = query.Take(takeCount);
            }

            return new PagedData<T>(query.ToList(), pageNumber, pageSize);
        }
    }
}