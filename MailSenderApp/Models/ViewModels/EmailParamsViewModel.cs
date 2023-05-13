using MailSenderApp.Models.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MailSenderApp.Models.ViewModels
{
    public class EmailParamsViewModel
    {
        public EmailParams EmailParameters { get; set; }
        public string Heading { get; set; }
    }
}