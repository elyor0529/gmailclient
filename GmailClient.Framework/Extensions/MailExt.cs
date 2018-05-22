using System;
using System.Net.Mail;
using System.Net.NetworkInformation;

namespace GmailClient.Framework.Extensions
{
    public static class MailExt
    {

        /// <summary>
        /// get unique id
        /// </summary>
        /// <param name="message">generic message content</param>
        /// <returns></returns>
        public static uint GetUId(this MailMessage message)
        {
            if (message.AlternateViews.Count == 0)
                throw new Exception("Message unique id is not null");

            var bytes = Guid.Parse(message.AlternateViews[0].ContentId).ToByteArray();
            var uid = BitConverter.ToUInt32(bytes, 0);

            return uid;
        }
    }
}
