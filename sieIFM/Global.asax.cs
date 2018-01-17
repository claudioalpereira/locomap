﻿using MVCSignalRtest2.Log;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace MVCSignalRtest2
{
    public class MvcApplication : System.Web.HttpApplication
    {
        string connString = ConfigurationManager.ConnectionStrings
        ["DefaultConnection"].ConnectionString;
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            //Start SqlDependency with application initialization
            SqlDependency.Start(connString);
        }

        protected void Application_Error()
        {
            var ex = Server.GetLastError();
          
            //log the error!
            Logger.Log(ex);

            try
            {
                MailSender.Send("Uma excepção foi amandada!", ex.ToString());
            }
            catch (Exception e)
            {
                Logger.Log(e);
            }
        }

        protected void Application_End()
        {
            //Stop SQL dependency
            SqlDependency.Stop(connString);
        }

        void Session_Start(object sender, EventArgs e)
        {
            HttpContext.Current.Session.Add("ifmAppSession", string.Empty);
        }
    }
}
