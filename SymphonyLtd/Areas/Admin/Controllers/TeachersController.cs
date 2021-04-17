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
using System.IO;
using SymphonyLtd.Helper;

namespace SymphonyLtd.Areas.Admin.Controllers
{
    public class TeachersController : Controller
    {
        private SymphonyDBEntities db = new SymphonyDBEntities();

        // GET: Admin/Teachers
        public async Task<ActionResult> Index()
        {
            return View(await db.tblTeachers.ToListAsync());
        }

        // GET: Admin/Teachers/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblTeacher tblTeacher = await db.tblTeachers.FindAsync(id);
            if (tblTeacher == null)
            {
                return HttpNotFound();
            }
            return View(tblTeacher);
        }

        // GET: Admin/Teachers/Create
        public async Task<ActionResult> CreateAsync(int? id)
        {
            if (id == null)
            {
            return View();
            }
            tblTeacher tblTeacher = await db.tblTeachers.FindAsync(id);
            if (tblTeacher == null)
            {
                return HttpNotFound();
            }
            return View(tblTeacher);
        }

        [HttpPost]
        public async Task<ActionResult> Create([Bind(Include = "TeacherID,Name,Subj,Image,IsActive")] tblTeacher tblTeacher, HttpPostedFileBase file)
        {
            if (file != null)
            {
                var UniqueName = Common.GenerateRandomDigitCode(20);
                var extension = Path.GetExtension(file.FileName);
                var path = Path.Combine(Server.MapPath("~/uploads"), UniqueName + extension);
                file.SaveAs(path);
                tblTeacher.Image = UniqueName + extension;
            }
            else
            {
                tblTeacher.Image = "Default.jpg";
            }
            if (tblTeacher.TeacherID==0)
            {
                db.tblTeachers.Add(tblTeacher);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            if (tblTeacher.TeacherID > 0)
            {
                db.Entry(tblTeacher).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(tblTeacher);
        }

        // GET: Admin/Teachers/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblTeacher tblTeacher = await db.tblTeachers.FindAsync(id);
            if (tblTeacher == null)
            {
                return HttpNotFound();
            }
            return View(tblTeacher);
        }

        // POST: Admin/Teachers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "TeacherID,Name,Subj,Image,IsActive")] tblTeacher tblTeacher)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblTeacher).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(tblTeacher);
        }

        // GET: Admin/Teachers/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblTeacher tblTeacher = await db.tblTeachers.FindAsync(id);
            if (tblTeacher == null)
            {
                return HttpNotFound();
            }
            return View(tblTeacher);
        }

        // POST: Admin/Teachers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            tblTeacher tblTeacher = await db.tblTeachers.FindAsync(id);
            db.tblTeachers.Remove(tblTeacher);
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
