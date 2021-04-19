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
    public class ContactUsController : Controller
    {
        private SymphonyDBEntities db = new SymphonyDBEntities();

        // GET: ContactUs
        public async Task<ActionResult> Index()
        {
            ViewBag.Branch = await db.tblBranches.FirstOrDefaultAsync();
            ViewBag.BranchList = await db.tblBranches.ToListAsync();
            return View();
        }

        // GET: ContactUs/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblContactU tblContactU = await db.tblContactUs.FindAsync(id);
            if (tblContactU == null)
            {
                return HttpNotFound();
            }
            return View(tblContactU);
        }

        // GET: ContactUs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ContactUs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ContactID,Name,Email,Subject,Message,CreatedOn")] tblContactU tblContactU)
        {
            if (ModelState.IsValid)
            {
                db.tblContactUs.Add(tblContactU);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(tblContactU);
        }

        // GET: ContactUs/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblContactU tblContactU = await db.tblContactUs.FindAsync(id);
            if (tblContactU == null)
            {
                return HttpNotFound();
            }
            return View(tblContactU);
        }

        // POST: ContactUs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ContactID,Name,Email,Subject,Message,CreatedOn")] tblContactU tblContactU)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblContactU).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(tblContactU);
        }

        // GET: ContactUs/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblContactU tblContactU = await db.tblContactUs.FindAsync(id);
            if (tblContactU == null)
            {
                return HttpNotFound();
            }
            return View(tblContactU);
        }

        // POST: ContactUs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            tblContactU tblContactU = await db.tblContactUs.FindAsync(id);
            db.tblContactUs.Remove(tblContactU);
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
