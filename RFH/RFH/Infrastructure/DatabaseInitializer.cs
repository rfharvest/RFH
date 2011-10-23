using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using RFH.Models;

namespace RFH.Infrastructure {
    public class DatabaseInitializer: DropCreateDatabaseAlways<DataContext> {

        protected override void Seed(DataContext context) {



            var categories = new List<Category> {
                new Category {Name="Gleaning", Description="Gleans"},
                new Category {Name="Market Recovery", Description="Something to do with Market Recovery"}
            };

            categories.ForEach(c => context.Categories.Add(c));


            var hostSites = new List<HostSite> {
                new HostSite {
                    Name="MT Vernon", 
                    Description="TBD", 
                    HostSiteUrl="http://mtvernon.org", 
                    Area="MT Vernon", 
                    IsActive=true,
                    MetaData = new List<HostSiteMetaData> {
                        new HostSiteMetaData {
                            Type="Organization Type", Values= new List<HostSiteMetaDataValue> {new HostSiteMetaDataValue {Value="Food Bank"}, new HostSiteMetaDataValue {Value="Non-Profit"} }
                        },
                        new HostSiteMetaData {
                            Type="Agricultural Size", Values= new List<HostSiteMetaDataValue> {new HostSiteMetaDataValue {Value="Small"} }
                        },
                    }
                },
                new HostSite {Name="Seattle Lettuce Link", Description="TBD", HostSiteUrl="http://seattlelettuce.org", Area="Seattle", IsActive=true}
            };

            hostSites.ForEach(h => context.HostSites.Add(h));





            var article1 = new Article {
                HostSite = hostSites[0],
                Category = categories[0],
                Content = "Here is the content of article 1",
                ShortDescription="Article Short Description",
                IsPublished=true
            };
            context.Articles.Add(article1);


            var article2 = new Article {
                HostSite = hostSites[0],
                Category = categories[1],
                Content = "Here is the content of article 2",
                ShortDescription = "Article Short Description 2",
                IsPublished = true
            };
            context.Articles.Add(article2);



            var article3 = new Article {
                HostSite = hostSites[0],
                Category = categories[0],
                Content = "I am article 3",
                ShortDescription = "Article Short Description 3",
                IsPublished = true
            };
            context.Articles.Add(article3);



            var article4 = new Article {
                HostSite = hostSites[1],
                Category = categories[0],
                Content = "I am article 4",
                ShortDescription = "Article Short Description 4",
                IsPublished = true
            };
            context.Articles.Add(article4);


        
            // Adding content for ContentData
            context.ContentDatas.Add(new ContentData {
                ControllerName="Home",
                ActionName="Index",
                Content="Hello from Home!"
            });


        }

    }
}