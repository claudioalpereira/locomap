using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MVCSignalRtest2.Models;

namespace MVCSignalRtest2
{
    public static class MailSender
    {
        public static void SendMail(string body)
        {
            var email = new Email
            {
                To = "claudio.leal.pereira@gmail.com",
                UserName = "myUserName",
                Comment = body
            };

            email.Send();
        }
    }
}