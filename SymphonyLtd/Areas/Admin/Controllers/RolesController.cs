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
    public class RolesController : Controller
    {
        private SymphonyDBEntities db = new SymphonyDBEntities();

        // GET: Admin/Roles
        public async Task<ActionResult> Index()
        {
            var tblRoles = db.tblRoles.Include(t => t.tblUser);
            return View(await tblRoles.ToListAsync());
        }

        // GET: Admin/Roles/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblRole tblRole = await db.tblRoles.FindAsync(id);
            if (tblRole == null)
            {
                return HttpNotFound();
            }
            return View(tblRole);
        }

        // GET: Admin/Roles/Create
        public async Task<ActionResult> Create(int? id)
        {
            if (id == null)
            {
                ViewBag.DeletedBy = new SelectList(db.tblUsers, "UserID", "Name");
                return View();
            }
            tblRole tblRole = await db.tblRoles.FindAsync(id);
            if (tblRole == null)
            {
                return HttpNotFound();
            }
            ViewBag.DeletedBy = new SelectList(db.tblUsers, "UserID", "Name", tblRole.DeletedBy);
            return View(tblRole);
           
        }

        // POST: Admin/Roles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "RoleID,RoleName,IsActive")] tblRole tblRole)
        {
            if (tblRole !=null)
            {
                if (tblRole.RoleID == 0)
                {
                    db.tblRoles.Add(tblRole);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                if (tblRole.RoleID > 0)
                {
                    db.Entry(tblRole).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                return View(tblRole);
            }
            return View();
        }
        // GET: Admin/Roles/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblRole tblRole = await db.tblRoles.FindAsync(id);
            if (tblRole == null)
            {
                return HttpNotFound();
            }
            return View(tblRole);
        }

        // POST: Admin/Roles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            tblRole tblRole = await db.tblRoles.FindAsync(id);
            db.tblRoles.Remove(tblRole);
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
