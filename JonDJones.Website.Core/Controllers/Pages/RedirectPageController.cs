namespace JonDJones.Website.Core.Controllers.Pages
{
    using System.Web.Mvc;

    using JonDJones.Website.Core.Controllers.Base;
    using JonDJones.Website.Core.Pages;

    public class RedirectPageController : BasePageController<RedirectPage>
    {
        public ActionResult Index(RedirectPage currentPage)
        {
            var returnLink = currentPage.CustomRedirect ?? currentPage.ParentLink;
            return Redirect(WebsiteDependencies.LinkResolver.ResolveUrl(returnLink));
        }
    }
}