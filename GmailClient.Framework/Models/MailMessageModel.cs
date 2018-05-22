using System.Net.Mail;

namespace GmailClient.Framework.Models
{
    public class MailMessageModel
    {

        /// <summary>
        /// message unique id
        /// </summary>
        public uint UId { get; set; }

        /// <summary>
        /// generic mail data
        /// </summary>
        public MailMessage Mail { get; set; }

        /// <summary>
        /// read status
        /// </summary>
        public bool IsSeen { get; set; }

    }
}
