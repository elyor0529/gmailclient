using log4net.Config;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GmailClient.WebUI.Startup))]
[assembly: XmlConfigurator(ConfigFile = "log4net.config", Watch = true)]
namespace GmailClient.WebUI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
