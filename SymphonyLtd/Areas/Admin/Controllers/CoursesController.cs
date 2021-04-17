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
using SymphonyLtd.Security;

namespace SymphonyLtd.Areas.Admin.Controllers
{
    [FormAuthentication(RoleId = "1")]

    public class CoursesController : Controller
    {
        private SymphonyDBEntities db = new SymphonyDBEntities();

        // GET: Admin/Courses
        public async Task<ActionResult> Index()
        {
            var tblCourses = db.tblCourses.Include(t => t.tblCourseCategory).Include(t => t.tblUser);
            return View(await tblCourses.ToListAsync());
        }

        // GET: Admin/Courses/Details/5
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

        // GET: Admin/Courses/Create
        public async Task<ActionResult> Create(int? id)
        {
            if (id == null)
            {
                ViewBag.CourseCategory_FK = new SelectList(db.tblCourseCategories, "CourseCategoryID", "CourseCategory");
                ViewBag.Topics_FK = new SelectList(db.tblTopics, "TopicID", "Topic");
                ViewBag.ClassType_FK = new SelectList(db.tblClassTypes, "ClassTypeID", "ClassTypeName");
                return View();
            }
            tblCourse tblCourse = await db.tblCourses.FindAsync(id);
            if (tblCourse == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourseCategory_FK = new SelectList(db.tblCourseCategories, "CourseCategoryID", "CourseCategory", tblCourse.CourseCategory_FK);
            ViewBag.ClassType_FK = new SelectList(db.tblClassTypes, "ClassTypeID", "ClassTypeName",tblCourse.ClassType_FK);
            return View(tblCourse);          
           
        }

        // POST: Admin/Courses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<ActionResult> Create([Bind(Include = "CourseID,CourseName,CourseCategory_FK,CourseDuration,CourseFees,Description,Term,IsActive,Icon,Starts")] tblCourse tblCourse, HttpPostedFileBase Image, List<string> Topics_FK)
        {
            ViewBag.CourseCategory_FK = new SelectList(db.tblCourseCategories, "CourseCategoryID", "CourseCategory", tblCourse.CourseCategory_FK);            
            ViewBag.Topics_FK = new SelectList(db.tblTopics, "TopicID", "Topic");
            ViewBag.ClassType_FK = new SelectList(db.tblClassTypes, "ClassTypeID", "ClassTypeName",tblCourse.ClassType_FK);

            if (Image != null)
            {
                var UniqueName = Common.GenerateRandomDigitCode(20);
                var extension = Path.GetExtension(Image.FileName);
                var path = Path.Combine(Server.MapPath("~/uploads"), UniqueName + extension);
                Image.SaveAs(path);
                tblCourse.Image = UniqueName + extension;
            }
            else
            {
                tblCourse.Image = "Default.jpg";
            }
            if (tblCourse.CourseID > 0)
            {
                db.Entry(tblCourse).State = EntityState.Modified;
                db.tblCourseTopicsMappings.RemoveRange(db.tblCourseTopicsMappings.Where(x => x.CourseID == tblCourse.CourseID));
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            if (tblCourse.CourseID==0)
            {
                tblCourse.CreatedOn = DateTime.Now;
                db.tblCourses.Add(tblCourse);
                await db.SaveChangesAsync();
            }
            //if (!String.IsNullOrEmpty(Topics_FK))
            //{
                //var Topic = Topics_FK.Split(',');
                foreach (var item in Topics_FK)
                {
                    tblCourseTopicsMapping courseTopicsMapping = new tblCourseTopicsMapping();
                    courseTopicsMapping.CourseID = tblCourse.CourseID;
                    courseTopicsMapping.TopicID = int.Parse(item);
                    courseTopicsMapping.CreateOn = DateTime.Now;  
                    db.tblCourseTopicsMappings.Add(courseTopicsMapping);
                     await db.SaveChangesAsync();

                }
                //return RedirectToAction("Index");
            //}
            return View(tblCourse);
        }
        // GET: Admin/Courses/Delete/5
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

        // POST: Admin/Courses/Delete/5
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
