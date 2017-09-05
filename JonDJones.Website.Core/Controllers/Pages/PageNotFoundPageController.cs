namespace JonDJones.Website.Core.Controllers.Pages
{
    using System.Web.Mvc;
    using BVNetwork.NotFound.Core.NotFoundPage;
    using EPiServer.Web.Mvc;
    using JonDJones.Website.Core.Controllers.Base;
    using JonDJones.Website.Core.Pages;
    using JonDJones.Website.Core.ViewModel.Pages;
    using JonDJones.Website.Shared.Resources;

    [NotFoundPage]
    [ContentOutputCache(CacheProfile = GlobalConstants.CacheProfiles.Day)]
    public class PageNotFoundPageController : BasePageController<PageNotFoundPage>
    {
        public ActionResult Index(PageNotFoundPage currentPage)
        {
            Response.TrySkipIisCustomErrors = true;
            Response.StatusCode = 404;

            return View("Index", new PageNotFoundPageViewModel(currentPage, WebsiteDependencies));
        }
    }
}