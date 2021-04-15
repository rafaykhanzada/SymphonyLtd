using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SymphonyLtd.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult Unauthorised()
        {
            Response.StatusCode = 401;
            return View("Unauthorised");
        }
        //404
        public ActionResult NotFound()
        {
            Response.StatusCode = 404;
            return View("NotFound");
        }
        //500
        public ActionResult ServerError()
        {
            Response.StatusCode = 500;
            return View("ServerError");
        }
        public ActionResult BadRequest()
        {
            Response.StatusCode = 400;
            return View("BadReques");
        }
    }
}