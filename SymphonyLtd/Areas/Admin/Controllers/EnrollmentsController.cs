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
using SymphonyLtd.Security;

namespace SymphonyLtd.Areas.Admin.Controllers
{
    [FormAuthentication(RoleId = "1")]

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
        public async Task<ActionResult> Create(int? id)
        {
            if (id == null)
            {
                ViewBag.CourseID = new SelectList(db.tblCourses, "CourseID", "CourseName");
                ViewBag.StudentID = new SelectList(db.tblUsers, "UserID", "Name");
                ViewBag.Branches = new SelectList(db.tblBranches, "BranchID", "BranchName");

                return View();
            }
            tblEnrollment tblEnrollment = await db.tblEnrollments.FindAsync(id);
            if (tblEnrollment == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourseID = new SelectList(db.tblCourses, "CourseID", "CourseName", tblEnrollment.CourseID);
            ViewBag.StudentID = new SelectList(db.tblUsers, "UserID", "Name",tblEnrollment.StudentID);
            ViewBag.Branches = new SelectList(db.tblBranches, "BranchID", "BranchName", tblEnrollment.Branch_FK);

            return View(tblEnrollment);
        }

        [HttpPost]
        public async Task<ActionResult> Create(tblEnrollment tblEnrollment)
        {
            ViewBag.CourseID = new SelectList(db.tblCourses, "CourseID", "CourseName", tblEnrollment.CourseID);
            ViewBag.Branches = new SelectList(db.tblBranches, "BranchID", "BranchName", tblEnrollment.CourseID);

            if (tblEnrollment.EnrollmentID == 0)
            {
                Random random = new Random();
                tblEnrollment.CreatedOn = DateTime.Now;
                tblEnrollment.GRNumber = random.Next(000001,999999).ToString();
                db.tblEnrollments.Add(tblEnrollment);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }           
            if (tblEnrollment.EnrollmentID > 0)
            {
                tblEnrollment.GRNumber = db.tblEnrollments.Where(x => x.EnrollmentID == tblEnrollment.EnrollmentID).FirstOrDefault().GRNumber;
                db.Entry(tblEnrollment).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }          
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
