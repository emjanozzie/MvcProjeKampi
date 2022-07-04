using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcProjeKampi.Controllers
{
    public class ContentController : Controller
    {
        ContentManager contentManager = new ContentManager(new EfContentDal());
        // GET: Content
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ContentByHeading(int id)
        {
            var contentvalues = contentManager.GetListByHeadingId(id);
            return View(contentvalues);
        }


        public ActionResult GetAllList(string p)
        {        
            var searchList = contentManager.GetAllList(p);    
            var allList = contentManager.GetList();  //Arama yapmadığımızda da tüm veriler gelsin istiyoruz ilk başta p değeri boş geliyor
            if (p!=null)
            {
                return View(searchList);
            }
            return View(allList);
        }
    }
}