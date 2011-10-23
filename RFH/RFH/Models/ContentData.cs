using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace RFH.Models {
    public class ContentData {


        public int Id { get; set; }

        public string ControllerName { get; set; }

        public string ActionName { get; set; }


        public string Content { get; set; }




    }
}