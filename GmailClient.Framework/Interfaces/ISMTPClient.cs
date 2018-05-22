using System;
using System.Net.Mail;

namespace GmailClient.Framework.Interfaces
{
    public interface ISMTPClient
    {

        /// <summary>
        /// send email
        /// </summary>
        /// <param name="subject">message subject</param>
        /// <param name="body">message body</param>
        /// <param name="to">message to</param>
        /// <returns></returns>
        bool SendMessage(string subject, string body, string to);

    }
}
