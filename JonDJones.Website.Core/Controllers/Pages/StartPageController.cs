namespace JonDJones.Website.Core.Controllers.Pages
{
    using System.Web.Mvc;

    using JonDJones.Website.Core.Controllers.Base;
    using JonDJones.Website.Core.Pages;
    using JonDJones.Website.Core.ViewModel.Pages;

    public class StartPageController : BasePageController<StartPage>
    {
        public ActionResult Index(StartPage currentPage)
        {
            return View("Index", new StartPageViewModel(currentPage, WebsiteDependencies));
        }
    }
}