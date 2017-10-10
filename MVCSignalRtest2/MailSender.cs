using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace MVCSignalRtest2
{
    public class MailSender
    {
        public static void Send(string subject, string htmlBody)
        {
            SmtpClient SmtpServer = new SmtpClient("smtp.live.com");
            var mail = new MailMessage();
           // mail.From = new MailAddress("claudio_leal@hotmail.com");
            mail.To.Add("claudio.leal.pereira@gmail.com");
            mail.Subject = subject;
            mail.IsBodyHtml = true;
            mail.Body = htmlBody;
            //SmtpServer.Port = 587;
            //SmtpServer.UseDefaultCredentials = false;
            //SmtpServer.Credentials = new System.Net.NetworkCredential("claudio_leal@hotmail.com", "anasofia");
            //SmtpServer.EnableSsl = true;
            SmtpServer.Send(mail);
        }
    }
}