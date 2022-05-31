using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUNAK.VMS.CORE.CustomEntities
{
    public class Pagination
    {
        public int Length { get; set; }
        public int Size { get; set; }
        public int Page { get; set; }
        public int LastPage { get; set; }
        public bool HasNextPage { get; set; }
        public bool HasPreviousPage { get; set; }

        //ADD ENDINDEX Y STARTINDEX
        public int StartIndex { get; set; }
        public int EndIndex { get; set; }
        public string? NextPageUrl { get; set; }
        public string? PreviousPageUrl { get; set; }
    }
}
