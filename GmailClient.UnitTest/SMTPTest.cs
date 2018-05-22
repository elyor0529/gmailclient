using GmailClient.Framework.Interfaces;
using GmailClient.Framework.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GmailClient.UnitTest
{
    [TestClass]
    public class SMTPTest : BaseTest
    {
        private readonly ISMTPClient _client;

        public SMTPTest()
        {
            _client = new SMTPClientRepo(Usr, Psw);
        }

        [TestMethod]
        [Description("Send message")]
        public void TestMethod1()
        {
            var subjet = "test";
            var body = "test";
            var to = "elyor@outlook.com;levdeo@outlook.com;";

            Assert.IsTrue(_client.SendMessage(subjet, body, to));
        }
    }
}
