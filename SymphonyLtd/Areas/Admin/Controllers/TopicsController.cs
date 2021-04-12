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

namespace SymphonyLtd.Areas.Admin.Controllers
{
    public class TopicsController : Controller
    {
        private SymphonyDBEntities db = new SymphonyDBEntities();

        // GET: Admin/Topics
        public async Task<ActionResult> Index()
        {
            var tblTopics = db.tblTopics.Include(t => t.tblUser);
            return View(await tblTopics.ToListAsync());
        }

        // GET: Admin/Topics/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblTopic tblTopic = await db.tblTopics.FindAsync(id);
            if (tblTopic == null)
            {
                return HttpNotFound();
            }
            return View(tblTopic);
        }

        // GET: Admin/Topics/Create
        public async Task<ActionResult> Create(int? id)
        {
            if (id == null)
            {
                ViewBag.Days = new SelectList(Common.GetDays());
                return View();
            }
            tblTopic tblTopic = await db.tblTopics.FindAsync(id);
            if (tblTopic == null)
            {
                return HttpNotFound();
            }
            ViewBag.Days = new SelectList(Common.GetDays());
            return View(tblTopic);           
        }       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "TopicID,Topic,LearningOutcome,Description,ClassSchedule,ClassTime,Thumbnail,IsActive")] tblTopic tblTopic)
        {
            if (tblTopic.TopicID==0)
            {
                tblTopic.CreateOn = DateTime.Now;
                db.tblTopics.Add(tblTopic);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            if (tblTopic.TopicID > 0)
            {
                tblTopic.ModifiedOn = DateTime.Now;
                db.Entry(tblTopic).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.DeletedBy = new SelectList(db.tblUsers, "UserID", "Name", tblTopic.DeletedBy);
            return View(tblTopic);
        }

        // GET: Admin/Topics/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblTopic tblTopic = await db.tblTopics.FindAsync(id);
            if (tblTopic == null)
            {
                return HttpNotFound();
            }
            ViewBag.DeletedBy = new SelectList(db.tblUsers, "UserID", "Name", tblTopic.DeletedBy);
            return View(tblTopic);
        }

        // POST: Admin/Topics/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "TopicID,Topic,LearningOutcome,Description,ClassSchedule,ClassTime,Thumbnail,IsActive,CreateOn,ModifiedOn,DeletedBy,DeletedOn")] tblTopic tblTopic)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblTopic).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.DeletedBy = new SelectList(db.tblUsers, "UserID", "Name", tblTopic.DeletedBy);
            return View(tblTopic);
        }

        // GET: Admin/Topics/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblTopic tblTopic = await db.tblTopics.FindAsync(id);
            if (tblTopic == null)
            {
                return HttpNotFound();
            }
            return View(tblTopic);
        }

        // POST: Admin/Topics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            tblTopic tblTopic = await db.tblTopics.FindAsync(id);
            db.tblTopics.Remove(tblTopic);
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
