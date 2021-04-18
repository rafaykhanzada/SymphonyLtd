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
    public class AllCoursesController : Controller
    {
        private SymphonyDBEntities db = new SymphonyDBEntities();

        // GET: AllCourses
        public async Task<ActionResult> Index()
        {
            var tblCourses = db.tblCourses.Where(x=>x.DeletedOn==null && x.DeletedBy==null).Include(t => t.tblCourseCategory).Include(t => t.tblUser).Include(t => t.tblClassType);
            return View(await tblCourses.ToListAsync());
        }

        // GET: AllCourses/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblCourse tblCourse = await db.tblCourses.FindAsync(id);
            if (tblCourse == null)
            {
                return HttpNotFound();
            }
            return View(tblCourse);
        }

        // GET: AllCourses/Create
        public ActionResult Create()
        {
            ViewBag.CourseCategory_FK = new SelectList(db.tblCourseCategories, "CourseCategoryID", "CourseCategory");
            ViewBag.DeletedBy = new SelectList(db.tblUsers, "UserID", "Name");
            ViewBag.ClassType_FK = new SelectList(db.tblClassTypes, "ClassTypeID", "ClassTypeName");
            return View();
        }

        // POST: AllCourses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "CourseID,CourseName,CourseCategory_FK,CourseDuration,CourseFees,Image,Description,Term,IsActive,CreatedOn,ModifiedOn,DeletedBy,DeletedOn,Icon,ClassType_FK")] tblCourse tblCourse)
        {
            if (ModelState.IsValid)
            {
                db.tblCourses.Add(tblCourse);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.CourseCategory_FK = new SelectList(db.tblCourseCategories, "CourseCategoryID", "CourseCategory", tblCourse.CourseCategory_FK);
            ViewBag.DeletedBy = new SelectList(db.tblUsers, "UserID", "Name", tblCourse.DeletedBy);
            ViewBag.ClassType_FK = new SelectList(db.tblClassTypes, "ClassTypeID", "ClassTypeName", tblCourse.ClassType_FK);
            return View(tblCourse);
        }

        // GET: AllCourses/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblCourse tblCourse = await db.tblCourses.FindAsync(id);
            if (tblCourse == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourseCategory_FK = new SelectList(db.tblCourseCategories, "CourseCategoryID", "CourseCategory", tblCourse.CourseCategory_FK);
            ViewBag.DeletedBy = new SelectList(db.tblUsers, "UserID", "Name", tblCourse.DeletedBy);
            ViewBag.ClassType_FK = new SelectList(db.tblClassTypes, "ClassTypeID", "ClassTypeName", tblCourse.ClassType_FK);
            return View(tblCourse);
        }

        // POST: AllCourses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "CourseID,CourseName,CourseCategory_FK,CourseDuration,CourseFees,Image,Description,Term,IsActive,CreatedOn,ModifiedOn,DeletedBy,DeletedOn,Icon,ClassType_FK")] tblCourse tblCourse)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblCourse).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.CourseCategory_FK = new SelectList(db.tblCourseCategories, "CourseCategoryID", "CourseCategory", tblCourse.CourseCategory_FK);
            ViewBag.DeletedBy = new SelectList(db.tblUsers, "UserID", "Name", tblCourse.DeletedBy);
            ViewBag.ClassType_FK = new SelectList(db.tblClassTypes, "ClassTypeID", "ClassTypeName", tblCourse.ClassType_FK);
            return View(tblCourse);
        }

        // GET: AllCourses/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblCourse tblCourse = await db.tblCourses.FindAsync(id);
            if (tblCourse == null)
            {
                return HttpNotFound();
            }
            return View(tblCourse);
        }

        // POST: AllCourses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            tblCourse tblCourse = await db.tblCourses.FindAsync(id);
            db.tblCourses.Remove(tblCourse);
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
