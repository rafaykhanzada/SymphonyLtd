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
    public class StatesController : Controller
    {
        private SymphonyDBEntities db = new SymphonyDBEntities();

        // GET: Admin/States
        public async Task<ActionResult> Index()
        {
            var tblStates = db.tblStates.Include(t => t.tblCountry);
            return View(await tblStates.ToListAsync());
        }

        // GET: Admin/States/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblState tblState = await db.tblStates.FindAsync(id);
            if (tblState == null)
            {
                return HttpNotFound();
            }
            return View(tblState);
        }

        // GET: Admin/States/Create
        public async Task<ActionResult> Create(int? id)
        {
            if (id == null)
            {
              ViewBag.Country_FK = new SelectList(db.tblCountries, "CountryID", "Name");
                return View();
            }
            tblState tblState = await db.tblStates.FindAsync(id);
            if (tblState == null)
            {
                return HttpNotFound();
            }
            ViewBag.Country_FK = new SelectList(db.tblCountries, "CountryID", "ISO", tblState.Country_FK);
            return View(tblState);
        }

        // POST: Admin/States/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<ActionResult> Create([Bind(Include = "StateID,StateName,Country_FK,IsActive")] tblState tblState)
        {
            if (tblState.StateID==0)
            {
                ViewBag.Country_FK = new SelectList(db.tblCountries, "CountryID", "Name");
                db.tblStates.Add(tblState);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            if (tblState.StateID > 0)
            {
                db.Entry(tblState).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.Country_FK = new SelectList(db.tblCountries, "CountryID", "Name", tblState.Country_FK);
            return View(tblState);
        }

        // GET: Admin/States/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblState tblState = await db.tblStates.FindAsync(id);
            if (tblState == null)
            {
                return HttpNotFound();
            }
            ViewBag.Country_FK = new SelectList(db.tblCountries, "CountryID", "ISO", tblState.Country_FK);
            return View(tblState);
        }

        // POST: Admin/States/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "StateID,StateName,Country_FK,IsActive,CreatedOn,ModifiedOn,DeletedBy,DeletedOn")] tblState tblState)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblState).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.Country_FK = new SelectList(db.tblCountries, "CountryID", "ISO", tblState.Country_FK);
            return View(tblState);
        }

        // GET: Admin/States/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblState tblState = await db.tblStates.FindAsync(id);
            if (tblState == null)
            {
                return HttpNotFound();
            }
            return View(tblState);
        }

        // POST: Admin/States/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            tblState tblState = await db.tblStates.FindAsync(id);
            db.tblStates.Remove(tblState);
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
