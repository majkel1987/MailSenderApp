using System.ComponentModel.DataAnnotations;

namespace MailSenderApp.Models
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Hasło")]
        public string Password { get; set; }

        [Display(Name = "Nie wylogowywuj mnie")]
        public bool RememberMe { get; set; }
    }
}
