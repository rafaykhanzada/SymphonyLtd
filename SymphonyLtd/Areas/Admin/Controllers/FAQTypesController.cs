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
    public class FAQTypesController : Controller
    {
        private SymphonyDBEntities db = new SymphonyDBEntities();

        // GET: Admin/FAQTypes
        public async Task<ActionResult> Index()
        {
            return View(await db.tblFAQTypes.ToListAsync());
        }

        // GET: Admin/FAQTypes/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblFAQType tblFAQType = await db.tblFAQTypes.FindAsync(id);
            if (tblFAQType == null)
            {
                return HttpNotFound();
            }
            return View(tblFAQType);
        }

        // GET: Admin/FAQTypes/Create
        public async Task<ActionResult> Create(int? id)
        {
            if (id == null)
            {
            return View();
            }
            tblFAQType tblFAQType = await db.tblFAQTypes.FindAsync(id);
            if (tblFAQType == null)
            {
                return HttpNotFound();
            }
            return View(tblFAQType);
        }

        // POST: Admin/FAQTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<ActionResult> Create([Bind(Include = "FAQTypeID,FAQTypeName,IsActive")] tblFAQType tblFAQType)
        {
            if (tblFAQType.FAQTypeID == 0)
            {
                tblFAQType.CreatedOn = DateTime.Now;
                db.tblFAQTypes.Add(tblFAQType);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            if (tblFAQType.FAQTypeID > 0)
            {
                db.Entry(tblFAQType).State = EntityState.Modified;
                db.Entry(tblFAQType).Property(p => p.CreatedOn).IsModified = false;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(tblFAQType);
        }

        // GET: Admin/FAQTypes/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblFAQType tblFAQType = await db.tblFAQTypes.FindAsync(id);
            if (tblFAQType == null)
            {
                return HttpNotFound();
            }
            return View(tblFAQType);
        }

        // POST: Admin/FAQTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "FAQTypeID,FAQTypeName,IsActive,CreatedOn,ModifiedOn,DeletedBy,DeletedOn")] tblFAQType tblFAQType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblFAQType).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(tblFAQType);
        }

        // GET: Admin/FAQTypes/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblFAQType tblFAQType = await db.tblFAQTypes.FindAsync(id);
            if (tblFAQType == null)
            {
                return HttpNotFound();
            }
            return View(tblFAQType);
        }

        // POST: Admin/FAQTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            tblFAQType tblFAQType = await db.tblFAQTypes.FindAsync(id);
            db.tblFAQTypes.Remove(tblFAQType);
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
