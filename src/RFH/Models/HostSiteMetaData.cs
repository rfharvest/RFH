using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RFH.Models {
    public class HostSiteMetaData {

        public HostSiteMetaData() {
            this.Values = new List<HostSiteMetaDataValue>();
        }

        public int Id { get; set; }

        public string Type { get; set; }

        public ICollection<HostSiteMetaDataValue> Values { get; set; }
    }
}