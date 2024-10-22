﻿using System;
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
        public async Task<ActionResult> Index(int pageNumber = 1, string Search = null)
        {
            //var tblFees = db.tblFees.Include(t => t.tblFeesType);
            //return View(await tblFees.ToListAsync());
            IQueryable<tblFee> tblFees;
            try
            {
                if (!String.IsNullOrEmpty(Search))
                {
                    tblFees = db.tblFees.Where(x => x.StudentID == Convert.ToInt32(Search)).Include(t => t.tblFeesType).AsNoTracking();

                    if (tblFees.Count() == 0)
                    {
                        ViewBag.Product = "No Result Found";
                        return View();
                    }
                }
                else
                {
                    tblFees = db.tblFees.Include(t => t.tblFeesType).AsNoTracking();
                }

                int pageSize = 10;
                return View(await PaginatedList<tblFee>.CreateAsync(tblFees.OrderBy(x => x.CreateOn), pageNumber, pageSize));
            }
            catch (Exception ex)
            {
                return View();
            }
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
        public async Task<ActionResult> Create(int? id)
        {            
            if (id == null)
            {
                ViewBag.FeesType = new SelectList(db.tblFeesTypes, "FeesTypeID", "FeesType");
                ViewBag.EnrollmentID = new SelectList(db.tblEnrollments, "EnrollmentID", "EnrollmentID");
                ViewBag.StudentID = new SelectList(db.tblUsers, "UserID", "Name");
                ViewBag.ForMonth = new SelectList(Common.GetMonths(), Common.GetMonths());
                return View();
            }
            tblFee tblFee = await db.tblFees.FindAsync(id);
            if (tblFee == null)
            {
                return HttpNotFound();
            }
            ViewBag.FeesType = new SelectList(db.tblFeesTypes, "FeesTypeID", "FeesType", tblFee.FeesType);
            ViewBag.ForMonth = new SelectList(Common.GetMonths(),tblFee.ForMonth);
            ViewBag.EnrollmentID = new SelectList(db.tblEnrollments, "EnrollmentID", "EnrollmentID",tblFee.EnrollmentID);
            ViewBag.StudentID = new SelectList(db.tblUsers, "UserID", "Name",tblFee.StudentID);
            return View(tblFee);
        }

        // POST: Admin/Fees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<ActionResult> Create([Bind(Include = "FeesID,EnrollmentID,StudentID,PaidAmount,PaidDate,FeesType,ForMonth")] tblFee tblFee)
        {
            tblUser user = (tblUser)Session["User"];
            if (user==null)
            {
                user = new SymphonyLtd.Models.SymphonyDBEntities().tblUsers.FirstOrDefault(x => x.Email == User.Identity.Name);
            }
            tblFee.ChargeBy = user.UserID;
            if (tblFee.FeesID == 0)
            {
                tblFee.CreateOn = DateTime.Now;
                db.tblFees.Add(tblFee);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            if (tblFee.FeesID > 0)
            {
                db.Entry(tblFee).State = EntityState.Modified;
                db.Entry(tblFee).Property(p => p.CreateOn).IsModified = false;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.FeesType = new SelectList(db.tblFeesTypes, "FeesTypeID", "FeesType", tblFee.FeesType);
            ViewBag.ForMonth = new SelectList(Common.GetMonths(), tblFee.ForMonth);
            ViewBag.EnrollmentID = new SelectList(db.tblEnrollments, "EnrollmentID", "EnrollmentID", tblFee.EnrollmentID);
            ViewBag.StudentID = new SelectList(db.tblUsers, "UserID", "Name", tblFee.StudentID);
            return View(tblFee);
        }

      
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
