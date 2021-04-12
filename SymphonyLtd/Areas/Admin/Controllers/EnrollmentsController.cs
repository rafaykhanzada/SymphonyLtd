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
    public class EnrollmentsController : Controller
    {
        private SymphonyDBEntities db = new SymphonyDBEntities();

        // GET: Admin/Enrollments
        public async Task<ActionResult> Index()
        {
            var tblEnrollments = db.tblEnrollments.Include(t => t.tblCourse).Include(t => t.tblUser);
            return View(await tblEnrollments.ToListAsync());
        }

        // GET: Admin/Enrollments/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblEnrollment tblEnrollment = await db.tblEnrollments.FindAsync(id);
            if (tblEnrollment == null)
            {
                return HttpNotFound();
            }
            return View(tblEnrollment);
        }

        // GET: Admin/Enrollments/Create
        public ActionResult Create()
        {
            ViewBag.StudentID = new SelectList(db.tblUsers, "UserID", "Name");
            ViewBag.CourseID = new SelectList(db.tblCourses, "CourseID", "CourseName");
            ViewBag.DeletedBy = new SelectList(db.tblUsers, "UserID", "Name");
            return View();
        }

        // POST: Admin/Enrollments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "EnrollmentID,StudentID,CourseID,GRNumber,EntrollmentDate,EnrollFrom,EntrollTo,IsValid,IsDropOff,IsDefaulter,CreatedOn,ModifiedOn,DeletedBy,DeletedOn")] tblEnrollment tblEnrollment)
        {
            if (ModelState.IsValid)
            {
                db.tblEnrollments.Add(tblEnrollment);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.CourseID = new SelectList(db.tblCourses, "CourseID", "CourseName", tblEnrollment.CourseID);
            ViewBag.DeletedBy = new SelectList(db.tblUsers, "UserID", "Name", tblEnrollment.DeletedBy);
            return View(tblEnrollment);
        }

        // GET: Admin/Enrollments/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblEnrollment tblEnrollment = await db.tblEnrollments.FindAsync(id);
            if (tblEnrollment == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourseID = new SelectList(db.tblCourses, "CourseID", "CourseName", tblEnrollment.CourseID);
            ViewBag.DeletedBy = new SelectList(db.tblUsers, "UserID", "Name", tblEnrollment.DeletedBy);
            return View(tblEnrollment);
        }

        // POST: Admin/Enrollments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "EnrollmentID,StudentID,CourseID,GRNumber,EntrollmentDate,EnrollFrom,EntrollTo,IsValid,IsDropOff,IsDefaulter,CreatedOn,ModifiedOn,DeletedBy,DeletedOn")] tblEnrollment tblEnrollment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblEnrollment).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.CourseID = new SelectList(db.tblCourses, "CourseID", "CourseName", tblEnrollment.CourseID);
            ViewBag.DeletedBy = new SelectList(db.tblUsers, "UserID", "Name", tblEnrollment.DeletedBy);
            return View(tblEnrollment);
        }

        // GET: Admin/Enrollments/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblEnrollment tblEnrollment = await db.tblEnrollments.FindAsync(id);
            if (tblEnrollment == null)
            {
                return HttpNotFound();
            }
            return View(tblEnrollment);
        }

        // POST: Admin/Enrollments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            tblEnrollment tblEnrollment = await db.tblEnrollments.FindAsync(id);
            db.tblEnrollments.Remove(tblEnrollment);
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
