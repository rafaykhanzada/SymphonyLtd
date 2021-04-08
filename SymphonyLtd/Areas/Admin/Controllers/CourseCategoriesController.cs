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
    public class CourseCategoriesController : Controller
    {
        private SymphonyDBEntities db = new SymphonyDBEntities();

        // GET: Admin/CourseCategories
        public async Task<ActionResult> Index()
        {
            var tblCourseCategories = db.tblCourseCategories.Include(t => t.tblUser);
            return View(await tblCourseCategories.ToListAsync());
        }

        // GET: Admin/CourseCategories/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblCourseCategory tblCourseCategory = await db.tblCourseCategories.FindAsync(id);
            if (tblCourseCategory == null)
            {
                return HttpNotFound();
            }
            return View(tblCourseCategory);
        }

        // GET: Admin/CourseCategories/Create
        public ActionResult Create()
        {
            ViewBag.DeletedBy = new SelectList(db.tblUsers, "UserID", "Name");
            return View();
        }

        // POST: Admin/CourseCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "CourseCategoryID,CourseCategory,Description,Image,IsActive,CreatedOn,ModifiedOn,DeletedBy,DeletedOn")] tblCourseCategory tblCourseCategory)
        {
            if (ModelState.IsValid)
            {
                db.tblCourseCategories.Add(tblCourseCategory);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.DeletedBy = new SelectList(db.tblUsers, "UserID", "Name", tblCourseCategory.DeletedBy);
            return View(tblCourseCategory);
        }

        // GET: Admin/CourseCategories/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblCourseCategory tblCourseCategory = await db.tblCourseCategories.FindAsync(id);
            if (tblCourseCategory == null)
            {
                return HttpNotFound();
            }
            ViewBag.DeletedBy = new SelectList(db.tblUsers, "UserID", "Name", tblCourseCategory.DeletedBy);
            return View(tblCourseCategory);
        }

        // POST: Admin/CourseCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "CourseCategoryID,CourseCategory,Description,Image,IsActive,CreatedOn,ModifiedOn,DeletedBy,DeletedOn")] tblCourseCategory tblCourseCategory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblCourseCategory).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.DeletedBy = new SelectList(db.tblUsers, "UserID", "Name", tblCourseCategory.DeletedBy);
            return View(tblCourseCategory);
        }

        // GET: Admin/CourseCategories/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblCourseCategory tblCourseCategory = await db.tblCourseCategories.FindAsync(id);
            if (tblCourseCategory == null)
            {
                return HttpNotFound();
            }
            return View(tblCourseCategory);
        }

        // POST: Admin/CourseCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            tblCourseCategory tblCourseCategory = await db.tblCourseCategories.FindAsync(id);
            db.tblCourseCategories.Remove(tblCourseCategory);
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
