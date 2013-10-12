using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RFH.Models
{
    public class Comment
    {
        [Key]
        public Int64 CommentId { get; set; }

        [Required]
        public int ArticleId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        [Column("Comment")]
        public string Text { get; set; }

        public string Location { get; set; }

        public DateTime PostDate { get; set; }

        public bool Visible { get; set; }

        [ForeignKey("ArticleId")]
        public Article Article { get; set; }
    }
}