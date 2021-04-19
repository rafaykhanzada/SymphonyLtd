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
using Microsoft.AspNetCore.Http;
using SymphonyLtd.Helper;
using System.IO;
using SymphonyLtd.Security;

namespace SymphonyLtd.Areas.Admin.Controllers
{
    [FormAuthentication(RoleId = "1")]

    public class NewsItemsController : Controller
    {
        private SymphonyDBEntities db = new SymphonyDBEntities();

        // GET: Admin/NewsItems
        public async Task<ActionResult> Index()
        {
            return View(await db.tblNewsItems.ToListAsync());
        }

        // GET: Admin/NewsItems/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblNewsItem tblNewsItem = await db.tblNewsItems.FindAsync(id);
            if (tblNewsItem == null)
            {
                return HttpNotFound();
            }
            return View(tblNewsItem);
        }

        // GET: Admin/NewsItems/Create
        public async Task<ActionResult> Create(int? id)
        {
            if (id == null)
            {
                return View();
            }
            tblNewsItem tblNewsItem = await db.tblNewsItems.FindAsync(id);
            if (tblNewsItem == null)
            {
                return HttpNotFound();
            }
            return View(tblNewsItem);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "NewsItemsID,NewsItemHeader,Image,Tages")] tblNewsItem tblNewsItem, HttpPostedFileBase file)
        {
            tblUser user = (SymphonyLtd.Models.tblUser)Session["User"];
            if (user == null)
            {
                user = new SymphonyLtd.Models.SymphonyDBEntities().tblUsers.FirstOrDefault(x => x.Email == User.Identity.Name);
            }
            if (file != null)
            {
                var UniqueName = Common.GenerateRandomDigitCode(20);
                var extension = Path.GetExtension(file.FileName);
                var path = Path.Combine(Server.MapPath("~/uploads"), UniqueName + extension);
                file.SaveAs(path);
                tblNewsItem.Image = UniqueName + extension;
            }
            else
            {
                tblNewsItem.Image = "Default.jpg";
            }
            if (tblNewsItem.NewsItemsID == 0)
            {
                tblNewsItem.CreatedOn = DateTime.Now;
                tblNewsItem.CreatedBy = user.UserID;
                db.tblNewsItems.Add(tblNewsItem);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            if (tblNewsItem.NewsItemsID > 0)
            {
                tblNewsItem.ModifiedOn = DateTime.Now;
                tblNewsItem.CreatedBy = user.UserID;
                db.Entry(tblNewsItem).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(tblNewsItem);
        }

        // GET: Admin/NewsItems/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblNewsItem tblNewsItem = await db.tblNewsItems.FindAsync(id);
            if (tblNewsItem == null)
            {
                return HttpNotFound();
            }
            return View(tblNewsItem);
        }

        // POST: Admin/NewsItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "NewsItemsID,NewsItemHeader,Image,Tages,CreatedBy,CreatedOn,IsActive,ModifiedOn,DeletedOn,DeletedBy")] tblNewsItem tblNewsItem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblNewsItem).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(tblNewsItem);
        }

        // GET: Admin/NewsItems/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblNewsItem tblNewsItem = await db.tblNewsItems.FindAsync(id);
            if (tblNewsItem == null)
            {
                return HttpNotFound();
            }
            return View(tblNewsItem);
        }

        // POST: Admin/NewsItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            tblNewsItem tblNewsItem = await db.tblNewsItems.FindAsync(id);
            db.tblNewsItems.Remove(tblNewsItem);
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
