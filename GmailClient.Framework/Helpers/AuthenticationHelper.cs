using System;
using GmailClient.Framework.Configurations;
using S22.Imap;

namespace GmailClient.Framework.Helpers
{
    public static class AuthenticationHelper
    {

        /// <summary>
        /// check authorization
        /// </summary>
        /// <param name="usr">Gmail user name</param>
        /// <param name="psw">Gmail password</param>
        /// <returns></returns>
        public static bool Login(string usr, string psw)
        {
            if (string.IsNullOrEmpty(usr))
                throw new ArgumentNullException("usr");

            if (string.IsNullOrWhiteSpace(psw))
                throw new ArgumentNullException("psw");

            using (var client = new ImapClient(IMAPConfig.IMAP_HOST, IMAPConfig.IMAP_PORT, usr, psw, AuthMethod.Login, true, (sender, certificate, chain, errors) => true))
            {
                client.Logout();
            }

            return true;
        }
    }
}
