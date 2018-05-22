using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using log4net.Config;

namespace GmailClient.WebUI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start(object sender,EventArgs args)
        {
            //log4net configure
            XmlConfigurator.Configure(new FileInfo(Server.MapPath("~/log4net.config")));

            //basic configs
            AreaRegistration.RegisterAllAreas(); 
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //ioc configure
            IOCConfig.RegisterDependencies();
        }
    }
}
