﻿using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity;
using RFH.Models;

namespace RFH.Infrastructure
{
    public class DataContext : DbContext
    {
        public DataContext() : base("RFHDB")
        {
            Database.SetInitializer(new DatabaseInitializer());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }

        public IDbSet<Article> Articles { get; set; }

        public IDbSet<Category> Categories { get; set; }

        public IDbSet<HostSite> HostSites { get; set; }

        public IDbSet<HostSiteMetaData> HostSiteMetaData { get; set; }

        public IDbSet<BackupRecipient> BackupRecipients { get; set; }

        public IDbSet<ContentData> ContentDatas { get; set; }
        
        public IDbSet<HostSiteTag> HostSiteTags { get; set; }
        
        public IDbSet<HostSiteTagValue> HostSiteTagValues { get; set; }
        
        public IDbSet<HostSiteToHostSiteTagValue> HostSiteToHostSiteTagValues { get; set; }
    }
}