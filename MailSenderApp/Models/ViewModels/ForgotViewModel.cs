using System.ComponentModel.DataAnnotations;

namespace MailSenderApp.Models
{
    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
