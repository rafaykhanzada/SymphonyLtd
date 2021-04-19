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
    public class AdminAboutUsController : Controller
    {
        private SymphonyDBEntities db = new SymphonyDBEntities();

        // GET: Admin/AdminAboutUs
     
        public async Task<ActionResult> Create(int? id)
        {
            if (id == null)
            {
                return View(await db.tblAboutUs.FirstOrDefaultAsync());
            }
            tblAboutU tblAboutU = await db.tblAboutUs.FindAsync(id);
            if (tblAboutU == null)
            {
                return HttpNotFound();
            }
            return View(tblAboutU);
        }

        // POST: Admin/AdminAboutUs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "AboutUsID,MainHeader,SubTitle,Description,Image,ButtonText,ButtonUrl")] tblAboutU tblAboutU, HttpPostedFileBase Image)
        {
            tblAboutU.IsActive = true;
            if (Image != null)
            {
                var UniqueName = Common.GenerateRandomDigitCode(20);
                var extension = Path.GetExtension(Image.FileName);
                var path = Path.Combine(Server.MapPath("~/uploads"), UniqueName + extension);
                Image.SaveAs(path);
                tblAboutU.Image = UniqueName + extension;
            }
            else
            {
                tblAboutU.Image = "Default.jpg";
            }
            if (tblAboutU.AboutUsID==0)
            {
                tblAboutU.CreatedOn = DateTime.Now;
                db.tblAboutUs.Add(tblAboutU);
                await db.SaveChangesAsync();
                return View(tblAboutU);
            }
            if (tblAboutU.AboutUsID > 0)
            {
                db.Entry(tblAboutU).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return View(tblAboutU);

            }
            return View(tblAboutU);
        }

        // GET: Admin/AdminAboutUs/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblAboutU tblAboutU = await db.tblAboutUs.FindAsync(id);
            if (tblAboutU == null)
            {
                return HttpNotFound();
            }
            return View(tblAboutU);
        }

        // POST: Admin/AdminAboutUs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "AboutUsID,MainHeader,SubTitle,Description,Image,ButtonText,ButtonUrl,IsActive,CreatedOn,ModifiedOn,DeletedBy,DeletedOn")] tblAboutU tblAboutU)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblAboutU).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(tblAboutU);
        }

        // GET: Admin/AdminAboutUs/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblAboutU tblAboutU = await db.tblAboutUs.FindAsync(id);
            if (tblAboutU == null)
            {
                return HttpNotFound();
            }
            return View(tblAboutU);
        }

        // POST: Admin/AdminAboutUs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            tblAboutU tblAboutU = await db.tblAboutUs.FindAsync(id);
            db.tblAboutUs.Remove(tblAboutU);
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
