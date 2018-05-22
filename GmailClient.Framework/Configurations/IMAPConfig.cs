namespace GmailClient.Framework.Configurations
{
    public struct IMAPConfig
    {
        /// <summary>
        /// default subject text
        /// </summary>
        public const string NO_SUBJECT_TEXT = "(no subject)";

        /// <summary>
        /// default mailbox name
        /// </summary>
        public const string DEFAULT_MAIL_BOX = "INBOX";

        /// <summary>
        /// imap host
        /// </summary>
        public const string IMAP_HOST = "imap.gmail.com";

        /// <summary>
        /// imap ssl port
        /// </summary>
        public const int IMAP_PORT = 993;
    }
}
