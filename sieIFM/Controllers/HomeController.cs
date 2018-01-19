﻿using MVCSignalRtest2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCSignalRtest2.Controllers
{
    [Authorize]
    [AuthorizeConcurrentUser(false)]
    [MVCSignalRtest2.Utils.MyRequireHttps]
    public class HomeController : Controller
    {
        public ActionResult Index(string view)
        {
            int userTimeout = 30;
            int.TryParse(System.Web.Configuration.WebConfigurationManager.AppSettings["user_timeout"], out userTimeout);

            ViewBag.UserTimeout = userTimeout;

            return View(view??"Map4");
        }

        public ActionResult GetLocos()
        {
            var ms = MessagesRepository.GetAllMessages();
            return PartialView("_LocoTablePartial", ms);
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public JsonResult GetMessages()
        {
            var ms = MessagesRepository.GetAllMessages();
            return Json(ms, JsonRequestBehavior.AllowGet);
        }

        
    }
}