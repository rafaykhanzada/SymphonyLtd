using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SymphonyLtd.Models;

namespace SymphonyLtd.Controllers
{
    public class AboutUsController : Controller
    {
        private SymphonyDBEntities db = new SymphonyDBEntities();

        // GET: AboutUs
        public async Task<ActionResult> Index()
        {
            ViewBag.Partners = await db.tblPartners.ToListAsync();
            return View(await db.tblAboutUs.FirstOrDefaultAsync());
        }
    }
}
