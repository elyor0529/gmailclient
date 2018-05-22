using System;
using System.Linq;
using GmailClient.Framework.Interfaces;
using GmailClient.Framework.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using S22.Imap;
using GmailClient.Framework.Configurations;

namespace GmailClient.UnitTest
{
    [TestClass]
    public class IMAPTest : BaseTest
    {
        private readonly IIMAPClient _repo;

        public IMAPTest()
        {
            _repo = new IMAPClientRepo(Usr, Psw);
        }

        [TestMethod]
        [Description("Folders")]
        public void TestMethod1()
        {
            var folders = _repo.GetBoxes().ToArray();

            foreach (var folder in folders.Take(10))
            {
                Console.WriteLine(folder);
            }

            Assert.IsTrue(folders.Length > 0);
        }

        [TestMethod]
        [Description("Messages")]
        public void TestMethod2()
        {
            var messages = _repo.GetMails(IMAPConfig.DEFAULT_MAIL_BOX, DateTime.Now.Date).ToArray();

            foreach (var message in messages.Take(10))
            {
                Console.WriteLine("#{0} - {1} {2} {3}", message.UId, message.Mail.From, message.Mail.Subject, message.Mail.Date());
            }

            Assert.IsTrue(messages.Length > 0);
        }

        [TestMethod]
        [Description("Read")]
        public void TestMethod3()
        {
            //#9980 - "Elyor Latipov" <elyor@outlook.com> test 01/18/2016 12:23:44
            uint uid = 9980;

            Assert.IsTrue(_repo.Read(uid, IMAPConfig.DEFAULT_MAIL_BOX));
        }


        [TestMethod]
        [Description("Delete")]
        public void TestMethod4()
        {
            //#9980 - "Elyor Latipov" <elyor@outlook.com> test 01/18/2016 12:23:44
            uint uid = 9980;

            Assert.IsTrue(_repo.Delete(uid, IMAPConfig.DEFAULT_MAIL_BOX));
        }


        [TestMethod]
        [Description("Message")]
        public void TestMethod5()
        {
            //#9980 - "Elyor Latipov" <elyor@outlook.com> test 01/18/2016 12:23:44
            uint uid = 9980;
            var message = _repo.GetMail(uid, IMAPConfig.DEFAULT_MAIL_BOX);

            Console.WriteLine("#{0} - {1} {2} {3}", message.UId, message.Mail.From, message.Mail.Subject, message.Mail.Date());

            Assert.IsNotNull(message);
        }
    }
}
