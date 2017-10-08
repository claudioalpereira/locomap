﻿using MVCSignalRtest2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCSignalRtest2.Controllers
{
    [Authorize]
    //[RequireHttps]
    public class HomeController : Controller
    {
        public ActionResult Index(string view)
        {
            return View(view??"Map4");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

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