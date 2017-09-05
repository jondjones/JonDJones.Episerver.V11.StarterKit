namespace JonDJones.Website.Core.Controllers.Base
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    using EPiServer;
    using EPiServer.Core;
    using EPiServer.Framework.DataAnnotations;
    using EPiServer.Framework.Web;
    using EPiServer.Web;

    using JonDJones.Website.Core.Dependencies.RepositoryDependencies.Interfaces;
    using JonDJones.Website.Core.Entities;
    using JonDJones.Website.Core.Pages;
    using JonDJones.Website.Core.ViewModel.Base;
    using JonDJones.Website.Interfaces;
    using JonDJones.Website.Shared.Helpers;

    [TemplateDescriptor(
        Inherited = true,
        Tags = new[] { RenderingTags.Preview },
        TemplateTypeCategory = TemplateTypeCategories.MvcController)]
    public class PreviewController : Controller, IRenderTemplate<BlockData>
    {
        private readonly DisplayOptions _displayOptions;

        private readonly TemplateResolver _templateResolver;

        private readonly IWebsiteDependencies __websiteDependencies;

        private readonly IEpiserverContentRepositories _episerverContentRepositories;

        public PreviewController(
            DisplayOptions displayOptions,
            TemplateResolver templateResolver,
            IWebsiteDependencies _websiteDependencies,
            IEpiserverContentRepositories episerverContentRepositories)
        {
            Guard.ValidateObject(_websiteDependencies);
            Guard.ValidateObject(displayOptions);
            Guard.ValidateObject(templateResolver);
            Guard.ValidateObject(episerverContentRepositories);

            _displayOptions = displayOptions;
            _templateResolver = templateResolver;
            __websiteDependencies = _websiteDependencies;
            _episerverContentRepositories = episerverContentRepositories;
        }

        public ActionResult Index(IContent currentContent)
        {
            var startPage = _episerverContentRepositories.StartPageRepository.StartPage;
            var supportedAreas = GetSupportedPreviewAreas(currentContent);
            var viewModel = new PreviewViewModel(startPage, __websiteDependencies, currentContent, supportedAreas);

            return View("~/Views/Blocks/Preview.cshtml", viewModel);
        }

        private IEnumerable<PreviewArea> GetSupportedPreviewAreas(IContent content)
        {
            var previewModels = new List<PreviewArea>();

            foreach (var displayOption in _displayOptions)
            {
                var isSupported = IsTagSupported(content, displayOption.Tag);
                if (!isSupported)
                {
                    continue;
                }

                var previewDisplayOption =
                    new PreviewDisplayOption { Tag = displayOption.Tag, Name = displayOption.Name, };

                var previewArea = CreatePreviewArea(previewDisplayOption, content.ContentLink);
                previewModels.Add(previewArea);
            }

            return previewModels;
        }

        private PreviewArea CreatePreviewArea(PreviewDisplayOption previewDisplayOption, ContentReference contentReference)
        {
            var item = new ContentAreaItem
            {
                ContentLink = contentReference
            };

            var contentArea = __websiteDependencies.ContextResolver.AddContentAreaItem(new ContentArea(), item);

            var areaModel = new PreviewArea
            {
                ContentArea = contentArea,
                Supported = previewDisplayOption.IsSupported,
                AreaTag = previewDisplayOption.Tag,
                AreaName = previewDisplayOption.Name,
            };

            return areaModel;
        }

        private bool IsTagSupported(IContent content, string tag)
        {
            var templateModel = _templateResolver.Resolve(
                                      HttpContext,
                                      content.GetOriginalType(),
                                      content,
                                      TemplateTypeCategories.MvcPartial,
                                      tag);

            return templateModel != null;
        }
    }
}