namespace JonDJones.Website.Core.DependencyResolution
{
    using System.Web.Http.Dependencies;
    using StructureMap;

    public class StructureMapWebApiDependencyResolver : StructureMapWebApiDependencyScope, IDependencyResolver
    {
        public StructureMapWebApiDependencyResolver(IContainer container)
            : base(container)
        {
        }

        public IDependencyScope BeginScope()
        {
            IContainer child = Container.GetNestedContainer();
            return new StructureMapWebApiDependencyResolver(child);
        }
    }
}