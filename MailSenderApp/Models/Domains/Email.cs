using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MailSenderApp.Models.Domains
{
    public class Email
    {
        public int Id { get; set; }
        public DateTime SentDate { get; set; }

        [Required, Display(Name = "Tytuł wiadomości")]
        public string Title { get; set; }

        [Required, Display(Name = "Do (adres email):"), EmailAddress]
        public string RecipientAddress { get; set; }

        [Display(Name = "DW (adres email):"), EmailAddress]
        public string CcAddress { get; set; }

        [Required, Display(Name = "Treść wiadomości:")]
        [AllowHtml]
        public string Message { get; set; }
        

        [Required]
        [ForeignKey("User")]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        [Required, Display(Name = "Email nadawcy:")]
        public int EmailParamsId { get; set; }
        public EmailParams EmailParams { get; set; }
    }
}