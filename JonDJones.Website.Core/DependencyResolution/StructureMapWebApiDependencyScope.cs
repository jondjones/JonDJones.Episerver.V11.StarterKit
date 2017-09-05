namespace JonDJones.Website.Core.DependencyResolution
{
    using System.Web.Http.Dependencies;
    using StructureMap;

    public class StructureMapWebApiDependencyScope : StructureMapDependencyScope, IDependencyScope
    {
        public StructureMapWebApiDependencyScope(IContainer container)
            : base(container)
        {
        }
    }
}
