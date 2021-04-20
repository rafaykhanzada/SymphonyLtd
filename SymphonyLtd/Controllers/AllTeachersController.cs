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
    public class AllTeachersController : Controller
    {
        private SymphonyDBEntities db = new SymphonyDBEntities();

        // GET: Teachers
        public async Task<ActionResult> Index()
        {
            ViewBag.TeacherCount = db.tblTeachers.Count();
            return View(await db.tblTeachers.ToListAsync());
        }

        // GET: Teachers/Details/5
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

        // GET: Teachers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Teachers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "TeacherID,Name,Subj,Image,IsActive,Start,Experience,Likes")] tblTeacher tblTeacher)
        {
            if (ModelState.IsValid)
            {
                db.tblTeachers.Add(tblTeacher);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(tblTeacher);
        }

        // GET: Teachers/Edit/5
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

        // POST: Teachers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "TeacherID,Name,Subj,Image,IsActive,Start,Experience,Likes")] tblTeacher tblTeacher)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblTeacher).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(tblTeacher);
        }

        // GET: Teachers/Delete/5
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

        // POST: Teachers/Delete/5
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
