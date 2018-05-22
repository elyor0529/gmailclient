using GmailClient.Framework.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GmailClient.UnitTest
{
    [TestClass]
    public class AuthorizeTest : BaseTest
    {
        [TestMethod]
        [Description("Authorize test")]
        public void TestMethod1()
        {
            var authorized = AuthenticationHelper.Login(Usr, Psw);

            Assert.IsTrue(authorized);
        }
    }
}
