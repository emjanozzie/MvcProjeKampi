using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcProjeKampi.Controllers
{
    public class ContactController : Controller
    {
        // GET: Contact
        ContactManager cm = new ContactManager(new EfContactDal());
        MessageManager mm = new MessageManager(new EfMessageDal());
        ContactValidator validationRules = new ContactValidator();
        public ActionResult Index()
        {
            var contactvalue = cm.GetList();
            return View(contactvalue);
        }

        public ActionResult GetContactDetail(int id)
        {
            var contactinfo=cm.GetById(id);
            return View(contactinfo);
        }

        public PartialViewResult ContactPartial()
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