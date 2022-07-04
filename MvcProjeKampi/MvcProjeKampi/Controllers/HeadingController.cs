using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcProjeKampi.Controllers
{
    public class HeadingController : Controller
    {
        // GET: Heading
        HeadingManager hd = new HeadingManager(new EfHeadingDal());
        CategoryManager cm = new CategoryManager(new EfCategoryDal());
        WriterManager wm = new WriterManager(new EfWriterDal());
        public ActionResult Index()
        {
            var headingvalues= hd.GetList();
            return View(headingvalues);
        }

        public ActionResult HeadingReport()
        {
            var headingvalues = hd.GetList();
            return View(headingvalues);
        }

        [HttpGet]
        public ActionResult AddHeading()
        {
            List<SelectListItem> valuecategory = (from x in cm.GetList()   //Bu kısımda dropdownlistte gösterilecek değerleri alıyoruz
                                                  select new SelectListItem
                                                  {
                                                      Text = x.CategoryName,
                                                      Value = x.CategoryID.ToString()
                                                  }).ToList();

            List<SelectListItem> valuewriter = (from x in wm.GetList()
                                                select new SelectListItem
                                                {
                                                    Text=x.WriterName+" "+ x.WriterSurName,
                                                    Value=x.WriterID.ToString()
                                                }).ToList();

            ViewBag.vlc = valuecategory;     //Bu sayede view tarafına bunu taşıyabiliriz.
            ViewBag.vlw = valuewriter;
            return View();
        }
        [HttpPost]
        public ActionResult AddHeading(Heading p)
        {
            p.HeadingDate = DateTime.Parse(DateTime.Now.ToShortDateString());
            p.HeadingStatus = true;
            hd.HeadingAdd(p);
            return RedirectToAction("Index");
        }


        [HttpGet]
        public ActionResult EditHeading(int id)
        {
            var editvalues = hd.GetById(id);

            List<SelectListItem> valueheading = (from x in cm.GetList()
                                                  select new SelectListItem
                                                  {
                                                      Text=x.CategoryName,
                                                      Value=x.CategoryID.ToString()
                                                  }).ToList();

            ViewBag.vlh = valueheading;

            return View(editvalues);
        }

        [HttpPost]
        public ActionResult EditHeading(Heading p)
        {
            hd.HeadingUpdate(p);
            return RedirectToAction("Index");
        }

        public ActionResult DeleteHeading(int id)
        {
            var headingvalue = hd.GetById(id);
            headingvalue.HeadingStatus = false;
            hd.HeadingDelete(headingvalue);
            return RedirectToAction("Index");
        }

        
    }
}