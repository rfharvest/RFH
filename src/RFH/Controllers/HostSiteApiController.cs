using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using RFH.Infrastructure;
using RFH.Models;

namespace RFH.Controllers
{
    public class HostSiteApiController : ApiController
    {
        private readonly DataContext _dataContext = new DataContext();

        public IEnumerable<HostSite> Get()
        {
            var rv = _dataContext.HostSites;
            return rv;
        }
    }
}
