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
    public class AuthorizeAdminOrOwnerOfPostAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var authorized = base.AuthorizeCore(httpContext);
            if (!authorized)
            {
                // The user is not authenticated
                return false;
            }

            var user = httpContext.User;
            if (user.IsInRole("Admin"))
            {
                // Administrator => let him in
                return true;
            }

            var rd = httpContext.Request.RequestContext.RouteData;
            var id = rd.Values["id"] as string;
            if (string.IsNullOrEmpty(id))
            {
                // No id was specified => we do not allow access
                return false;
            }

            return IsOwnerOfPost(user.Identity.Name, id);
        }

        private bool IsOwnerOfPost(string username, string postId)
        {
            // TODO: you know what to do here
            throw new NotImplementedException();
        }
       
    }
}