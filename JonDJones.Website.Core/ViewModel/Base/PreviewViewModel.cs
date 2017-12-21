using System.Collections.Generic;

using EPiServer.Core;

using JonDJones.Website.Core.Entities;
using JonDJones.Website.Core.Pages;
using JonDJones.Website.Core.ViewModel.Pages;

namespace JonDJones.Website.Core.ViewModel.Base
{
    public class PreviewViewModel<T, IAdditionalProperties> : PageViewModel<StartPage, StartPageAdditionalProperties>
    {
        public PreviewViewModel(
            StartPage currentPage,
            StartPageAdditionalProperties additionalProperties,
            IContent previewContent,
            IEnumerable<PreviewArea> areas)
            : base(currentPage, additionalProperties)
        {
            PreviewContent = previewContent;
            Areas = areas;
        }

        public IContent PreviewContent { get; }

        public IEnumerable<PreviewArea> Areas { get;  }
    }
}
