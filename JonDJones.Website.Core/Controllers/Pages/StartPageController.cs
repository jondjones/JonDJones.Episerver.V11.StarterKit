using System.Web.Mvc;
using JonDJones.Website.Core.Controllers.Base;
using JonDJones.Website.Core.Pages;
using JonDJones.Website.Core.ViewModel.Pages;
using JonDJones.Website.Core.ViewModel.Base;

namespace JonDJones.Website.Core.Controllers.Pages
{
    public class StartPageController : BasePageController<StartPage>
    {
        public ActionResult Index(StartPage currentPage)
        {
            var pageViewModel = new PageViewModel<StartPage, StartPageAdditionalProperties>(
                currentPage,
                PageServices.StartPageService.GetStartPageAdditionalProperties(currentPage));

            return View("Index", pageViewModel);
        }
    }
}