﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUNAK.VMS.CORE.QueryFilters
{
    public class CvssStateQueryFilter
    {
        public string? Sort { get; set; }
        public string? Order { get; set; }
        public int Size { get; set; }
        public int Page { get; set; }
    }
}
