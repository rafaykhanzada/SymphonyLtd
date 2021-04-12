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
using SymphonyLtd.ViewModels;

namespace SymphonyLtd.Controllers
{
    public class AccountController : Controller
    {
        private SymphonyDBEntities db = new SymphonyDBEntities();

        // GET: Account
        [HttpGet]
        public ActionResult Login()
        {            
            return View();
        }
        [HttpGet]
        public ActionResult SignUp()
        {            
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> SignUp(Signup model)
        {
            if (ModelState.IsValid)
            {
                var tblUsers =await db.tblUsers.Where(x=>x.Email==model.Email || x.PhoneNumber==model.PhoneNumber).ToListAsync();
                if (tblUsers.Count()==0)
                {
                    tblUser user = new tblUser();
                    user.Name = model.Name;
                    user.Email = model.Email;
                    user.Password = model.Password;
                    user.PhoneNumber = model.PhoneNumber;
                    user.CreatedOn = DateTime.Now;
                    user.Address = null;
                    user.DeletedBy = null;
                    user.IsActive = true;
                    db.tblUsers.Add(user);
                    await db.SaveChangesAsync();
                }
            }
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Login(Login model)
        {
            var tblUsers = db.tblUsers.Include(t => t.tblRole);
            var user =await tblUsers.Where(x => x.Email == model.Email && x.Password == model.Password).ToListAsync();
            if (user.Count()==1)
            {
                Session["User"] = user[0];
                if (user[0].tblRole.RoleID==1)
                {
                    return RedirectToAction("Index", "Dashboard", new { area = "Admin" });
                }
                else if (user[0].tblRole.RoleID == 2)
                {
                    return RedirectToAction("Home", "Index");
                }
                else
                {
                    return View();
                }
            }
            return View();
        }
        
    }
}
