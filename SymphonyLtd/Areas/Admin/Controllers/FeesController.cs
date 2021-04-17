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
using SymphonyLtd.Security;

namespace SymphonyLtd.Areas.Admin.Controllers
{
    [FormAuthentication(RoleId = "1")]

    public class FeesController : Controller
    {
        private SymphonyDBEntities db = new SymphonyDBEntities();

        // GET: Admin/Fees
        public async Task<ActionResult> Index()
        {
            var tblFees = db.tblFees.Include(t => t.tblFeesType);
            return View(await tblFees.ToListAsync());
        }

        // GET: Admin/Fees/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblFee tblFee = await db.tblFees.FindAsync(id);
            if (tblFee == null)
            {
                return HttpNotFound();
            }
            return View(tblFee);
        }

        // GET: Admin/Fees/Create
        public ActionResult Create()
        {
            ViewBag.FeesType = new SelectList(db.tblFeesTypes, "FeesTypeID", "FeesType");
            ViewBag.ForMonth = new SelectList(Common.GetMonths());
            return View();
        }

        // POST: Admin/Fees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "FeesID,EnrollmentID,StudentID,PaidAmount,PaidDate,FeesType,ForMonth,ChargeBy,CreateOn,ModifiedOn,DeleteBy,DeleteOn")] tblFee tblFee)
        {
            if (ModelState.IsValid)
            {
                db.tblFees.Add(tblFee);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.FeesType = new SelectList(db.tblFeesTypes, "FeesTypeID", "FeesType", tblFee.FeesType);
            return View(tblFee);
        }

        // GET: Admin/Fees/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblFee tblFee = await db.tblFees.FindAsync(id);
            if (tblFee == null)
            {
                return HttpNotFound();
            }
            ViewBag.FeesType = new SelectList(db.tblFeesTypes, "FeesTypeID", "FeesType", tblFee.FeesType);
            return View(tblFee);
        }

        // POST: Admin/Fees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "FeesID,EnrollmentID,StudentID,PaidAmount,PaidDate,FeesType,ForMonth,ChargeBy,CreateOn,ModifiedOn,DeleteBy,DeleteOn")] tblFee tblFee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblFee).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.FeesType = new SelectList(db.tblFeesTypes, "FeesTypeID", "FeesType", tblFee.FeesType);
            return View(tblFee);
        }

        // GET: Admin/Fees/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblFee tblFee = await db.tblFees.FindAsync(id);
            if (tblFee == null)
            {
                return HttpNotFound();
            }
            return View(tblFee);
        }

        // POST: Admin/Fees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            tblFee tblFee = await db.tblFees.FindAsync(id);
            db.tblFees.Remove(tblFee);
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
