using JonDJones.Website.Core.Pages.Base;
using JonDJones.Website.Interfaces;
using JonDJones.Website.Shared.Helpers;

namespace JonDJones.Website.Core.ViewModel.Base
{
    public class PageViewModel<T, IAdditionalProperties> : IPageViewModel<T> where T : GlobalBasePage
    {
        public PageViewModel(T currentPage,
                             IAdditionalProperties additionalProperties)
        {
            Guard.ValidateObject(currentPage);

            CurrentPage = currentPage;
            AdditionalProperties = additionalProperties;
        }

        public T CurrentPage { get; }

        public ILayoutViewModel Layout { get; set; }

        public IAdditionalProperties AdditionalProperties { get; }
    }
}