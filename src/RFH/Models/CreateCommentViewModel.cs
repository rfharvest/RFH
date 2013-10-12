using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RFH.Models
{
    public class CreateCommentViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        [Column("Comment")]
        public string Text { get; set; }

        [Required]
        public string Location { get; set; }

        public int ArticleId { get; set; }

    }
}