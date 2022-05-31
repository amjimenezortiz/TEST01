using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUNAK.VMS.CORE.CustomEntities
{
    public class PagedList<T> : List<T>
    {
        public int Page { get; set; }

        public int LastPage { get; set; }
        public int Size { get; set; }
        public int Length { get; set; }

        /// <summary>
        /// StarIndex and EndIndex for use in FrontEnd
        /// </summary>
        public int StartIndex { get; set; }
        public int EndIndex { get; set; }
        public bool HasPreviousPage => Page > 1;
        public bool HasNextPage => Page < LastPage;
        public int? NextPageNumber => HasNextPage ? Page + 1 : (int?)null;
        public int? PreviousPageNumber => HasPreviousPage ? Page - 1 : (int?)null;

        public PagedList(List<T> items, int count, int page, int size)
        {
            Length = count;
            Size = size;
            Page = page;
            LastPage = (int)Math.Ceiling(count / (double)Size);

            //StartIndex = firstItem.
            AddRange(items);
        }

        public static PagedList<T> Create(IEnumerable<T> source, int page, int size)
        {
            var count = source.Count();
            var items = source.Skip(page * size).Take(size).ToList();

            return new PagedList<T>(items, count, page, size);

        }
    }
}
