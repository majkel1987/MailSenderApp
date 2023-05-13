using MailSenderApp.Models.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MailSenderApp.Models.Repositories
{
    public class EmailParametersRepository
    {
        public List<EmailParams> GetEmailParameters(string userId)
        {
            using (var context = new ApplicationDbContext())
            {
                return context.EmailParameters.Where(x => x.UserId == userId).ToList();
            }
        }

        public EmailParams GetEmailParameters(int id, string userId)
        {
            using (var context = new ApplicationDbContext())
            {
                return context.EmailParameters.Single(x => x.Id == id && x.UserId == userId);
            }
        }

        public void AddNewAccount(EmailParams emailParameters)
        {
            using (var context = new ApplicationDbContext())
            {
                context.EmailParameters.Add(emailParameters);
                context.SaveChanges();
            }
        }

        public void UpdateAccount(EmailParams emailParameters)
        {
            using (var context = new ApplicationDbContext())
            {
                var emailParametersToUpdate = context.EmailParameters
                     .Single(x => x.Id == emailParameters.Id && x.UserId == emailParameters.UserId);
                
                emailParametersToUpdate.Port = emailParameters.Port;
                emailParametersToUpdate.HostSmtp = emailParameters.HostSmtp;
                emailParametersToUpdate.SenderEmail = emailParameters.SenderEmail;
                emailParametersToUpdate.SenderEmailPassword = emailParameters.SenderEmailPassword;
                emailParametersToUpdate.SenderName = emailParameters.SenderName;
                emailParametersToUpdate.EnableSsl = emailParameters.EnableSsl;

                context.SaveChanges();
            }
        }

        public void Delete(int id, string userId)
        {
            using (var context = new ApplicationDbContext())
            {
                var accountToDelete = context.EmailParameters.Single(x => x.Id == id && x.UserId == userId);
                context.EmailParameters.Remove(accountToDelete);
                context.SaveChanges();
            }
        }
    }
}