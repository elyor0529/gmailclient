using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Autofac;
using GmailClient.WebUI.BLL.Modules;
using Autofac.Integration.Mvc;

namespace GmailClient.WebUI
{
    public static class IOCConfig
    {
        /// <summary>
        /// MVC Integration - http://docs.autofac.org/en/stable/integration/mvc.html
        /// </summary>
        public static void RegisterDependencies()
        {
            //IOC continer
            var builder = new ContainerBuilder();

            //current assembly
            var assembly = Assembly.GetExecutingAssembly();

            // Register your MVC controllers.
            builder.RegisterControllers(assembly).PropertiesAutowired();

            // Register model binders that require DI.
            builder.RegisterModelBinders(assembly);
            builder.RegisterModelBinderProvider();

            //  Register web abstractions like HttpContextBase.
            builder.RegisterModule<AutofacWebTypesModule>();
            builder.RegisterModule(new RepositoryModule());

            //  Enable property injection in view pages.
            builder.RegisterSource(new ViewRegistrationSource());

            // Enable property injection into action filters.
            builder.RegisterFilterProvider();

            // Set the dependency resolver to be Autofac.
            var container = builder.Build();

            //Set continer to MVC dependency kernel 
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
           
        }
    }
}