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

    public class AjaxController : Controller
    {
        private SymphonyDBEntities db = new SymphonyDBEntities();

        // GET: Admin/Ajax
        [HttpGet]
        public  List<tblExamStudentMapping> GetAllStuentForExamByID(string StudentIds)
        {
            if (!string.IsNullOrEmpty(StudentIds))
            {
                List<tblExamStudentMapping> studentMappings = new List<tblExamStudentMapping>();
                var StudentList = StudentIds.Split(',');
                foreach (var item in StudentList)
                {
                    var tblExamStudentMappings =  db.tblExamStudentMappings.Where(x => x.StudentID == Convert.ToInt32(item)).FirstOrDefault();
                    studentMappings.Add(tblExamStudentMappings);

                }
                return studentMappings;
            }
            else
            {
                return null;
            }
        }
        [HttpPost]
        public async Task<bool> ApplicationSubmit(tblApplication model)
        {
            if (model!=null)
            {
                db.tblApplications.Add(model);
                await db.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
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
