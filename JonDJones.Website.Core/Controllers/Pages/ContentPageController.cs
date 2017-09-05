namespace JonDJones.Website.Core.Controllers.Pages
{
    using System.Web.Mvc;

    using EPiServer.Web.Mvc;

    using JonDJones.Website.Core.Controllers.Base;
    using JonDJones.Website.Core.Pages;
    using JonDJones.Website.Core.ViewModel.Pages;
    using JonDJones.Website.Shared.Resources;

    [ContentOutputCache(CacheProfile = GlobalConstants.CacheProfiles.Hour)]
    public class ContentPageController : BasePageController<ContentPage>
    {
        public ActionResult Index(ContentPage currentPage)
        {
            return View("Index", new ContentPageViewModel(currentPage, WebsiteDependencies));
        }
    }
}