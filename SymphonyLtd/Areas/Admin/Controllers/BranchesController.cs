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
    public class BranchesController : Controller
    {
        private SymphonyDBEntities db = new SymphonyDBEntities();

        // GET: Admin/Branches
        public async Task<ActionResult> Index()
        {
            return View(await db.tblBranches.ToListAsync());
        }

        // GET: Admin/Branches/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblBranch tblBranch = await db.tblBranches.FindAsync(id);
            if (tblBranch == null)
            {
                return HttpNotFound();
            }
            return View(tblBranch);
        }

        // GET: Admin/Branches/Create
        public async Task<ActionResult> Create(int? id)
        {
            if (id == null)
            {               
                ViewBag.Country = new SelectList(db.tblCountries, "CountryID", "Name");
                ViewBag.State = new SelectList(db.tblStates, "StateID", "StateName");
                ViewBag.City = new SelectList(db.tblCities, "CityID", "CityName");

                return View();
            }
            tblBranch tblBranch = await db.tblBranches.FindAsync(id);
            if (tblBranch == null)
            {
                return HttpNotFound();
            }
            ViewBag.Country = new SelectList(db.tblCountries, "CountryID", "Name",tblBranch.Country);
            ViewBag.State = new SelectList(db.tblStates.Where(x=>x.Country_FK== tblBranch.Country), "StateID", "StateName", tblBranch.State);
            ViewBag.City = new SelectList(db.tblCities.Where(x=>x.State_FK== tblBranch.State), "CityID", "CityName", tblBranch.City);
            return View(tblBranch);
        }

        // POST: Admin/Branches/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "BranchID,BranchName,Email,Phone,City,State,Country,StreetAddress,Time")] tblBranch tblBranch)
        {
            if (tblBranch.BranchID==0)
            {
                db.tblBranches.Add(tblBranch);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            if (tblBranch.BranchID > 0)
            {
                db.Entry(tblBranch).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.Country = new SelectList(db.tblCountries, "CountryID", "Name", tblBranch.Country);
            ViewBag.State = new SelectList(db.tblStates.Where(x => x.Country_FK == tblBranch.Country), "StateID", "StateName", tblBranch.State);
            ViewBag.City = new SelectList(db.tblCities.Where(x => x.State_FK == tblBranch.State), "CityID", "CityName", tblBranch.City);
            return View(tblBranch);
        }

        // GET: Admin/Branches/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblBranch tblBranch = await db.tblBranches.FindAsync(id);
            if (tblBranch == null)
            {
                return HttpNotFound();
            }
            return View(tblBranch);
        }

        // POST: Admin/Branches/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "BranchID,BranchName,Email,Phone,City,State,Country,StreetAddress,Time")] tblBranch tblBranch)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblBranch).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(tblBranch);
        }

        // GET: Admin/Branches/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblBranch tblBranch = await db.tblBranches.FindAsync(id);
            if (tblBranch == null)
            {
                return HttpNotFound();
            }
            return View(tblBranch);
        }

        // POST: Admin/Branches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            tblBranch tblBranch = await db.tblBranches.FindAsync(id);
            db.tblBranches.Remove(tblBranch);
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
