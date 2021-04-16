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
using System.IO;
using SymphonyLtd.Helper;
using SymphonyLtd.Security;

namespace SymphonyLtd.Areas.Admin.Controllers
{
    //[FormAuthentication(RoleId = "1")]
    [AuthorizeAdminOrOwnerOfPostAttribute]
    public class UserController : Controller
    {
        private SymphonyDBEntities db = new SymphonyDBEntities();

        // GET: Admin/User
        public async Task<ActionResult> Index()
        {
            var tblUsers = db.tblUsers.Include(t => t.tblRole).Where(x=>x.UserRole_FK!=1);
            return View(await tblUsers.ToListAsync());
        }

        // GET: Admin/User/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblUser tblUser = await db.tblUsers.FindAsync(id);
            if (tblUser == null)
            {
                return HttpNotFound();
            }
            return View(tblUser);
        }

        // GET: Admin/User/Create
        public ActionResult Create(int? id)
        {
            if (id == null)
            {
                ViewBag.UserRole_FK = new SelectList(db.tblRoles, "RoleID", "RoleName");
                return View();
            }
            tblUser tblUser = db.tblUsers.Find(id);
            if (tblUser == null)
            {
                return View();
            }
            ViewBag.UserRole_FK = new SelectList(db.tblRoles, "RoleID", "RoleName", tblUser.UserRole_FK);
            return View(tblUser);
            
        }

        // POST: Admin/User/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "UserID,Name,Email,Password,Address,PhoneNumber,UserRole_FK")] tblUser tblUser,HttpPostedFileBase Profile)
        {
            if (Profile.ContentLength > 0)
            {
                var UniqueName = Common.GenerateRandomDigitCode(20);
                var extension = Path.GetExtension(Profile.FileName);
                var path = Path.Combine(Server.MapPath("~/uploads"), UniqueName + extension);
                Profile.SaveAs(path);
                tblUser.Profile = UniqueName + extension;
            }
            if (tblUser.UserID==0)
            {
                tblUser.IsActive = true;
                tblUser.CreatedOn = DateTime.Now;
                tblUser.TokenCode = Common.GenerateRandomInteger(6732,127338).ToString();                 
                db.tblUsers.Add(tblUser);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            else if (tblUser.UserID>0)
            {
              tblUser.ModifiedOn = DateTime.Now;
              db.Entry(tblUser).State = EntityState.Modified;
              await db.SaveChangesAsync();
              return RedirectToAction("Index");
            }
            
            ViewBag.UserRole_FK = new SelectList(db.tblRoles, "RoleID", "RoleName", tblUser.UserRole_FK);
            return View(tblUser);
        }

        // GET: Admin/User/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblUser tblUser = await db.tblUsers.FindAsync(id);
            if (tblUser == null)
            {
                return HttpNotFound();
            }
            return View(tblUser);
        }

        // POST: Admin/User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            tblUser tblUser = await db.tblUsers.FindAsync(id);
            db.tblUsers.Remove(tblUser);
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
