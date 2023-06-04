using MailSenderApp.Models.Domains;
using MailSenderApp.Models.Repositories;
using Microsoft.AspNet.Identity;
using Rotativa;
using Rotativa.Options;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace MailSenderApp.Controllers
{
    public class PrintController : Controller
    {
        private EmailRepository _emailRepository = new EmailRepository();
        [Authorize]
        
        public ActionResult EmailToPdf(int id)
        {
            var handle = Guid.NewGuid().ToString();
            var userId = User.Identity.GetUserId();
            var email = _emailRepository.GetEmail(id, userId);

            TempData[handle] = GetPdfContent(email);

            return Json(new
            {
                FileGuid = handle,
                FileName = $@"Email_{email.Id}.pdf"
            });
        }

        private byte[] GetPdfContent(Email email)
        {
            var pdfResult = new ViewAsPdf(@"PrintEmail", email)
            {
                PageSize = Rotativa.Options.Size.A4,
                PageOrientation = Rotativa.Options.Orientation.Portrait
            };

            return pdfResult.BuildFile(ControllerContext);
        }

        public ActionResult DownloadEmailPdf(string fileGuid, string fileName)
        {
            if (TempData[fileGuid] == null)
                throw new Exception("Błąd przy próbie eksportu email do PDF.");

            var data = TempData[fileGuid] as byte[];
            return File(data, "application/pdf", fileName);
        }

        public ActionResult PrintEmail(int id)
        {
            var userId = User.Identity.GetUserId();
            var emailToPrint = _emailRepository.GetEmail(id, userId);

            return View("PrintEmail", emailToPrint);
        }

    }
}

