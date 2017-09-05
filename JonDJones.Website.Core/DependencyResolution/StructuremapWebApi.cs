using System.Web.Http;
using JonDJones.Website.Core.DependencyResolution;

[assembly: WebActivatorEx.PostApplicationStartMethod(typeof(JonDJones.Website.Core.App_Start.StructuremapWebApi), "Start")]

namespace JonDJones.Website.Core.App_Start
{
    public static class StructuremapWebApi
    {
        public static void Start()
        {
            var container = StructuremapMvc.StructureMapDependencyScope.Container;
            GlobalConfiguration.Configuration.DependencyResolver = new StructureMapWebApiDependencyResolver(container);
        }
    }
}