using Autofac;
using Autofac.Core;
using Autofac.Integration.Mvc;
using GmailClient.Framework.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GmailClient.WebUI.BLL.Helpers
{
    public class AutofactHelper
    {
        /// <summary>
        /// Resolve type an request scop
        /// </summary>
        /// <typeparam name="T">asking type</typeparam>
        /// <param name="parameters">ctor or argument paramas</param>
        /// <returns></returns>
        private static T DoResolve<T>(params Parameter[] parameters)
        {
            // cast to Autofact continer from MVC continer
            var container = (AutofacDependencyResolver)DependencyResolver.Current;

            //open per reqest scope
            using (var lifetime = container.RequestLifetimeScope.BeginLifetimeScope())
            {
                //do resolve it
                return lifetime.Resolve<T>(parameters);
            }
        }

        public static T GetClient<T>()
        {
            //current http context
            var context = HttpContext.Current;

            //resolving
            var client = DoResolve<T>
               (
               new NamedParameter(ParameterConfig.CREDENTIALS_USR_KEY, context.User.Identity.Name),
               new NamedParameter(ParameterConfig.CREDENTIALS_PSW_KEY, context.Session[Settings.KEYS.PSW_SESSION_KEY])
               );

            return client;
        }

    }
}