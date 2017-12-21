namespace JonDJones.Website.Core.StartUp
{
    using System.Web.Mvc;

    using EPiServer.ServiceLocation;

    using JonDJones.Website.Core.Dependencies.RepositoryDependencies.Interfaces;
    using JonDJones.Website.Core.Pages.Base;
    using JonDJones.Website.Core.Repository;
    using JonDJones.Website.Core.ViewModel.Factory;
    using JonDJones.Website.Interfaces;

    public class BasePageDataSetup : IResultFilter
    {
        internal Injected<IWebsiteDependencies> EpiServerDependencies { get; set; }

        internal Injected<IPageTypeServices> EpiserverContentRepositories { get; set; }

        internal Injected<IBlockHelper> BlockHelper { get; set; }

        public void OnResultExecuting(ResultExecutingContext filterContext)
        {
            var model = filterContext.Controller.ViewData.Model;

            var layoutModel = model as IPageViewModel<GlobalBasePage>;
            if (layoutModel == null)
            {
                return;
            }

            var dependencies = EpiServerDependencies.Service;
            var menuPageRepository = new MenuService(dependencies);
            var headerViewModelFactory = new HeaderViewModelFactory(menuPageRepository);

            var viewModel = new LayoutViewModelFactory(dependencies, EpiserverContentRepositories.Service, headerViewModelFactory);
            var layout = viewModel.CreateLayoutViewModel(layoutModel.CurrentPage);
            layoutModel.Layout = layout;
        }

        public void OnResultExecuted(ResultExecutedContext filterContext)
        {
        }
    }
}