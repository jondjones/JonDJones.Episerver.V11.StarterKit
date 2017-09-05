namespace JonDJones.Website.Core.ViewModel.Base
{
    using System.Collections.Generic;
    using System.Linq;

    using EPiServer.Core;

    using JonDJones.Website.Core.Entities;
    using JonDJones.Website.Core.Pages;
    using JonDJones.Website.Interfaces;

    public class PreviewViewModel : BaseViewModel<StartPage>
    {
        public PreviewViewModel(StartPage currentPage, IWebsiteDependencies dependencies, IContent previewContent, IEnumerable<PreviewArea> areas)
            : base(currentPage, dependencies)
        {
            PreviewContent = previewContent;
            Areas = areas;
        }

        public IContent PreviewContent { get; }

        public IEnumerable<PreviewArea> Areas { get;  }
    }
}
