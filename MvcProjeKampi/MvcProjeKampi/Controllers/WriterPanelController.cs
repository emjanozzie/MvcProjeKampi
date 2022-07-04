using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using BusinessLayer.ValidationRules;
using FluentValidation.Results;

namespace MvcProjeKampi.Controllers
{
    public class WriterPanelController : Controller
    {
        // GET: WriterPanel
        HeadingManager hm = new HeadingManager(new EfHeadingDal());
        CategoryManager cm = new CategoryManager(new EfCategoryDal());
        WriterManager wm = new WriterManager(new EfWriterDal());
        
        Context c = new Context();
   
        [HttpGet]
        public ActionResult WriterProfile()
        {
            var loginMail = (string)Session["WriterMail"];
            var loginId = c.Writers.Where(x => x.WriterMail == loginMail).Select(y => y.WriterID).FirstOrDefault();
            var loginInfo = wm.GetById(loginId);
            return View(loginInfo);
        }

        [HttpPost]
        public ActionResult WriterProfile(Writer p)
        {
            WriterValidator categoryValidator = new WriterValidator();
            ValidationResult results = categoryValidator.Validate(p);
            if (results.IsValid)
            {
                wm.WriterUpdate(p);
                return RedirectToAction("AllHeading","WriterPanel");
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

        public ActionResult MyHeading(string p)
        {
            p = (string)Session["WriterMail"];
            var writerIdInfo = c.Writers.Where(x => x.WriterMail == p).Select(y => y.WriterID).FirstOrDefault();
            var value=hm.GetListByWriter(writerIdInfo);
            return View(value);
        }

        [HttpGet]
        public ActionResult AddNewHeading()
        {
            List<SelectListItem> valuecategory = (from x in cm.GetList()   //Bu kısımda dropdownlistte gösterilecek değerleri alıyoruz
                                                  select new SelectListItem
                                                  {
                                                      Text = x.CategoryName,
                                                      Value = x.CategoryID.ToString()
                                                  }).ToList();

            ViewBag.vlc = valuecategory;     //Bu sayede view tarafına bunu taşıyabiliriz.
            return View();
        }

        [HttpPost]
        public ActionResult AddNewHeading(Heading p,string mail)
        {
            mail = (string)Session["WriterMail"];
            var WriterFindId = c.Writers.Where(x => x.WriterMail == mail).Select(y => y.WriterID).FirstOrDefault();//Writer maile göre writer id getiriyor
            p.HeadingDate = DateTime.Parse(DateTime.Now.ToShortDateString());
            p.WriterID = WriterFindId;
            p.HeadingStatus = true;
            hm.HeadingAdd(p);
            return RedirectToAction("MyHeading");
        }

        [HttpGet]
        public ActionResult EditNewHeading(int id)
        {
            var editvalues = hm.GetById(id);

            List<SelectListItem> valueheading = (from x in cm.GetList()
                                                 select new SelectListItem
                                                 {
                                                     Text = x.CategoryName,
                                                     Value = x.CategoryID.ToString()
                                                 }).ToList();

            ViewBag.vlh = valueheading;

            return View(editvalues);
        }

        [HttpPost]
        public ActionResult EditNewHeading(Heading p)
        {
            hm.HeadingUpdate(p);
            return RedirectToAction("MyHeading");
        }

        public ActionResult DeleteNewHeading(int id)
        {
            var headingvalue = hm.GetById(id);
            headingvalue.HeadingStatus = false;
            hm.HeadingDelete(headingvalue);
            return RedirectToAction("MyHeading");
        }

        public ActionResult AllHeading(int initialPage1=1) {

            var headingAllList = hm.GetList().ToPagedList(initialPage1, 4);
            return View(headingAllList);
        }
    }
}