namespace JonDJones.Website.Core.EpiserverConfiguration
{
    using System.Web.Mvc;
    using EPiServer.Core;
    using EPiServer.ServiceLocation;

    using JonDJones.Website.Core.EpiserverConfiguration.ContentAreaRenderer;

    public static class ContentAreaHtmlHelpers
    {
        private static Injected<NoDivContentAreaRenderer> noDivContentAreaRenderer;

        public static void RenderNoDivContentAreaRenderer(this HtmlHelper htmlHelper, ContentArea contentArea)
        {
            noDivContentAreaRenderer.Service.Render(htmlHelper, contentArea);
        }
    }
}