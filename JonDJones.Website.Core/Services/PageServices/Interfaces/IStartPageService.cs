namespace JonDJones.Website.Core.Dependencies.RepositoryDependencies.Interfaces
{
    using JonDJones.Website.Core.Pages;
    using JonDJones.Website.Core.ViewModel.Pages;
    using JonDJones.Website.Interfaces;

    public interface IStartPageService : IRepository<StartPage>
    {
        StartPage Homepage { get; }

        StartPageAdditionalProperties GetStartPageAdditionalProperties(StartPage startpage);
    }
}