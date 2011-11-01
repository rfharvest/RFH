using System.ComponentModel.DataAnnotations;

namespace RFH.Models 
{
    public class ContentData 
    {
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; }

        [Required, MaxLength(100)]
        public string ControllerName { get; set; }

        [Required, MaxLength(100)]
        public string ActionName { get; set; }

        [UIHint("HtmlContent")]
        [Required]
        [MaxLength]
        public string Content { get; set; }
    }
}