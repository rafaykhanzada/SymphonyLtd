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

namespace SymphonyLtd.Areas.Admin.Controllers
{
    public class ClassTypesController : Controller
    {
        private SymphonyDBEntities db = new SymphonyDBEntities();

        // GET: Admin/ClassTypes
        public async Task<ActionResult> Index()
        {
            return View(await db.tblClassTypes.ToListAsync());
        }

        // GET: Admin/ClassTypes/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblClassType tblClassType = await db.tblClassTypes.FindAsync(id);
            if (tblClassType == null)
            {
                return HttpNotFound();
            }
            return View(tblClassType);
        }

        // GET: Admin/ClassTypes/Create
        public async Task<ActionResult> Create(int? id)
        {
            if (id == null)
            {
            return View();
            }
            tblClassType tblClassType = await db.tblClassTypes.FindAsync(id);
            if (tblClassType == null)
            {
                return HttpNotFound();
            }
            return View(tblClassType);
        }

        // POST: Admin/ClassTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<ActionResult> Create([Bind(Include = "ClassTypeID,ClassTypeName,IsActive")] tblClassType tblClassType)
        {
            if (tblClassType.ClassTypeID==0)
            {
                db.tblClassTypes.Add(tblClassType);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            if (tblClassType.ClassTypeID > 0)
            {
                db.Entry(tblClassType).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(tblClassType);
        }

      
        // GET: Admin/ClassTypes/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblClassType tblClassType = await db.tblClassTypes.FindAsync(id);
            if (tblClassType == null)
            {
                return HttpNotFound();
            }
            return View(tblClassType);
        }

        // POST: Admin/ClassTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            tblClassType tblClassType = await db.tblClassTypes.FindAsync(id);
            db.tblClassTypes.Remove(tblClassType);
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
