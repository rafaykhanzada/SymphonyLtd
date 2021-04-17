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
using SymphonyLtd.ViewModels;
using SymphonyLtd.Security;

namespace SymphonyLtd.Areas.Admin.Controllers
{
    [FormAuthentication(RoleId = "1")]

    public class ExamsController : Controller
    {
        private SymphonyDBEntities db = new SymphonyDBEntities();

        // GET: Admin/Exams
        public async Task<ActionResult> Index()
        {
            var tblExams = db.tblExams.Include(t => t.tblExamType).Include(t => t.tblTopic).Include(t => t.tblUser);
            return View(await tblExams.ToListAsync());
        }
        public async Task<ActionResult> GetAllExamnationStudent()
        {
            var AllExamnationStudent = db.tblExamStudentMappings.Select(x=> new ExamMappingVM { 
            
                ExamID=x.ExamID,
                ExamName=x.tblExam.ExamName,
                StudentID=x.StudentID,
                StudentGR = db.tblEnrollments.Where(o => o.StudentID.Value ==x.StudentID).FirstOrDefault().GRNumber,
                StudentName=x.tblUser.Name,                
                ExamDuration= (x.tblExam.ExamScheduleFrom - x.tblExam.ExamScheduleTo).Value,
                
            });
            
            var data= await AllExamnationStudent.ToListAsync();
            return View();
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
        public async Task<ActionResult> Create(int? id)
        {
            
            if (id == null)
            {
                ViewBag.Students = new SelectList(db.tblUsers.Where(x => x.UserRole_FK == 2), "UserID", "Name");
                ViewBag.ExamType_FK = new SelectList(db.tblExamTypes, "ExamTypeID", "ExamTypeName");
                ViewBag.Topics_FK = new SelectList(db.tblTopics, "TopicID", "Topic");
                ViewBag.DeletedBy = new SelectList(db.tblUsers, "UserID", "Name");
                return View();
            }
            tblExam tblExam = await db.tblExams.FindAsync(id);
            if (tblExam == null)
            {
                return HttpNotFound();
            }
            ViewBag.Students = new SelectList(db.tblUsers.Where(x => x.UserRole_FK == 2), "UserID", "Name", tblExam.tblExamStudentMappings);
            ViewBag.ExamType_FK = new SelectList(db.tblExamTypes, "ExamTypeID", "ExamTypeName", tblExam.ExamType_FK);
            ViewBag.Topics_FK = new SelectList(db.tblTopics, "TopicID", "Topic", tblExam.Topics_FK);
            ViewBag.DeletedBy = new SelectList(db.tblUsers, "UserID", "Name", tblExam.DeletedBy);
            return View(tblExam);
        }

        // POST: Admin/Exams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<ActionResult> Create([Bind(Include = "ExamID,ExamName,Topics_FK,ExamDuration,ExamScheduleFrom,ExamScheduleTo,ExamType_FK,TotalMarks,PassingMarks,IsValid,IsCancled,IsCancledReason")] tblExam tblExam,string Students)
        {
            if (tblExam.ExamID==0)
            {

                db.tblExams.Add(tblExam);
                await db.SaveChangesAsync();
                var StudentList = Students.Split(',');
                foreach (var item in StudentList)
                {
                    var FeesRec = db.tblFees.Where(x => x.StudentID == Convert.ToInt32(item)).Where(o => o.ForMonth.Value.Year == tblExam.ExamScheduleFrom.Value.Year && o.ForMonth.Value.Month == tblExam.ExamScheduleFrom.Value.Month).FirstOrDefault();
                    if (FeesRec!=null)
                    {
                        tblExamStudentMapping studentMapping = new tblExamStudentMapping();
                        studentMapping.ExamID = tblExam.ExamID;
                        studentMapping.StudentID = Convert.ToInt32(item);
                        db.tblExamStudentMappings.Add(studentMapping);
                        await db.SaveChangesAsync();
                    }
                }                
                return RedirectToAction("Index");
            }
            if (tblExam.ExamID > 0)
            {
                var StudentList = Students.Split(',');
                var Mapping = db.tblExamStudentMappings.Where(x => x.ExamID == tblExam.ExamID).AsEnumerable<tblExamStudentMapping>();
                db.tblExamStudentMappings.RemoveRange(Mapping);
                await db.SaveChangesAsync();
                foreach (var item in StudentList)
                {
                    tblExamStudentMapping studentMapping = new tblExamStudentMapping();
                    studentMapping.ExamID = tblExam.ExamID;
                    studentMapping.StudentID = Convert.ToInt32(item);
                    db.tblExamStudentMappings.Add(studentMapping);
                    await db.SaveChangesAsync();
                }
                db.Entry(tblExam).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.Students = new SelectList(db.tblUsers.Where(x=>x.UserRole_FK==2), "UserID", "Name", tblExam.tblExamStudentMappings);
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
