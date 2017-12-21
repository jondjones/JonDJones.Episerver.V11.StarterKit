using System.Web.Mvc;
using EPiServer;
using EPiServer.Core;
using EPiServer.SpecializedProperties;
using JonDJones.Website.Core.ViewModel.Factory.Interfaces;
using JonDJones.Website.Shared.Helpers;
using JonDJones.Website.Core.ViewModel.Common;
using JonDJones.Website.Core.Blocks.Common;
using JonDJones.Website.Core.Blocks;
using EPiServer.Web.Mvc.Html;

namespace DeLonghi.Web.Business.ViewModelFactory
{
    public class CommonBlockViewModelFactory : ICommonBlockViewModelFactory
    {
        private readonly IContentRepository _contentRepository;

        private readonly UrlHelper _urlHelper;

        public CommonBlockViewModelFactory(
            IContentRepository contentRepository,
            UrlHelper urlHelper)
        {
            Guard.ValidateObject(contentRepository);
            Guard.ValidateObject(urlHelper);

            _contentRepository = contentRepository;
            _urlHelper = urlHelper;
        }

        public ImageViewModel CreateImageViewModel(ImageBlock image)
        {
            if (image == null)
            {
                return null;
            }

            var imageViewModel = new ImageViewModel
            {
                Url = GetFriendlyUrl(image.Image),
                AlternativeText = image.AlternativeText
            };

            return imageViewModel;
        }

        public LinkViewModel CreateLinkViewModel(PageReference linkReference, string displayText)
        {
            if (linkReference == null)
            {
                return null;
            }

            var linkViewModel = new LinkViewModel
            {
                Url = GetFriendlyUrl(linkReference),
                DisplayText = displayText
            };

            return linkViewModel;
        }

        public LinkViewModel CreateLinkViewModel(LinkItem linkItem)
        {
            if (linkItem == null)
            {
                return null;
            }

            var linkViewModel = new LinkViewModel
            {
                Url = GetFriendlyUrl(linkItem.Href),
                DisplayText = linkItem.Text
            };

            return linkViewModel;
        }

        public CallToActionViewModel CreateCallToActionViewModel(CallToActionBlock callToAction)
        {
            if (callToAction == null)
            {
                return null;
            }

            return new CallToActionViewModel
            {
                RenderCallToAction = callToAction.CallToActionLink != null,
                Url = GetFriendlyUrl(callToAction.CallToActionLink),
                DisplayText = callToAction.CallToActionText
            };
        }

        private string GetFriendlyUrl(ContentReference itemReference)
        {
            return itemReference != null
                 ? _urlHelper.ContentUrl(itemReference)
                 : string.Empty;
        }

        private string GetFriendlyUrl(Url url)
        {
            return url != null
                ? _urlHelper.ContentUrl(url)
                : string.Empty;
        }
    }
}