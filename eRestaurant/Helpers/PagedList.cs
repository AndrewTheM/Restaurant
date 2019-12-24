using eRestaurant.DTO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace eRestaurant.Helpers
{
    public class PagedList<T> : List<T>, IPaginationHelper
    {
        public int CurrentPage { get; private set; }
        public int PageSize { get; private set; }
        public int TotalCount { get; private set; }
        public int TotalPages { get; private set; }

        public PagedList(IEnumerable<T> enumerable, PagingParameters pars)
        {
            CurrentPage = pars.PageNumber > 0 ? pars.PageNumber : 1;
            PageSize = pars.PageSize > 0 ? pars.PageSize : 10;
            TotalCount = enumerable.Count();
            TotalPages = (int)Math.Ceiling(TotalCount / (double)PageSize);

            var paged = enumerable.Skip((CurrentPage - 1) * PageSize).Take(PageSize);
            AddRange(paged);
        }
    }
}
