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
using SymphonyLtd.Helper;
using System.IO;

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
        public async Task<ActionResult> Create(int? id)
        {
             if (id == null)
            {
                ViewBag.DeletedBy = new SelectList(db.tblUsers, "UserID", "Name");
                return View();
            }
            tblCourseCategory tblCourseCategory = await db.tblCourseCategories.FindAsync(id);
            if (tblCourseCategory == null)
            {
                return HttpNotFound();
            }
            ViewBag.DeletedBy = new SelectList(db.tblUsers, "UserID", "Name", tblCourseCategory.DeletedBy);
            return View(tblCourseCategory);         
        }

        // POST: Admin/CourseCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "CourseCategoryID,CourseCategory,Description,IsActive")] tblCourseCategory tblCourseCategory, HttpPostedFileBase Image)
        {
            if (Image.ContentLength > 0)
            {
                var UniqueName = Common.GenerateRandomDigitCode(20);
                var extension = Path.GetExtension(Image.FileName);
                var path = Path.Combine(Server.MapPath("~/uploads"), UniqueName + extension);
                Image.SaveAs(path);
               tblCourseCategory.Image = UniqueName + extension;
            }
            if (tblCourseCategory.CourseCategoryID==0)
            {
                tblCourseCategory.CreatedOn = DateTime.Now;
                db.tblCourseCategories.Add(tblCourseCategory);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            else if (tblCourseCategory.CourseCategoryID > 0)
            {
                tblCourseCategory.ModifiedOn = DateTime.Now;
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
