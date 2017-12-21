using System.Web.Mvc;
using BVNetwork.NotFound.Core.NotFoundPage;
using EPiServer.Web.Mvc;
using JonDJones.Website.Core.Controllers.Base;
using JonDJones.Website.Core.Pages;
using JonDJones.Website.Shared.Resources;
using JonDJones.Website.Core.ViewModel.Base;
using JonDJones.Website.Core.ViewModel.AdditionalProperties;

namespace JonDJones.Website.Core.Controllers.Pages
{
    [NotFoundPage]
    [ContentOutputCache(CacheProfile = GlobalConstants.CacheProfiles.Day)]
    public class PageNotFoundPageController : BasePageController<PageNotFoundPage>
    {
        public ActionResult Index(PageNotFoundPage currentPage)
        {
            Response.TrySkipIisCustomErrors = true;
            Response.StatusCode = 404;

            var pageViewModel = new PageViewModel<PageNotFoundPage, NoAdditionalProperties>(
                currentPage,
                new NoAdditionalProperties());

            return View("Index", pageViewModel);
        }
    }
}