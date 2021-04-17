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
using SymphonyLtd.Security;

namespace SymphonyLtd.Areas.Admin.Controllers
{
    [FormAuthentication(RoleId = "1")]

    public class CountriesController : Controller
    {
        private SymphonyDBEntities db = new SymphonyDBEntities();

        // GET: Admin/Countries
        public async Task<ActionResult> Index()
        {
            return View(await db.tblCountries.ToListAsync());
        }

        // GET: Admin/Countries/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblCountry tblCountry = await db.tblCountries.FindAsync(id);
            if (tblCountry == null)
            {
                return HttpNotFound();
            }
            return View(tblCountry);
        }

        // GET: Admin/Countries/Create
        public async Task<ActionResult> Create(int? id)
        {
            if (id == null)
            {
                return View();
            }
            tblCountry tblCountry = await db.tblCountries.FindAsync(id);
            if (tblCountry == null)
            {
                return HttpNotFound();
            }
            return View(tblCountry);
        }

        // POST: Admin/Countries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "CountryID,ISO,Name,PhoneCode")] tblCountry tblCountry)
        {
            if (tblCountry.CountryID==0)
            {
                db.tblCountries.Add(tblCountry);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            if (tblCountry.CountryID > 0)
            {
                db.Entry(tblCountry).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(tblCountry);
        }


        // GET: Admin/Countries/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblCountry tblCountry = await db.tblCountries.FindAsync(id);
            if (tblCountry == null)
            {
                return HttpNotFound();
            }
            return View(tblCountry);
        }

        // POST: Admin/Countries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            tblCountry tblCountry = await db.tblCountries.FindAsync(id);
            db.tblCountries.Remove(tblCountry);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
