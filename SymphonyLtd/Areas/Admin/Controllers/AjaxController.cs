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
