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
using SymphonyLtd.Security;

namespace SymphonyLtd.Areas.Admin.Controllers
{
    [FormAuthentication(RoleId = "1")]

    public class FeesTypesController : Controller
    {
        private SymphonyDBEntities db = new SymphonyDBEntities();

        // GET: Admin/FeesTypes
        public async Task<ActionResult> Index()
        {
            return View(await db.tblFeesTypes.ToListAsync());
        }

        // GET: Admin/FeesTypes/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblFeesType tblFeesType = await db.tblFeesTypes.FindAsync(id);
            if (tblFeesType == null)
            {
                return HttpNotFound();
            }
            return View(tblFeesType);
        }

        // GET: Admin/FeesTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/FeesTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "FeesTypeID,FeesType,IsActive,CreateOn,ModifiedOn,DeletedBy,DeletedOn")] tblFeesType tblFeesType)
        {
            if (ModelState.IsValid)
            {
                db.tblFeesTypes.Add(tblFeesType);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(tblFeesType);
        }

        // GET: Admin/FeesTypes/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblFeesType tblFeesType = await db.tblFeesTypes.FindAsync(id);
            if (tblFeesType == null)
            {
                return HttpNotFound();
            }
            return View(tblFeesType);
        }

        // POST: Admin/FeesTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "FeesTypeID,FeesType,IsActive,CreateOn,ModifiedOn,DeletedBy,DeletedOn")] tblFeesType tblFeesType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblFeesType).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(tblFeesType);
        }

        // GET: Admin/FeesTypes/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblFeesType tblFeesType = await db.tblFeesTypes.FindAsync(id);
            if (tblFeesType == null)
            {
                return HttpNotFound();
            }
            return View(tblFeesType);
        }

        // POST: Admin/FeesTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            tblFeesType tblFeesType = await db.tblFeesTypes.FindAsync(id);
            db.tblFeesTypes.Remove(tblFeesType);
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
