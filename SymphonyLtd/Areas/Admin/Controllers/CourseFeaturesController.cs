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
    public class CourseFeaturesController : Controller
    {
        private SymphonyDBEntities db = new SymphonyDBEntities();

        // GET: Admin/CourseFeatures
        public async Task<ActionResult> Index()
        {
            var tblCourseFeatures = db.tblCourseFeatures.Include(t => t.tblCourse);
            return View(await tblCourseFeatures.ToListAsync());
        }

        // GET: Admin/CourseFeatures/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblCourseFeature tblCourseFeature = await db.tblCourseFeatures.FindAsync(id);
            if (tblCourseFeature == null)
            {
                return HttpNotFound();
            }
            return View(tblCourseFeature);
        }

        // GET: Admin/CourseFeatures/Create
        public async Task<ActionResult> Create(int? id)
        {            
            if (id == null)
            {
                ViewBag.CourseID = new SelectList(db.tblCourses.Where(x=>x.DeletedBy==null && x.DeletedOn==null), "CourseID", "CourseName");
                return View();
            }
            tblCourseFeature tblCourseFeature = await db.tblCourseFeatures.FindAsync(id);
            if (tblCourseFeature == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourseID = new SelectList(db.tblCourses.Where(x => x.DeletedBy == null && x.DeletedOn == null), "CourseID", "CourseName", tblCourseFeature.CourseID);
            return View(tblCourseFeature);
        }

        // POST: Admin/CourseFeatures/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "CourseFeatureID,CourseFeatureName,ShortDesc,Icon,CourseID")] tblCourseFeature tblCourseFeature)
        {
            if (tblCourseFeature.CourseFeatureID == 0)
            {
                db.tblCourseFeatures.Add(tblCourseFeature);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            if (tblCourseFeature.CourseFeatureID > 0)
            {
                db.Entry(tblCourseFeature).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.CourseID = new SelectList(db.tblCourses.Where(x => x.DeletedBy == null && x.DeletedOn == null), "CourseID", "CourseName", tblCourseFeature.CourseID);
            return View(tblCourseFeature);
        }

        // GET: Admin/CourseFeatures/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblCourseFeature tblCourseFeature = await db.tblCourseFeatures.FindAsync(id);
            if (tblCourseFeature == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourseID = new SelectList(db.tblCourses, "CourseID", "CourseName", tblCourseFeature.CourseID);
            return View(tblCourseFeature);
        }

        // POST: Admin/CourseFeatures/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "CourseFeatureID,CourseFeatureName,ShortDesc,Icon,CourseID")] tblCourseFeature tblCourseFeature)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblCourseFeature).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.CourseID = new SelectList(db.tblCourses, "CourseID", "CourseName", tblCourseFeature.CourseID);
            return View(tblCourseFeature);
        }

        // GET: Admin/CourseFeatures/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblCourseFeature tblCourseFeature = await db.tblCourseFeatures.FindAsync(id);
            if (tblCourseFeature == null)
            {
                return HttpNotFound();
            }
            return View(tblCourseFeature);
        }

        // POST: Admin/CourseFeatures/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            tblCourseFeature tblCourseFeature = await db.tblCourseFeatures.FindAsync(id);
            db.tblCourseFeatures.Remove(tblCourseFeature);
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
