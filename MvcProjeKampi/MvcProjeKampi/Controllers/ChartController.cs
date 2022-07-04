using MvcProjeKampi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcProjeKampi.Controllers
{
    public class ChartController : Controller
    {
        // GET: Chart
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CategoryChart()
        {
            return Json(BlogList(), JsonRequestBehavior.AllowGet);
        }

        public List<CategoryClass> BlogList()
        {
            List<CategoryClass> cc = new List<CategoryClass>();
            cc.Add(new CategoryClass()
            {
                CategoryCount=10,
                CategoryName="Yazılım"
            });
            cc.Add(new CategoryClass()
            {
                CategoryCount = 1,
                CategoryName = "Spor"
            });
            cc.Add(new CategoryClass()
            {
                CategoryCount = 7,
                CategoryName = "Teknoloji"
            });
            cc.Add(new CategoryClass()
            {
                CategoryCount = 4,
                CategoryName = "Seyehat"
            });

            return cc;
        }
    }
}