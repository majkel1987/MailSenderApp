using MailSenderApp.Models;
using MailSenderApp.Models.Domains;
using MailSenderApp.Models.Repositories;
using MailSenderApp.Models.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MailSenderApp.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private EmailRepository _emailRepository = new EmailRepository();
        private EmailParametersRepository _emailParametersRepository = new EmailParametersRepository();
        private EmailSender _emailSender;
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        [Authorize]
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var emails = _emailRepository.GetEmails(userId);
            return View(emails);
        }
        public ActionResult Email(int id = 0)
        {
            var userId = User.Identity.GetUserId();
            var email = id == 0 ?
                SentEmail(userId) : _emailRepository.GetEmail(id, userId);
            
            var vm = PrepareEmailVm(email, userId);

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> NewEmail(Email email)
        {
            var userId = User.Identity.GetUserId();
            var emailParameters = _emailParametersRepository.GetEmailParameters(email.EmailParams.Id, userId);
            try
            {
                _emailSender = new EmailSender(emailParameters);
                await _emailSender.Send(email);
                _emailRepository.AddMail(email);

            }
            catch (Exception ex)
            {
                Logger.Error(ex, ex.Message);
            
            }
            _emailRepository.AddMail(email);
            return RedirectToAction("Index");
        
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EmailParameters(EmailParams emailParameters)
        {
            var userId = User.Identity.GetUserId();
            emailParameters.UserId = userId;

            if (emailParameters.Id == 0)
                _emailParametersRepository.AddNewAccount(emailParameters);
            else
                _emailParametersRepository.UpdateAccount(emailParameters);

            return RedirectToAction("Accounts");

        }
        private EmailSenderViewModel PrepareEmailVm(Email email, string userId)
        {
            return new EmailSenderViewModel
            {
                Email = email,
                Heading = email.Id == 0 ? "Nowa wiadomość" : "Wysłana wiadomość",
                EmailParameters = _emailParametersRepository.GetEmailParameters(userId)
            };
        }

        private Email SentEmail(string userId)
        {
            return new Email 
            { 
                UserId = userId,
                SentDate = DateTime.Now,
            };
        }

        public ActionResult Accounts()
        {
            var userId = User.Identity.GetUserId();
            var emailParameters = _emailParametersRepository.GetEmailParameters(userId);
            return View(emailParameters);
        }
        public ActionResult EmailParameters(int id = 0)
        {
            var userId = User.Identity.GetUserId();
            var emailParameters = id == 0 ?
                AddEmailParameters(userId) : _emailParametersRepository.GetEmailParameters(id, userId);

            var vm = PrepareEmailParametersVm(emailParameters, userId);

            return View(vm);
        }

        private EmailParams AddEmailParameters(string userId)
        {
            return new EmailParams
            {
                UserId = userId
            };
        }

        private EmailParamsViewModel PrepareEmailParametersVm(EmailParams emailParameters, string userId)
        {
            return new EmailParamsViewModel
            {
                EmailParameters = emailParameters,
                Heading = emailParameters.Id == 0 ? "Dodaj nowe konto" : "Edycja konta"
            };
        }

        [HttpPost]
        public ActionResult DeleteEmail(int id)
        {
            try
            {
                var userId = User.Identity.GetUserId();
                _emailRepository.Delete(id, userId);
            }
            catch (Exception exception)
            {
                //NLOG logowanie
                return Json(new { Success = false, Message = exception.Message });
            }
            return Json(new { Success = true });
        }

        [HttpPost]
        public ActionResult DeleteAccount(int id)
        {
            try
            {
                var userId = User.Identity.GetUserId();
                _emailParametersRepository.Delete(id, userId);
            }
            catch (Exception exception)
            {
                //NLOG logowanie
                return Json(new { Success = false, Message = exception.Message });
            }
            return Json(new { Success = true });
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}