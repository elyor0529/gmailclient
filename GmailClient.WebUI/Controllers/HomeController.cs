using GmailClient.Framework.Configurations;
using GmailClient.Framework.Interfaces;
using GmailClient.WebUI.BLL;
using GmailClient.WebUI.BLL.Attributes;
using GmailClient.WebUI.BLL.Controllers;
using GmailClient.WebUI.BLL.Extensions;
using GmailClient.WebUI.BLL.Helpers;
using GmailClient.WebUI.ViewModels;
using S22.Imap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GmailClient.WebUI.Controllers
{
    [Authorize]
    [CheckPassword]
    public class HomeController : BaseController
    {
       
        /// <summary>
        /// filter by filter model
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        private MessageListViewModel DoFilter(MessageFilterViewModel filter)
        {
            var client = AutofactHelper.GetClient<IIMAPClient>();
            var model = new MessageListViewModel
            {
                Items = client.GetMails(filter.Mailbox, filter.LastDate, filter.Keyword).Select(s => new MessageViewModel
                {
                    UId = s.UId,
                    Date = s.Mail.Date().ToEmailDate(),
                    Readed = s.IsSeen,
                    From = s.Mail.From.DisplayName,
                    Subject = String.IsNullOrWhiteSpace(s.Mail.Subject) ? IMAPConfig.NO_SUBJECT_TEXT : s.Mail.Subject
                }),
                Filter = filter
            };

            Logger.InfoFormat("Filter {0} messages", model.Items.Count());

            return model;
        }


        /// <summary>
        /// Load viewbags
        /// </summary>
        /// <param name="filter">filter model</param>
        private void LoadViewBags(MessageFilterViewModel filter = null)
        {
            var client = AutofactHelper.GetClient<IIMAPClient>();

            ViewBag.Mailboxes = client.GetBoxes();

            if (filter != null)
            {
                ViewBag.Mailbox = filter.Mailbox;
                ViewBag.Filter = filter;
            }

        }

        /// <summary>
        /// Home page
        /// </summary>
        /// <param name="mailbox"></param>
        /// <returns></returns>
        public ActionResult Index(string mailbox = IMAPConfig.DEFAULT_MAIL_BOX)
        {
            var filter = new MessageFilterViewModel
            {
                Mailbox = mailbox
            };
            var model = DoFilter(filter);

            LoadViewBags(filter);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(MessageFilterViewModel filter)
        {
            if (ModelState.IsValid)
            {
                var model = DoFilter(filter);

                LoadViewBags(filter);

                return View(model);
            }

            return RedirectToAction("Index");
        }


        public ActionResult Compose()
        {
            LoadViewBags();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Compose(MessageViewModel model)
        {
            if (ModelState.IsValid)
            {
                var client = AutofactHelper.GetClient<ISMTPClient>();

                if (client.SendMessage(model.Subject, model.Body, model.To))
                {
                    TempData[Settings.KEYS.TMP_SUCCESS_KEY] = "Sended successfully message";

                    Logger.InfoFormat("Sended message to {1}", model.Body);

                    return RedirectToAction("Index");
                }

                ModelState.AddModelError("", "Failed");
            }

            LoadViewBags();

            return View(model);
        }


        public ActionResult Details(uint id, string mailbox = IMAPConfig.DEFAULT_MAIL_BOX)
        {
            var client = AutofactHelper.GetClient<IIMAPClient>();

            //set a read status
            client.Read(id, mailbox);

            Logger.InfoFormat("Readed #{0} message from {1}", id, mailbox);

            var message = client.GetMail(id, mailbox);
            var model = new MessageViewModel
            {
                UId = message.UId,
                Date = message.Mail.Date().ToEmailDate(),
                Readed = message.IsSeen,
                From = message.Mail.From.ToString(),
                Subject = String.IsNullOrWhiteSpace(message.Mail.Subject) ? IMAPConfig.NO_SUBJECT_TEXT : message.Mail.Subject,
                Body = message.Mail.Body
            };

            LoadViewBags();

            ViewBag.Mailbox = mailbox;

            return View(model);
        }

        public ActionResult Delete(uint id, string mailbox = IMAPConfig.DEFAULT_MAIL_BOX)
        {
            var client = AutofactHelper.GetClient<IIMAPClient>();

            if (client.Delete(id, mailbox))
            {
                TempData[Settings.KEYS.TMP_SUCCESS_KEY] = "Deleted successfully message";

                Logger.InfoFormat("Deleted #{0} message from {1}", id, mailbox);

                return RedirectToAction("Index");
            }

            return View("Error");
        }

        [AllowAnonymous]
        public ActionResult About()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult Contact()
        {
            return View();
        }
    }
}