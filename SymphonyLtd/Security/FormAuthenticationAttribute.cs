using SymphonyLtd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SymphonyLtd.Security
{
    public class FormAuthenticationAttribute : AuthorizeAttribute
    {
        public string RoleId { get; set; }
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            
            if (!base.AuthorizeCore(httpContext))
            {
                return false;
            }

            if (string.IsNullOrEmpty(RoleId))
            {
                return true;
            }
            else
            {
                string Email = httpContext.User.Identity.Name;
                SymphonyDBEntities db = new SymphonyDBEntities();
                var userTypeId = db.tblUsers.FirstOrDefault(x => x.Email == Email).UserRole_FK;
                if (RoleId.Contains(userTypeId.ToString()))
                {
                    return true;
                }
                else
                {
                    //UnAutherize
                    return false;
                }
            }
        }

        //Handel Unauthorize Request
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                //unauthoriza page
                RouteValueDictionary route = new RouteValueDictionary(
                    new
                    {
                        area = "",
                        controller = "Error",
                        action = "Unauthorize"
                    }
                    );

                filterContext.Result = new RedirectToRouteResult(route);
            }
            else
            {
                //redirect to login
                base.HandleUnauthorizedRequest(filterContext);
            }
        }

    }

 
    }