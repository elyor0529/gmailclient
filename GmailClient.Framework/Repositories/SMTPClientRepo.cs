using System;
using System.Net;
using System.Net.Mail;
using System.Text;
using GmailClient.Framework.Configurations;
using GmailClient.Framework.Interfaces;

namespace GmailClient.Framework.Repositories
{
    public class SMTPClientRepo : ISMTPClient
    {
        private readonly string _usr;
        private readonly SmtpClient _client;

        public SMTPClientRepo(string usr, string psw)
        {
            if (string.IsNullOrEmpty(usr))
                throw new ArgumentNullException("usr");

            if (string.IsNullOrWhiteSpace(psw))
                throw new ArgumentNullException("psw");
             
            _client = new SmtpClient
            {
                UseDefaultCredentials = false,
                EnableSsl = true,
                Host = SMTPConfig.SMTP_HOST,
                Port = SMTPConfig.SMTP_PORT,
                Credentials = new NetworkCredential(usr, psw),
                DeliveryMethod = SmtpDeliveryMethod.Network
            };
 
            _usr = usr;
        }
         
        public bool SendMessage(string subject, string body, string to)
        {
            if (string.IsNullOrEmpty(subject))
                throw new ArgumentNullException("subject");
             
            if (string.IsNullOrWhiteSpace(to))
                throw new ArgumentNullException("to");
             
            var message = new MailMessage
            {
                From = new MailAddress(_usr),
                Subject = subject,
                Body = body,
                BodyEncoding = Encoding.UTF8,
                DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure,
                IsBodyHtml = false,
                SubjectEncoding = Encoding.UTF8
            };

            if (to.IndexOf(";", StringComparison.InvariantCultureIgnoreCase) != -1)
            {
                var toEmails = to.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);

                foreach (var email in toEmails)
                {
                    message.To.Add(new MailAddress(email));
                }

            }
            else
            {
                message.To.Add(new MailAddress(to));
            }

            _client.Send(message);

            return true;
        } 

    }
}
