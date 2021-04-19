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
    public class ResultsController : Controller
    {
        private SymphonyDBEntities db = new SymphonyDBEntities();

        // GET: Admin/Results
        public async Task<ActionResult> Index()
        {
            var tblResults = db.tblResults.Include(t => t.tblExam).Include(t => t.tblUser);
            return View(await tblResults.ToListAsync());
        }

        // GET: Admin/Results/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblResult tblResult = await db.tblResults.FindAsync(id);
            if (tblResult == null)
            {
                return HttpNotFound();
            }
            return View(tblResult);
        }

        // GET: Admin/Results/Create
        public async Task<ActionResult> Create(int? id)
        {
           
            if (id == null)
            {
                ViewBag.ExamID = new SelectList(db.tblExams, "ExamID", "ExamName");
                ViewBag.StudentID = new SelectList(db.tblUsers, "UserID", "Name");
                return View();
            }
            tblResult tblResult = await db.tblResults.FindAsync(id);
            if (tblResult == null)
            {
                return HttpNotFound();
            }
            ViewBag.ExamID = new SelectList(db.tblExams, "ExamID", "ExamName", tblResult.ExamID);
            ViewBag.StudentID = new SelectList(db.tblUsers, "UserID", "Name", tblResult.StudentID);
            return View(tblResult);
        }

        // POST: Admin/Results/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<ActionResult> Create([Bind(Include = "ResultID,ExamID,ObtainNo,TotalMarks,Remarks,StudentID,IsFailed")] tblResult tblResult)
        {
            if (tblResult.ResultID == 0)
            {
                tblResult.CreatedOn = DateTime.Now;
                db.tblResults.Add(tblResult);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            if (tblResult.ResultID > 0)
            {
                tblResult.ModifiedOn = DateTime.Now;
                db.Entry(tblResult).State = EntityState.Modified;
                db.Entry(tblResult).Property(p => p.CreatedOn).IsModified = false;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.ExamID = new SelectList(db.tblExams, "ExamID", "ExamName", tblResult.ExamID);
            ViewBag.StudentID = new SelectList(db.tblUsers, "UserID", "Name", tblResult.StudentID);
            return View(tblResult);
        }

        // GET: Admin/Results/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblResult tblResult = await db.tblResults.FindAsync(id);
            if (tblResult == null)
            {
                return HttpNotFound();
            }
            ViewBag.ExamID = new SelectList(db.tblExams, "ExamID", "ExamName", tblResult.ExamID);
            ViewBag.StudentID = new SelectList(db.tblUsers, "UserID", "Name", tblResult.StudentID);
            return View(tblResult);
        }

        // POST: Admin/Results/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ResultID,ExamID,ObtainNo,TotalMarks,Remarks,IsFailed,CreatedOn,ModifiedOn,DeletedOn,DeletedBy,StudentID")] tblResult tblResult)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblResult).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.ExamID = new SelectList(db.tblExams, "ExamID", "ExamName", tblResult.ExamID);
            ViewBag.StudentID = new SelectList(db.tblUsers, "UserID", "Name", tblResult.StudentID);
            return View(tblResult);
        }

        // GET: Admin/Results/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblResult tblResult = await db.tblResults.FindAsync(id);
            if (tblResult == null)
            {
                return HttpNotFound();
            }
            return View(tblResult);
        }

        // POST: Admin/Results/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            tblResult tblResult = await db.tblResults.FindAsync(id);
            db.tblResults.Remove(tblResult);
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
