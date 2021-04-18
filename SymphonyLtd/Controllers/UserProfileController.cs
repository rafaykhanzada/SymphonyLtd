using SymphonyLtd.Models;
using SymphonyLtd.Security;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SymphonyLtd.Controllers
{
    //[FormAuthentication(RoleId = "2")]
    public class UserProfileController : Controller
    {
        private SymphonyDBEntities db = new SymphonyDBEntities();
        // GET: UserProfile
        public async Task<ActionResult> MyAccount(int id)
        {
            tblUser user = await db.tblUsers.FindAsync(id);
            return View(user);
        }
        public async Task<ActionResult> Courses(int id)
        {
            tblUser user = await db.tblUsers.FindAsync(id);
            ViewBag.Enroll = await db.tblEnrollments.Where(x => x.StudentID == id).ToListAsync();
            return View(user);
        }
        public async Task<ActionResult> Exams(int id)
        {
            tblUser user = await db.tblUsers.FindAsync(id);
            var Exam = await db.tblExamStudentMappings.Where(x => x.StudentID == id).Include(t => t.tblExam).Include(t=>t.tblExam.tblTopic).ToListAsync();
            ViewBag.Exam = Exam;
            return View(user);
        }
        public async Task<ActionResult> Results(int id)
        {
            tblUser user = await db.tblUsers.FindAsync(id);
            //var Result = await db.tblResults.Where(x => x.tblExam.tblUser.UserID == id).Include(t => t.tblUser.tblCertificates).ToListAsync();
            //ViewBag.Results = Result;
            return View(user);
        }
        public async Task<ActionResult> Payments(int id)
        {
            tblUser user = await db.tblUsers.FindAsync(id);
            return View(user);
        }
    }
}