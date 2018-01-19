
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Web.Mvc;
using RequireHttpsAttributeBase = System.Web.Mvc.RequireHttpsAttribute;


// https://support.appharbor.com/discussions/problems/78440-getting-error-too-many-redirects-on-appharbor
namespace MVCSignalRtest2.Utils
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true,
         AllowMultiple = false)]
    public class MyRequireHttpsAttribute : RequireHttpsAttributeBase
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext == null)
            {
                throw new ArgumentNullException("filterContext");
            }

            if (filterContext.HttpContext.Request.IsSecureConnection)
            {
                return;
            }

            if (string.Equals(filterContext.HttpContext.Request.Headers["X-Forwarded-Proto"],
                "https",
                StringComparison.InvariantCultureIgnoreCase))
            {
                return;
            }

            if (filterContext.HttpContext.Request.IsLocal)
            {
                return;
            }

            HandleNonHttpsRequest(filterContext);
        }
    }

    public class Utils
    {
        public static async System.Threading.Tasks.Task<bool> CreateUserIfNotExists(Controllers.AccountController ctrl, string username, string password)
        {
            if(ctrl.SignInManager.UserManager.FindByEmail(username) == null)
            {
                var user = new Models.ApplicationUser { UserName = username, Email = password };
                await ctrl.SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                return true;
            }
            return false;
        }
    }
}