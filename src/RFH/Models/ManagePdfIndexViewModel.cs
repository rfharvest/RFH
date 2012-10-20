using System.Collections.Generic;

namespace RFH.Models
{
    public class ManagePdfIndexViewModel
    {
        public string FolderUrl { get; set; }
        public IList<string> PdfUrls { get; set; }
    }
}