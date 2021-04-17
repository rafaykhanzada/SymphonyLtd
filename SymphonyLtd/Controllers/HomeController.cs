using SymphonyLtd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SymphonyLtd.Controllers
{
    public class HomeController : Controller

    { 
         private SymphonyDBEntities db = new SymphonyDBEntities();
        public ActionResult Index()
        {
            ViewBag.HomePageBoxes = db.tblHomePageBoxes.Where(x => x.IsActive == true).ToList();
            ViewBag.NewsItem = db.tblNewsItems.Where(x => x.IsActive == true).ToList();
            ViewBag.Partners = db.tblPartners.Where(x => x.IsActive == true).ToList();
            ViewBag.Teachers = db.tblTeachers.Where(x => x.IsActive == true).ToList();
            ViewBag.AboutUs = db.tblAboutUs.FirstOrDefault();
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}