using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using RFH.Models;

namespace RFH.Infrastructure {
    public class DatabaseInitializer : DropCreateDatabaseIfModelChanges<DataContext>
    {
        protected override void Seed(DataContext context) {



            var categories = new List<Category> {
                new Category {Name="Gleaning", Description="Gleans"},
                new Category {Name="Market Recovery", Description="Something to do with Market Recovery"}
            };

            categories.ForEach(c => context.Categories.Add(c));


            var hostSites = new List<HostSite> {
                new HostSite {
                    Name="MT Vernon Food Bank", 
                    Description=@"The following information describes how the gleaning coordinator organize a food recovery program in this region- including collection of surplus
                    produce from farms, home gardens, and farmers markets.  Because the program is new and still growing, information reflects the gleaning coordinator's experience in starting a program essentially from scratch.
                    Goals for the future of the program are described as well", 
                    HostSiteUrl="http://mtvernon.org", 
                    Area="MT Vernon", 
                    IsActive=true,
                    MetaData = new List<HostSiteMetaData> {
                        new HostSiteMetaData {
                            Type="Organization Type", Values= new List<HostSiteMetaDataValue> {new HostSiteMetaDataValue {Value="Food Bank"}, new HostSiteMetaDataValue {Value="Non-Profit"} }
                        },
                        new HostSiteMetaData {
                            Type="Agricultural Size", Values= new List<HostSiteMetaDataValue> {new HostSiteMetaDataValue {Value="Small scale"}, new HostSiteMetaDataValue{Value="residential"} }
                        },
                        new HostSiteMetaData {
                            Type="Type of region", Values= new List<HostSiteMetaDataValue> {new HostSiteMetaDataValue {Value="city surrounded"}, new HostSiteMetaDataValue{Value="by rural area"}, new HostSiteMetaDataValue{Value="suburban"} }
                        },
                        new HostSiteMetaData {
                            Type="Years running a gleaning program", Values= new List<HostSiteMetaDataValue> {new HostSiteMetaDataValue {Value="less than 2 years"} }
                        },
                        new HostSiteMetaData {
                            Type="Primary agriculture crops", Values= new List<HostSiteMetaDataValue> {new HostSiteMetaDataValue {Value="vegetable row crops (95%)"}, new HostSiteMetaDataValue {Value="tree fruit (5%)"} }
                        },
                        new HostSiteMetaData {
                            Type="Number of recipient agencies", Values= new List<HostSiteMetaDataValue> {new HostSiteMetaDataValue {Value="more than 8 agencies"} }
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

            context.ContentDatas.Add(new ContentData
            {
                ControllerName = "GeneralResource",
                ActionName = "Index",
                Content = "<h1>These are general resources!</h1>"
            });

            var hostSiteTag1 = new HostSiteTag {Name = "Organization Type"};
            var hostSiteTag2 = new HostSiteTag {Name = "Agricultural Size"};
            var hostSiteTag3 = new HostSiteTag {Name = "Primary Agriculture Crops"};
            var hostSiteTag4 = new HostSiteTag {Name = "Region Type"};
            var hostSiteTag5 = new HostSiteTag {Name = "Years Running A Gleaning Program"};
            var hostSiteTag6 = new HostSiteTag {Name = "Number of Recipient Agencies"};

            var hostSiteTag1Value1 = new HostSiteTagValue { HostSiteTag = hostSiteTag1, Name = "Food Bank" };
            var hostSiteTag1Value2 = new HostSiteTagValue { HostSiteTag = hostSiteTag1, Name = "Non-Profit" };
            var hostSiteTag1Value3 = new HostSiteTagValue { HostSiteTag = hostSiteTag1, Name = "Community Action Program" };
            var hostSiteTag1Value4 = new HostSiteTagValue { HostSiteTag = hostSiteTag1, Name = "Coalition" };

            var hostSiteTag2Value1 = new HostSiteTagValue { HostSiteTag = hostSiteTag2, Name = "Residential" };
            var hostSiteTag2Value2 = new HostSiteTagValue { HostSiteTag = hostSiteTag2, Name = "Small Scale" };
            var hostSiteTag2Value3 = new HostSiteTagValue { HostSiteTag = hostSiteTag2, Name = "Large Commercial" };

            var hostSiteTag3Value1 = new HostSiteTagValue { HostSiteTag = hostSiteTag3, Name = "Veggie Row Crops" };
            var hostSiteTag3Value2 = new HostSiteTagValue { HostSiteTag = hostSiteTag3, Name = "Tree Fruit" };

            var hostSiteTag4Value1 = new HostSiteTagValue { HostSiteTag = hostSiteTag4, Name = "Rural" };
            var hostSiteTag4Value2 = new HostSiteTagValue { HostSiteTag = hostSiteTag4, Name = "Urban" };
            var hostSiteTag4Value3 = new HostSiteTagValue { HostSiteTag = hostSiteTag4, Name = "Suburban" };
            var hostSiteTag4Value4 = new HostSiteTagValue { HostSiteTag = hostSiteTag4, Name = "City Surrounded By Rural Area" };

            var hostSiteTag5Value1 = new HostSiteTagValue { HostSiteTag = hostSiteTag5, Name = "Less than 2 years" };
            var hostSiteTag5Value2 = new HostSiteTagValue { HostSiteTag = hostSiteTag5, Name = "2 to 5 years" };
            var hostSiteTag5Value3 = new HostSiteTagValue { HostSiteTag = hostSiteTag5, Name = "More than 5 years" };

            var hostSiteTag6Value1 = new HostSiteTagValue { HostSiteTag = hostSiteTag6, Name = "One (host site)" };
            var hostSiteTag6Value2 = new HostSiteTagValue { HostSiteTag = hostSiteTag6, Name = "2-8 Agencies" };
            var hostSiteTag6Value3 = new HostSiteTagValue { HostSiteTag = hostSiteTag6, Name = "More than 8 agencies" };

            context.HostSiteTags.Add(hostSiteTag1);
            context.HostSiteTags.Add(hostSiteTag2);
            context.HostSiteTags.Add(hostSiteTag3);
            context.HostSiteTags.Add(hostSiteTag4);
            context.HostSiteTags.Add(hostSiteTag5);
            context.HostSiteTags.Add(hostSiteTag6);

            context.HostSiteTagValues.Add(hostSiteTag1Value1);
            context.HostSiteTagValues.Add(hostSiteTag1Value2);
            context.HostSiteTagValues.Add(hostSiteTag1Value3);
            context.HostSiteTagValues.Add(hostSiteTag1Value4);

            context.HostSiteTagValues.Add(hostSiteTag2Value1);
            context.HostSiteTagValues.Add(hostSiteTag2Value2);
            context.HostSiteTagValues.Add(hostSiteTag2Value3);

            context.HostSiteTagValues.Add(hostSiteTag3Value1);
            context.HostSiteTagValues.Add(hostSiteTag3Value2);

            context.HostSiteTagValues.Add(hostSiteTag4Value1);
            context.HostSiteTagValues.Add(hostSiteTag4Value2);
            context.HostSiteTagValues.Add(hostSiteTag4Value3);
            context.HostSiteTagValues.Add(hostSiteTag4Value4);

            context.HostSiteTagValues.Add(hostSiteTag5Value1);
            context.HostSiteTagValues.Add(hostSiteTag5Value2);
            context.HostSiteTagValues.Add(hostSiteTag5Value3);

            context.HostSiteTagValues.Add(hostSiteTag6Value1);
            context.HostSiteTagValues.Add(hostSiteTag6Value2);
            context.HostSiteTagValues.Add(hostSiteTag6Value3);
        }
    }
}