using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RFH.Models;
using RFH.Infrastructure;
using System.Data.Entity;

namespace RFH.Controllers
{
    public class SearchFilterController : Controller
    {
        private DataContext _dataContext = new DataContext();

        //
        // GET: /SearchFilter/
        [HttpGet]
        public JsonResult Index(string orgType, string regionType, string agSize, string yearsOld, string cropType, string numRecipients)
        {
            IQueryable<HostSiteToHostSiteTagValue> query = _dataContext
                                            .HostSiteToHostSiteTagValues
                                            .Include("HostSite")
                                            .Include("HostSiteTagValue");


            List<HostSite> hostSiteList = new List<HostSite>();

            if (!string.IsNullOrEmpty(orgType))
            {
                var orgTypeRecord = _dataContext.HostSiteTagValues.FirstOrDefault(h => h.Name == orgType);

                if (orgTypeRecord != null)
                {
                    query = query.Where(r => r.HostSiteTagValueId == orgTypeRecord.Id);
                }

            }

            if (!string.IsNullOrEmpty(regionType))
            {
                var orgTypeRecord = _dataContext.HostSiteTagValues.FirstOrDefault(h => h.Name == regionType);

                if (orgTypeRecord != null)
                {
                    query = query.Where(r => r.HostSiteTagValueId == orgTypeRecord.Id);
                }
            }

            if (!string.IsNullOrEmpty(agSize))
            {
                var orgTypeRecord = _dataContext.HostSiteTagValues.FirstOrDefault(h => h.Name == agSize);

                if (orgTypeRecord != null)
                {
                    query = query.Where(r => r.HostSiteTagValueId == orgTypeRecord.Id);
                }

            }

            if (!string.IsNullOrEmpty(cropType))
            {
                var orgTypeRecord = _dataContext.HostSiteTagValues.FirstOrDefault(h => h.Name == cropType);

                if (orgTypeRecord != null)
                {
                    query = query.Where(r => r.HostSiteTagValueId == orgTypeRecord.Id);
                }
            }

            if (!string.IsNullOrEmpty(yearsOld))
            {
                var orgTypeRecord = _dataContext.HostSiteTagValues.FirstOrDefault(h => h.Name == yearsOld);

                if (orgTypeRecord != null)
                {
                    query = query.Where(r => r.HostSiteTagValueId == orgTypeRecord.Id);
                }

            }

            if (!string.IsNullOrEmpty(numRecipients))
            {
                var orgTypeRecord = _dataContext.HostSiteTagValues.FirstOrDefault(h => h.Name == numRecipients);

                if (orgTypeRecord != null)
                {
                    query = query.Where(r => r.HostSiteTagValueId == orgTypeRecord.Id);
                }

            }

            foreach (var hostSiteValue in query.ToList())
            {
                if(!hostSiteList.Exists(h => h.Id == hostSiteValue.HostSite.Id))
                {
                    hostSiteList.Add(hostSiteValue.HostSite);
                }
            }

            return Json(hostSiteList, JsonRequestBehavior.AllowGet);
        }

    }
}
