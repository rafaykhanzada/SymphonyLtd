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
    public class AboutUsController : Controller
    {
        private SymphonyDBEntities db = new SymphonyDBEntities();

        // GET: AboutUs
        public async Task<ActionResult> Index()
        {
            return View(await db.tblAboutUs.ToListAsync());
        }

        // GET: AboutUs/Details/5
        public async Task<ActionResult> Details(int? id)
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

        // GET: AboutUs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AboutUs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "AboutUsID,MainHeader,SubTitle,Description,Image,ButtonText,ButtonUrl,IsActive,CreatedOn,ModifiedOn,DeletedBy,DeletedOn")] tblAboutU tblAboutU)
        {
            if (ModelState.IsValid)
            {
                db.tblAboutUs.Add(tblAboutU);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(tblAboutU);
        }

        // GET: AboutUs/Edit/5
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

        // POST: AboutUs/Edit/5
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

        // GET: AboutUs/Delete/5
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

        // POST: AboutUs/Delete/5
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
