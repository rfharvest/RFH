using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RFH.Infrastructure;
using System.Data.Entity;
using RFH.Models;

namespace RFH.Controllers
{
    public class NavigationListController : Controller
    { 
        
        private DataContext _dataContext = new DataContext();


        public ActionResult ListCategory()
        {
            List<SuperCategoryNavigationList> model = new List<SuperCategoryNavigationList>();

            var superCategories =
                _dataContext.SuperCategories.Include("Categories")
                            .Include("Pages")
                            .Where(sc => sc.IsActive)
                            .OrderBy(c => c.SortOrder)
                            .ToList();

            foreach (var superCategory in superCategories)
            {
               var superCategoryNavList = new SuperCategoryNavigationList
                    {
                        Name = superCategory.Name
                    };

                superCategoryNavList.Categories = new List<CategoryNavigationList>();
                superCategoryNavList.Categories.AddRange(superCategory.Categories.Select(c => new CategoryNavigationList
                    {
                        Name = c.Name,
                        Url = "/Category/" + c.UrlFriendlyName,
                    }));
                superCategoryNavList.Categories.AddRange(superCategory.Pages.Where(p=>p.IsActive).Select(p => new CategoryNavigationList
                    {
                        Name = p.Name,
                        Url = "/Page/" + p.UrlFriendlyName,
                    }));

                if (superCategory.SuperCategoryId == 7)
                {
                    superCategoryNavList.Categories.Add(new CategoryNavigationList
                        {
                            Name = "Harvest Directory",
                            Url = "/HarvestDirectory",
                        });
                }

                superCategoryNavList.Categories = superCategoryNavList.Categories.OrderBy(c => c.Name).ToList();

                model.Add(superCategoryNavList);
            }

            

           return PartialView(model);
        }


        public ActionResult ListSite() {
            return PartialView(_dataContext.HostSites.Where(s => s.IsActive).OrderBy(s => s.Name).ToList());
        }


    }
}
