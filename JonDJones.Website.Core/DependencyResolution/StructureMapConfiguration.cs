namespace JonDJones.Website.Core
{
    using System.Web.Mvc;

    using EPiServer.Framework;
    using EPiServer.Framework.Initialization;
    using EPiServer.ServiceLocation;

    using StructureMap;
    using StructureMap.Graph;

    [InitializableModule]
    [ModuleDependency(typeof(EPiServer.Web.InitializationModule))]
    public class StructureMapConfiguration : IConfigurableModule
    {
        public void ConfigureContainer(ServiceConfigurationContext context)
        {
            context.Container.Configure(ConfigureContainer);
            DependencyResolver.SetResolver(new StructureMapDependencyResolver(context.Container));
        }

        public void Initialize(InitializationEngine context)
        {
        }

        public void Preload(string[] parameters)
        {
        }

        public void Uninitialize(InitializationEngine context)
        {
        }

        private static void ConfigureContainer(ConfigurationExpression container)
        {
            container.Scan(scan =>
            {
                scan.TheCallingAssembly();
                scan.WithDefaultConventions();
                scan.Assembly("JonDJones.Website");
                scan.Assembly("JonDJones.Website.Shared");
                scan.Assembly("JonDJones.Website.Core");
            });
        }
    }
}