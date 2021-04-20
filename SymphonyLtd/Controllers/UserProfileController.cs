using SymphonyLtd.Helper;
using SymphonyLtd.Models;
using SymphonyLtd.Security;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SymphonyLtd.Controllers
{
    [FormAuthentication(RoleId = "2")]
    public class UserProfileController : Controller
    {
        private SymphonyDBEntities db = new SymphonyDBEntities();
        // GET: UserProfile
        public async Task<ActionResult> MyAccount()
        {
            tblUser user = await db.tblUsers.FindAsync(GetUser().UserID);
            return View(user);
        }
        [HttpPost]
        public async Task<ActionResult> Update([Bind(Include = "UserID,Name,Email,Password,Address,PhoneNumber,UserRole_FK")] tblUser tblUser, HttpPostedFileBase file)
        {
            if (file.ContentLength > 0)
            {
                var UniqueName = Common.GenerateRandomDigitCode(20);
                var extension = Path.GetExtension(file.FileName);
                var path = Path.Combine(Server.MapPath("~/uploads"), UniqueName + extension);
                file.SaveAs(path);
                tblUser.Profile = UniqueName + extension;
            }
             if (tblUser.UserID > 0)
            {
                tblUser.ModifiedOn = DateTime.Now;
                db.Entry(tblUser).State = EntityState.Modified;
                db.Entry(tblUser).Property(p => p.CreatedOn).IsModified = false;
                db.Entry(tblUser).Property(p => p.IsActive).IsModified = false;
                db.Entry(tblUser).Property(p => p.Password).IsModified = false;
                db.Entry(tblUser).Property(p => p.UserRole_FK).IsModified = false;
                if (file.ContentLength == 0)
                {
                    db.Entry(tblUser).Property(p => p.Profile).IsModified = false;

                }
                await db.SaveChangesAsync();
            }
            return RedirectToAction("MyAccount", new { id = tblUser.UserID });
        }
        public async Task<ActionResult> Courses()
        {
            tblUser user = await db.tblUsers.FindAsync(GetUser().UserID);
            ViewBag.Enroll = await db.tblEnrollments.Where(x => x.StudentID == user.UserID).ToListAsync();
            return View(user);
        }
        public async Task<ActionResult> Exams()
        {
            tblUser user = await db.tblUsers.FindAsync(GetUser().UserID);
            var Exam = await db.tblExamStudentMappings.Where(x => x.StudentID == user.UserID).Include(t => t.tblExam).Include(t=>t.tblExam.tblTopic).ToListAsync();
            ViewBag.Exam = Exam;
            return View(user);
        }
        public async Task<ActionResult> Results()
        {
            tblUser user = await db.tblUsers.FindAsync(GetUser().UserID);          
            return View(user);
        }
        public async Task<ActionResult> Payments()
        {
            tblUser user = await db.tblUsers.FindAsync(GetUser().UserID);            
            return View(user);
        }
        public async Task<ActionResult> Certificate()
        {
            tblUser user = await db.tblUsers.FindAsync(GetUser().UserID);
            return View(user);
        }
        public tblUser GetUser()
        {
            tblUser user = (tblUser)Session["User"];
            if (user == null)
            {
                user = new SymphonyLtd.Models.SymphonyDBEntities().tblUsers.FirstOrDefault(x => x.Email == User.Identity.Name);
            }
            if (user != null)
            {
                return user;
            }
            else
            {
                AccountController account = new AccountController();
                account.Logout();
                return null;
            }

        }
    }
}