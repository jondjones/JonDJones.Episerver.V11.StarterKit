namespace JonDJones.Website.Core.DependencyResolution
{
    using StructureMap.Configuration.DSL;
    using StructureMap.Graph;

    public class DefaultRegistry : Registry
    {
        public DefaultRegistry()
        {
            Scan(scan =>
                {
                    scan.TheCallingAssembly();
                    scan.WithDefaultConventions();
                    scan.Assembly("JonDJones.Website");
                    scan.Assembly("JonDJones.Website.Core");
                    scan.Assembly("JonDJones.Website.Interfaces");
                });
        }
    }
}