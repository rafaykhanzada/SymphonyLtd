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
    public class PublicFAQsController : Controller
    {
        private SymphonyDBEntities db = new SymphonyDBEntities();

        // GET: FAQs
        public async Task<ActionResult> Index()
        {
            var tblFAQs = db.tblFAQs.Include(t => t.tblFAQType);
            return View(await tblFAQs.ToListAsync());
        }

        // GET: FAQs/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblFAQ tblFAQ = await db.tblFAQs.FindAsync(id);
            if (tblFAQ == null)
            {
                return HttpNotFound();
            }
            return View(tblFAQ);
        }

        // GET: FAQs/Create
        public ActionResult Create()
        {
            ViewBag.FAQType_FK = new SelectList(db.tblFAQTypes, "FAQTypeID", "FAQTypeName");
            return View();
        }

        // POST: FAQs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "FAQID,Question,Answer,FAQType_FK,IsActive,CreatedOn,ModifiedOn,DeletedBy,DeletedOn")] tblFAQ tblFAQ)
        {
            if (ModelState.IsValid)
            {
                db.tblFAQs.Add(tblFAQ);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.FAQType_FK = new SelectList(db.tblFAQTypes, "FAQTypeID", "FAQTypeName", tblFAQ.FAQType_FK);
            return View(tblFAQ);
        }

        // GET: FAQs/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblFAQ tblFAQ = await db.tblFAQs.FindAsync(id);
            if (tblFAQ == null)
            {
                return HttpNotFound();
            }
            ViewBag.FAQType_FK = new SelectList(db.tblFAQTypes, "FAQTypeID", "FAQTypeName", tblFAQ.FAQType_FK);
            return View(tblFAQ);
        }

        // POST: FAQs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "FAQID,Question,Answer,FAQType_FK,IsActive,CreatedOn,ModifiedOn,DeletedBy,DeletedOn")] tblFAQ tblFAQ)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblFAQ).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.FAQType_FK = new SelectList(db.tblFAQTypes, "FAQTypeID", "FAQTypeName", tblFAQ.FAQType_FK);
            return View(tblFAQ);
        }

        // GET: FAQs/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblFAQ tblFAQ = await db.tblFAQs.FindAsync(id);
            if (tblFAQ == null)
            {
                return HttpNotFound();
            }
            return View(tblFAQ);
        }

        // POST: FAQs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            tblFAQ tblFAQ = await db.tblFAQs.FindAsync(id);
            db.tblFAQs.Remove(tblFAQ);
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
