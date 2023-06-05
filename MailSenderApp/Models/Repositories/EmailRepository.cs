using MailSenderApp.Models.Domains;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace MailSenderApp.Models.Repositories
{
    public class EmailRepository
    {
        public List<Email> GetEmails(string userId)
        {
            using (var context = new ApplicationDbContext())
            {
                return context.Emails.Where(x => x.UserId == userId).ToList();
            }
        }

        public Email GetEmail(int id, string userId)
        {
            using (var context = new ApplicationDbContext())
            {
                return context.Emails.Include(x => x.EmailParams).Single(x => x.Id == id && x.UserId == userId);
            }
        }

        public void AddMail(Email email)
        {
            using (var context = new ApplicationDbContext())
            {
              
                    var emailDb = new Email();
                    emailDb.SentDate = DateTime.Now;
                    emailDb.CcAddress = email.CcAddress;
                    emailDb.RecipientAddress = email.RecipientAddress;
                    emailDb.UserId = email.UserId;
                    emailDb.Title = email.Title;
                    emailDb.Message = email.Message;
                    emailDb.EmailParamsId = email.EmailParams.Id;

                    context.Emails.Add(emailDb);
                    context.SaveChanges();
  
            }
        }

        public void Delete(int id, string userId)
        {
            using (var context = new ApplicationDbContext())
            {
                var emailToDelete = context.Emails.Single(x => x.Id == id && x.UserId == userId);
                context.Emails.Remove(emailToDelete);
                context.SaveChanges();
            }
        }
    }
}