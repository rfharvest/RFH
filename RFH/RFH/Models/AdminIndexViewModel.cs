﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RFH.Models
{
    public class AdminIndexViewModel
    {
        public IEnumerable<HostSite> HostSites { get; set; }
        public IEnumerable<Category> Categories { get; set; }
    }
}