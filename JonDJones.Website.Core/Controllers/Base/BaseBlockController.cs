namespace JonDJones.Website.Core.Controllers.Base
{
    using System;
    using System.Collections.Generic;

    using EPiServer.Core;
    using EPiServer.ServiceLocation;
    using EPiServer.Web.Mvc;

    using JonDJones.Website.Core.Dependencies.RepositoryDependencies.Interfaces;
    using JonDJones.Website.Interfaces;
    using JonDJones.Website.Interfaces.Enums;
    using JonDJones.Website.Shared.Resources;

    public class BaseBlockController<TBlockData> : BlockController<TBlockData>
         where TBlockData : BlockData
    {
        private Injected<IWebsiteDependencies> websiteDependencies;

        private Injected<IEpiserverContentRepositories> episerverRepositoryDependencies;

        public IWebsiteDependencies WebsiteDependencies => websiteDependencies.Service;

        public IEpiserverContentRepositories EpiserverContentRepositories => episerverRepositoryDependencies.Service;

        public PageData CurrentPage => WebsiteDependencies.ContextResolver.CurrentPage;

        public DisplayOptionEnum DisplayOption
        {
            get
            {
                var renderSettings = ControllerContext.RouteData.Values[GlobalConstants.ViewBagProperties.RenderSettings] as Dictionary<string, object>;
                if (renderSettings == null)
                {
                    return DisplayOptionEnum.Unknown;
                }

                object tag;

                if (!renderSettings.TryGetValue(GlobalConstants.ViewBagProperties.Tag, out tag))
                {
                    return DisplayOptionEnum.Unknown;
                }

                return tag == null
                           ? DisplayOptionEnum.Unknown
                           : GetDisplayOptionTag(tag.ToString());
            }
        }

        public DisplayOptionEnum GetDisplayOptionTag(string tag)
        {
            DisplayOptionEnum displayOptionEnum;
            Enum.TryParse(tag, out displayOptionEnum);

            return displayOptionEnum;
        }
    }
}