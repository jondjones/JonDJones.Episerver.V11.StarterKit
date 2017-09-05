namespace JonDJones.Website
{
    using System;
    using System.Web;
    using System.Web.Http;
    using System.Web.Mvc;
    using System.Web.Routing;

    using log4net;

    using JonDJones.Website.Core.MVC;

    public class EPiServerApplication : EPiServer.Global
    {
        private readonly ILog log = LogManager.GetLogger(typeof(EPiServerApplication));

        protected void Application_Start()
        {
            ViewEngines.Engines.Insert(0, new CustomViewEngine());
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            var error = Server.GetLastError();
            var con = HttpContext.Current;

            if (con == null)
            {
                return;
            }

            var url = con.Request.Url.ToString();
            log.ErrorFormat($"url={url}", error);
        }
    }
}