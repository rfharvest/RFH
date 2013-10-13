using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using RFH.Infrastructure;
using RFH.Models;
using System.Text.RegularExpressions;

namespace RFH.Controllers
{
    public class HostSiteApiController : ApiController
    {
        private readonly DataContext _dataContext = new DataContext();

        public IEnumerable<HostSite> Get()
        {
            var rv = _dataContext.HostSites.Where(h => h.Latitude.HasValue && h.Longitude.HasValue && h.IsActive).ToList();
            return rv;
        }
    }
}
