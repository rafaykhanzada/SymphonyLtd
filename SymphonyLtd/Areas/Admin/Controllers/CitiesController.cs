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

namespace SymphonyLtd.Areas.Admin.Controllers
{
    public class CitiesController : Controller
    {
        private SymphonyDBEntities db = new SymphonyDBEntities();

        // GET: Admin/Cities
        public async Task<ActionResult> Index()
        {
            var tblCities = db.tblCities.Include(t => t.tblState);
            return View(await tblCities.ToListAsync());
        }

        // GET: Admin/Cities/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblCity tblCity = await db.tblCities.FindAsync(id);
            if (tblCity == null)
            {
                return HttpNotFound();
            }
            return View(tblCity);
        }

        // GET: Admin/Cities/Create
        public async Task<ActionResult> Create(int? id)
        {
            if (id == null)
            {
                ViewBag.State_FK = new SelectList(db.tblStates, "StateID", "StateName");
                return View();
            }
            tblCity tblCity = await db.tblCities.FindAsync(id);
            if (tblCity == null)
            {
                return HttpNotFound();
            }
            ViewBag.State_FK = new SelectList(db.tblStates, "StateID", "StateName", tblCity.State_FK);
            return View(tblCity);
        }

        // POST: Admin/Cities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<ActionResult> Create([Bind(Include = "CityID,CityName,State_FK,IsActive")] tblCity tblCity)
        {
            if (tblCity.CityID == 0)
            {
                tblCity.CreatedOn = DateTime.Now;
                db.tblCities.Add(tblCity);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            if (tblCity.CityID > 0)
            {
                tblCity.ModifiedOn = DateTime.Now;
                db.Entry(tblCity).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.State_FK = new SelectList(db.tblStates, "StateID", "StateName", tblCity.State_FK);
            return View(tblCity);
        }

        // GET: Admin/Cities/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblCity tblCity = await db.tblCities.FindAsync(id);
            if (tblCity == null)
            {
                return HttpNotFound();
            }
            return View(tblCity);
        }

        // POST: Admin/Cities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            tblCity tblCity = await db.tblCities.FindAsync(id);
            db.tblCities.Remove(tblCity);
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
