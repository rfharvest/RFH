using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace RFH.Models {
    public class UserCredentials {
        [Required]
        public String UserName {get; set;}

        [Required]
        public String Password { get; set; }
    }
}