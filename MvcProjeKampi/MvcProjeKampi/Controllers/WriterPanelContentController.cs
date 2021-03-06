using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcProjeKampi.Controllers
{
    public class WriterPanelContentController : Controller
    {
        // GET: WriterPanelContent
        ContentManager cm = new ContentManager(new EfContentDal());
        Context c = new Context();
        public ActionResult MyContent(string p)
        {
            p = (string)Session["WriterMail"];
            var writerLoginId = c.Writers.Where(x => x.WriterMail == p).Select(y => y.WriterID).FirstOrDefault();
            var contentvalues = cm.GetListByWriter(writerLoginId);
            return View(contentvalues);
        }

        [HttpGet]
        public ActionResult AddContent(int id)
        {
            ViewBag.hi = id;
            return View();
        }

        [HttpPost]
        public ActionResult AddContent(Content p)
        {
            p.ContentDate = DateTime.Parse(DateTime.Now.ToShortDateString());
            string mail = (string)Session["WriterMail"];
            p.WriterID = c.Writers.Where(x => x.WriterMail == mail).Select(y => y.WriterID).FirstOrDefault();
            p.ContentStatus = true;
            cm.ContentAdd(p);

            return RedirectToAction("MyContent");
        }
    }
}