using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace MVCSignalRtest2.Controllers
{
    public class MailController : Controller
    {
        public ActionResult Send(string message)
        {

            MailSender.Send("Olá", "<h1>Panasca</h1>");
            return RedirectToAction("Home");
        }
    }
}