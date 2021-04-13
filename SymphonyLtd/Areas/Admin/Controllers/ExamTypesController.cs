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
    public class ExamTypesController : Controller
    {
        private SymphonyDBEntities db = new SymphonyDBEntities();

        // GET: Admin/ExamTypes
        public async Task<ActionResult> Index()
        {
            var tblExamTypes = db.tblExamTypes.Include(t => t.tblUser);
            return View(await tblExamTypes.ToListAsync());
        }

        // GET: Admin/ExamTypes/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblExamType tblExamType = await db.tblExamTypes.FindAsync(id);
            if (tblExamType == null)
            {
                return HttpNotFound();
            }
            return View(tblExamType);
        }

        // GET: Admin/ExamTypes/Create
        public async Task<ActionResult> Create(int? id)
        {
            
            if (id == null)
            {
                return View();
            }
            tblExamType tblExamType = await db.tblExamTypes.FindAsync(id);
            if (tblExamType == null)
            {
                return HttpNotFound();
            }
            return View(tblExamType);
        }

        // POST: Admin/ExamTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(tblExamType tblExamType)
        {
            if (tblExamType.ExamTypeID==0)
            {
                db.tblExamTypes.Add(tblExamType);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            if (tblExamType.ExamTypeID > 0)
            {
                db.Entry(tblExamType).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(tblExamType);
        }

        // GET: Admin/ExamTypes/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblExamType tblExamType = await db.tblExamTypes.FindAsync(id);
            if (tblExamType == null)
            {
                return HttpNotFound();
            }
            ViewBag.DeleteBy = new SelectList(db.tblUsers, "UserID", "Name", tblExamType.DeleteBy);
            return View(tblExamType);
        }

        // POST: Admin/ExamTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ExamTypeID,ExamName,ExamFees,IsActive,CreatedOn,ModifiedOn,DeletedOn,DeleteBy")] tblExamType tblExamType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblExamType).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.DeleteBy = new SelectList(db.tblUsers, "UserID", "Name", tblExamType.DeleteBy);
            return View(tblExamType);
        }

        // GET: Admin/ExamTypes/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblExamType tblExamType = await db.tblExamTypes.FindAsync(id);
            if (tblExamType == null)
            {
                return HttpNotFound();
            }
            return View(tblExamType);
        }

        // POST: Admin/ExamTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            tblExamType tblExamType = await db.tblExamTypes.FindAsync(id);
            db.tblExamTypes.Remove(tblExamType);
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
