using System.Web.Mvc;

using EPiServer.Web.Mvc;

using JonDJones.Website.Core.Controllers.Base;
using JonDJones.Website.Core.Pages;
using JonDJones.Website.Core.ViewModel.Pages;
using JonDJones.Website.Shared.Resources;
using JonDJones.Website.Core.ViewModel.Base;

namespace JonDJones.Website.Core.Controllers.Pages
{
    [ContentOutputCache(CacheProfile = GlobalConstants.CacheProfiles.Hour)]
    public class ContentPageController : BasePageController<ContentPage>
    {
        public ActionResult Index(ContentPage currentPage)
        {
            var pageViewModel = new PageViewModel<ContentPage, ContentPageAdditionalProperties>(
                currentPage,
                PageServices.ContentPageService.GetAdditionalProperties(currentPage));

            return View("Index", pageViewModel);
        }
    }
}