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
    public class HomePageBoxesController : Controller
    {
        private SymphonyDBEntities db = new SymphonyDBEntities();

        // GET: Admin/HomePageBoxes
        public async Task<ActionResult> Index()
        {
            return View(await db.tblHomePageBoxes.ToListAsync());
        }

        // GET: Admin/HomePageBoxes/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblHomePageBox tblHomePageBox = await db.tblHomePageBoxes.FindAsync(id);
            if (tblHomePageBox == null)
            {
                return HttpNotFound();
            }
            return View(tblHomePageBox);
        }

        // GET: Admin/HomePageBoxes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/HomePageBoxes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "HomePageBoxID,HeaderText,ShortDesc,Icon,IsActive,CreatedOn,ModifiedOn,DeletedBy,DeletedOn")] tblHomePageBox tblHomePageBox)
        {
            if (ModelState.IsValid)
            {
                db.tblHomePageBoxes.Add(tblHomePageBox);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(tblHomePageBox);
        }

        // GET: Admin/HomePageBoxes/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblHomePageBox tblHomePageBox = await db.tblHomePageBoxes.FindAsync(id);
            if (tblHomePageBox == null)
            {
                return HttpNotFound();
            }
            return View(tblHomePageBox);
        }

        // POST: Admin/HomePageBoxes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "HomePageBoxID,HeaderText,ShortDesc,Icon,IsActive,CreatedOn,ModifiedOn,DeletedBy,DeletedOn")] tblHomePageBox tblHomePageBox)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblHomePageBox).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(tblHomePageBox);
        }

        // GET: Admin/HomePageBoxes/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblHomePageBox tblHomePageBox = await db.tblHomePageBoxes.FindAsync(id);
            if (tblHomePageBox == null)
            {
                return HttpNotFound();
            }
            return View(tblHomePageBox);
        }

        // POST: Admin/HomePageBoxes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            tblHomePageBox tblHomePageBox = await db.tblHomePageBoxes.FindAsync(id);
            db.tblHomePageBoxes.Remove(tblHomePageBox);
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
