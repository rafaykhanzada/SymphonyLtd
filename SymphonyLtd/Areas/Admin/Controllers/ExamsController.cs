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
    public class ExamsController : Controller
    {
        private SymphonyDBEntities db = new SymphonyDBEntities();

        // GET: Admin/Exams
        public async Task<ActionResult> Index()
        {
            var tblExams = db.tblExams.Include(t => t.tblExamType).Include(t => t.tblTopic).Include(t => t.tblUser);
            return View(await tblExams.ToListAsync());
        }

        // GET: Admin/Exams/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblExam tblExam = await db.tblExams.FindAsync(id);
            if (tblExam == null)
            {
                return HttpNotFound();
            }
            return View(tblExam);
        }

        // GET: Admin/Exams/Create
        public ActionResult Create()
        {
            ViewBag.ExamType_FK = new SelectList(db.tblExamTypes, "ExamTypeID", "ExamName");
            ViewBag.Topics_FK = new SelectList(db.tblTopics, "TopicID", "Topic");
            ViewBag.DeletedBy = new SelectList(db.tblUsers, "UserID", "Name");
            return View();
        }

        // POST: Admin/Exams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ExamID,ExamName,Topics_FK,ExamDuration,ExamScheduleFrom,ExamScheduleTo,ExamType_FK,TotalMarks,PassingMarks,IsValid,IsCancled,IsCancledReason,CreatedOn,ModifiedOn,DeleteOn,DeletedBy")] tblExam tblExam)
        {
            if (ModelState.IsValid)
            {
                db.tblExams.Add(tblExam);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.ExamType_FK = new SelectList(db.tblExamTypes, "ExamTypeID", "ExamName", tblExam.ExamType_FK);
            ViewBag.Topics_FK = new SelectList(db.tblTopics, "TopicID", "Topic", tblExam.Topics_FK);
            ViewBag.DeletedBy = new SelectList(db.tblUsers, "UserID", "Name", tblExam.DeletedBy);
            return View(tblExam);
        }

        // GET: Admin/Exams/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblExam tblExam = await db.tblExams.FindAsync(id);
            if (tblExam == null)
            {
                return HttpNotFound();
            }
            ViewBag.ExamType_FK = new SelectList(db.tblExamTypes, "ExamTypeID", "ExamName", tblExam.ExamType_FK);
            ViewBag.Topics_FK = new SelectList(db.tblTopics, "TopicID", "Topic", tblExam.Topics_FK);
            ViewBag.DeletedBy = new SelectList(db.tblUsers, "UserID", "Name", tblExam.DeletedBy);
            return View(tblExam);
        }

        // POST: Admin/Exams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ExamID,ExamName,Topics_FK,ExamDuration,ExamScheduleFrom,ExamScheduleTo,ExamType_FK,TotalMarks,PassingMarks,IsValid,IsCancled,IsCancledReason,CreatedOn,ModifiedOn,DeleteOn,DeletedBy")] tblExam tblExam)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblExam).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.ExamType_FK = new SelectList(db.tblExamTypes, "ExamTypeID", "ExamName", tblExam.ExamType_FK);
            ViewBag.Topics_FK = new SelectList(db.tblTopics, "TopicID", "Topic", tblExam.Topics_FK);
            ViewBag.DeletedBy = new SelectList(db.tblUsers, "UserID", "Name", tblExam.DeletedBy);
            return View(tblExam);
        }

        // GET: Admin/Exams/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblExam tblExam = await db.tblExams.FindAsync(id);
            if (tblExam == null)
            {
                return HttpNotFound();
            }
            return View(tblExam);
        }

        // POST: Admin/Exams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            tblExam tblExam = await db.tblExams.FindAsync(id);
            db.tblExams.Remove(tblExam);
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
