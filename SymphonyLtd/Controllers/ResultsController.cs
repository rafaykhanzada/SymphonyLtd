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
    public class ResultsController : Controller
    {
        private SymphonyDBEntities db = new SymphonyDBEntities();

        // GET: Results
        public async Task<ActionResult> Index()
        {
            var tblResults = db.tblResults.Include(t => t.tblExam).Include(t => t.tblUser).Include(t => t.tblUser1);
            return View(await tblResults.ToListAsync());
        }

        // GET: Results/Details/5
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

        // GET: Results/Create
        public ActionResult Create()
        {
            ViewBag.ExamID = new SelectList(db.tblExams, "ExamID", "ExamName");
            ViewBag.DeletedBy = new SelectList(db.tblUsers, "UserID", "Name");
            ViewBag.StudentID = new SelectList(db.tblUsers, "UserID", "Name");
            return View();
        }

        // POST: Results/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ResultID,ExamID,ObtainNo,TotalMarks,Remarks,IsFailed,CreatedOn,ModifiedOn,DeletedOn,DeletedBy,StudentID")] tblResult tblResult)
        {
            if (ModelState.IsValid)
            {
                db.tblResults.Add(tblResult);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.ExamID = new SelectList(db.tblExams, "ExamID", "ExamName", tblResult.ExamID);
            ViewBag.DeletedBy = new SelectList(db.tblUsers, "UserID", "Name", tblResult.DeletedBy);
            ViewBag.StudentID = new SelectList(db.tblUsers, "UserID", "Name", tblResult.StudentID);
            return View(tblResult);
        }

        // GET: Results/Edit/5
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
            ViewBag.DeletedBy = new SelectList(db.tblUsers, "UserID", "Name", tblResult.DeletedBy);
            ViewBag.StudentID = new SelectList(db.tblUsers, "UserID", "Name", tblResult.StudentID);
            return View(tblResult);
        }

        // POST: Results/Edit/5
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
            ViewBag.DeletedBy = new SelectList(db.tblUsers, "UserID", "Name", tblResult.DeletedBy);
            ViewBag.StudentID = new SelectList(db.tblUsers, "UserID", "Name", tblResult.StudentID);
            return View(tblResult);
        }

        // GET: Results/Delete/5
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

        // POST: Results/Delete/5
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
