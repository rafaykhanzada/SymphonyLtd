using SymphonyLtd.Models;
using SymphonyLtd.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SymphonyLtd.Areas.Admin.Controllers
{
    [FormAuthentication(RoleId = "1")]

    public class DashboardController : Controller
    {
        private SymphonyDBEntities db = new SymphonyDBEntities();

        // GET: Admin/Dashboard
        public ActionResult Index()
        {
            ViewBag.tblUsers = db.tblUsers.Where(x => x.UserRole_FK.Value == 2 && x.IsActive == true).Count();
            ViewBag.tblEnrollments = db.tblEnrollments.Count();
            ViewBag.tblResults = db.tblResults.Count();
            ViewBag.tblTopics = db.tblTopics.Count();
            return View();
        }
    }
}