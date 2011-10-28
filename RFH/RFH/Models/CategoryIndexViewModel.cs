using System.Collections.Generic;

namespace RFH.Models
{
    public class CategoryIndexViewModel
    {
        public Category Category { get; set; }
        public IEnumerable<Article> Articles { get; set; }
    }
}