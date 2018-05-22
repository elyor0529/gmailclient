using Autofac;

namespace GmailClient.WebUI.BLL.Modules
{
    public class RepositoryModule : Module
    {
        /// <summary>
        /// register my repo
        /// </summary>
        /// <param name="builder">Authofact binded continer builder</param>
        protected override void Load(ContainerBuilder builder)
        {
            //register types with intherting [Interfaces]
            builder.RegisterAssemblyTypes(System.Reflection.Assembly.Load(Settings.CLR.FRAMEWORK_ASSEMBLY))
                .Where(t => t.Name.EndsWith("Repo"))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
        }
    }
}
