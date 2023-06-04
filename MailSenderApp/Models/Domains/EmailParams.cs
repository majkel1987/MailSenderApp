using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MailSenderApp.Models.Domains
{
    public class EmailParams
    {
        [Display(Name = "Konto:")]
        public int Id { get; set; }

        [Required, Display(Name = "Host SMTP:")]
        public string HostSmtp { get; set; }
        
        [Required, Display(Name = "Certyfikat SSL:")]
        public bool EnableSsl { get; set; }

        [Required, Display(Name = "Port:")]
        public int Port { get; set; }

        [Required, Display(Name = "Mail:")]
        public string SenderEmail { get; set; }

        [Required, Display(Name = "Hasło:")]
        public string SenderEmailPassword { get; set; }

        [Required, Display(Name = "Nazwa użytkownika:")]
        public string SenderName { get; set; }

        [Required]
        [ForeignKey("User")]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

    }
}