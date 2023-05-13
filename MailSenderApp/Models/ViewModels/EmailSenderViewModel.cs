using MailSenderApp.Models.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MailSenderApp.Models.ViewModels
{
    public class EmailSenderViewModel
    {
        public Email Email { get; set; }
        public List<EmailParams> EmailParameters { get; set; }
        public string Heading { get; set; }
    }
}