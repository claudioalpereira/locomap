using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCSignalRtest2.Models
{
    // https://stackoverflow.com/a/42493910
    // https://stackoverflow.com/a/5835631
    public static class SessionManager
    {
        private static List<User> _sessions = new List<User>();

        public static void RegisterLogin(User user)
        {
            if (user != null)
            {
                _sessions.RemoveAll(u => u.UserName == user.UserName);
                _sessions.Add(user);
            }
        }

        public static void DeregisterLogin(User user)
        {
            if (user != null)
                _sessions.RemoveAll(u => u.UserName == user.UserName && u.SessionId == user.SessionId);
        }

        public static bool ValidateCurrentLogin(User user)
        {
            return user != null && _sessions.Any(u => u.UserName == user.UserName && u.SessionId == user.SessionId);
        }

        public class User
        {
            public string UserName { get; set; }
            public string SessionId { get; set; }
        }
    }

    // https://stackoverflow.com/questions/13264496/asp-net-mvc-4-custom-authorize-attribute-with-permission-codes-without-roles
    public class AuthorizeConcurrentUserAttribute : System.Web.Mvc.AuthorizeAttribute
    {
        private bool _allow;
        public AuthorizeConcurrentUserAttribute(bool allow)
        {
            _allow = allow;
        }
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var isAuthorized = base.AuthorizeCore(httpContext);
            if (!isAuthorized)
            {
                return false;
            }
            
            return _allow || SessionManager.ValidateCurrentLogin(new SessionManager.User { UserName = httpContext.User.Identity.Name, SessionId = httpContext.Session.SessionID });
        }
        /*
        protected override void HandleUnauthorizedRequest(System.Web.Mvc.AuthorizationContext filterContext)
        {
            filterContext.Result = new System.Web.Mvc.RedirectToRouteResult(
                        new System.Web.Routing.RouteValueDictionary(
                            new
                            {
                                controller = "Account",
                                action = "Login"
                            })
                        );
        }
        */
    }


}