using JonDJones.Website.Core.Pages;
using JonDJones.Website.Core.ViewModel.Pages;
using JonDJones.Website.Interfaces;

namespace JonDJones.Website.Core.Dependencies.RepositoryDependencies.Interfaces
{
    public interface IContentPageService : IRepository<ContentPage>
    {
        ContentPageAdditionalProperties GetAdditionalProperties(ContentPage startpage);
    }
}