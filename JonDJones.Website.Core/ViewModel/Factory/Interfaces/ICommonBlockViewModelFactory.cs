using EPiServer.Core;
using EPiServer.SpecializedProperties;
using JonDJones.Website.Core.Blocks;
using JonDJones.Website.Core.ViewModel.Common;
using JonDJones.Website.Core.Blocks.Common;

namespace JonDJones.Website.Core.ViewModel.Factory.Interfaces
{
    public interface ICommonBlockViewModelFactory
    {
        ImageViewModel CreateImageViewModel(ImageBlock image);

        LinkViewModel CreateLinkViewModel(LinkItem linkItem);

        CallToActionViewModel CreateCallToActionViewModel(CallToActionBlock callToAction);
    }
}