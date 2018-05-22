using System;
using System.Collections.Generic;
using GmailClient.Framework.Models;
using S22.Imap;

namespace GmailClient.Framework.Interfaces
{
    public interface IIMAPClient
    {

        /// <summary>
        /// get all mailbox names
        /// </summary>
        /// <returns></returns>
        IEnumerable<string> GetBoxes();

        /// <summary>
        /// get mails 
        /// </summary>
        /// <param name="mailbox">mailbox name</param>
        /// <param name="last">last date</param>
        /// <param name="keyword">key word</param>
        /// <returns></returns>
        IEnumerable<MailMessageModel> GetMails(string mailbox, DateTime last, string keyword = null);

        /// <summary>
        /// get mail
        /// </summary>
        /// <param name="uid">messsage uunique id</param>
        /// <param name="mailbox">mailbox name</param>
        /// <returns></returns>
        MailMessageModel GetMail(uint uid, string mailbox);

        /// <summary>
        /// read
        /// </summary>
        /// <param name="uid">message unique id</param>
        /// <param name="mailbox">mailbox name</param>
        /// <returns></returns>
        bool Read(uint uid, string mailbox);

        /// <summary>
        /// delete
        /// </summary>
        /// <param name="uid">message unique id</param>
        /// <param name="mailbox">mailbox name</param>
        /// <returns></returns>
        bool Delete(uint uid, string mailbox);

    }
}
