using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RFH.Models
{
    public class BackupRecipient
    {
        public int Id { get; set; }

        [DisplayName("Email Address")]
        [RegularExpression(@"^([0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$", ErrorMessage = "Please check the email address")]
        [MaxLength(100)]
        [Required]
        public string EmailAddress { get; set; }
    }
}