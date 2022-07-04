using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcProjeKampi.Controllers
{
    public class WriterPanelMessageController : Controller
    {
        // GET: WriterPanelMessage
        MessageManager mm = new MessageManager(new EfMessageDal());
        MessageValidator messagevalidator = new MessageValidator();
        ContactManager cm = new ContactManager(new EfContactDal());


        public ActionResult Inbox()
        {
            var mail = (string)Session["WriterMail"];
            var messagevalue = mm.GetListInbox(mail);
            return View(messagevalue);
        }

        public ActionResult SendBox()
        {
            var mail = (string)Session["WriterMail"];
            var messagevalue = mm.GetListSendbox(mail);
            return View(messagevalue);
        }

        public ActionResult GetInBoxMessageDetails(int id)
        {
            var values = mm.GetByID(id);
            return View(values);
        }

        public ActionResult GetSendBoxMessageDetails(int id)
        {
            var values = mm.GetByID(id);
            return View(values);
        }

        [HttpGet]
        public ActionResult NewMessage()
        {
            return View();
        }
        [HttpPost]
        public ActionResult NewMessage(Message p)
        {
            ValidationResult results = messagevalidator.Validate(p);
            var mail = (string)Session["WriterMail"];
            if (results.IsValid)
            {
                p.SenderMail = mail;
                p.MessageDate = DateTime.Parse(DateTime.Now.ToShortDateString());
                mm.MessageAdd(p);
                return RedirectToAction("SendBox");
            }
            else
            {
                foreach (var item in results.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }

            }
            return View();
        }
        public PartialViewResult WriterPartialView()
        {
            var mail = (string)Session["WriterMail"];
            var contactvalue = cm.GetList();
            var messagevalue = mm.GetListInbox(mail);
            var messagevalue2 = mm.GetListSendbox(mail);
            ViewBag.Index = contactvalue.Count();
            ViewBag.Inbox = messagevalue.Count();
            ViewBag.SendBox = messagevalue2.Count();
            return PartialView();
        }
    }
}