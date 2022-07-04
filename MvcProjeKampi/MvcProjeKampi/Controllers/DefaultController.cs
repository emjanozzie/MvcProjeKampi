using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcProjeKampi.Controllers
{
    public class DefaultController : Controller
    {
        // GET: Default
        HeadingManager hm = new HeadingManager(new EfHeadingDal());
        ContentManager cm = new ContentManager(new EfContentDal());


        [AllowAnonymous]
        public ActionResult Headings()
        {
            var listheading = hm.GetList();
            return View(listheading);
        }
        public PartialViewResult Index(int id=0)
        {
            var ContentList = cm.GetListByHeadingId(id);
            return PartialView(ContentList);
        }
    }
}