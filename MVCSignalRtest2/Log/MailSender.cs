using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace MVCSignalRtest2.Log
{
    //https://stackoverflow.com/questions/9851319/how-to-add-smtp-hotmail-account-to-send-mail
    public class MailSender
    {
        public static void Send(string subject, string htmlBody)
        {

            SmtpClient SmtpServer = new SmtpClient();

            var recipients = ConfigurationManager.AppSettings["alertRecipients"].Split(',').Select(x => x.Trim()).ToList();

            recipients.ForEach(recipient => {
                var mail = new MailMessage();

                mail.To.Add(recipient);
                mail.Subject = subject;
                mail.IsBodyHtml = true;
                mail.Body = htmlBody;
                SmtpServer.Send(mail);                              
            });

            
        }
    }
}