using System;
using System.Collections.Generic;
using System.Linq;
using GmailClient.Framework.Configurations;
using GmailClient.Framework.Interfaces;
using GmailClient.Framework.Models;
using S22.Imap;

namespace GmailClient.Framework.Repositories
{
    public class IMAPClientRepo : IIMAPClient
    {
        private readonly ImapClient _client;

        public IMAPClientRepo(string usr, string psw)
        {
            if (string.IsNullOrEmpty(usr))
                throw new ArgumentNullException("usr");

            if (string.IsNullOrWhiteSpace(psw))
                throw new ArgumentNullException("psw");

            _client = new ImapClient(IMAPConfig.IMAP_HOST, IMAPConfig.IMAP_PORT, usr, psw, AuthMethod.Login, true, (sender, certificate, chain, errors) => true);
        }

        public IEnumerable<string> GetBoxes()
        {
            return _client.ListMailboxes();
        }

        public IEnumerable<MailMessageModel> GetMails(string mailbox, DateTime before, string search = null)
        {

            var condition = SearchCondition.SentSince(before);

            if (!String.IsNullOrWhiteSpace(search))
                condition = condition.And(SearchCondition.Keyword(search));

            var uids = _client.Search(condition, mailbox);

            return uids.Select(s => new MailMessageModel
            {
                UId = s,
                Mail = _client.GetMessage(s, FetchOptions.HeadersOnly, false, mailbox),
                IsSeen = _client.GetMessageFlags(s, mailbox).Any(a => a == MessageFlag.Seen)
            }).AsEnumerable();
        }

        public MailMessageModel GetMail(uint uid, string mailbox)
        {
            if (uid <= 0)
                throw new ArgumentOutOfRangeException("uid");

            return new MailMessageModel
            {
                UId = uid,
                Mail = _client.GetMessage(uid, FetchOptions.Normal, false, mailbox)
            };
        }

        public bool Read(uint uid, string mailbox)
        {
            if (uid <= 0)
                throw new ArgumentOutOfRangeException("uid");

            var flags = _client.GetMessageFlags(uid, mailbox).ToList();

            if (!flags.Contains(MessageFlag.Seen))
            {
                flags.Add(MessageFlag.Seen);

                _client.SetMessageFlags(uid, mailbox, flags.ToArray());

            }

            return true;
        }

        public bool Delete(uint uid, string mailbox)
        {
            if (uid <= 0)
                throw new ArgumentOutOfRangeException("uid");

            _client.DeleteMessage(uid, mailbox);

            return true;
        }

    }
}
