using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RFH.Models
{
    [Table("County")]
    public class County
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [DisplayName("County Name")]
        public string Name { get; set; }
        public string BoundaryPointDataString { get; set; }
    }
}