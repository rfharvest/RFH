using System.Collections.Generic;

namespace RFH.Models
{
    public class ManageDocumentIndexViewModel
    {
        public string FolderUrl { get; set; }
        public IList<string> DocumentUrls { get; set; }
    }
}