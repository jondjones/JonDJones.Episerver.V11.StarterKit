namespace JonDJones.Website.Core.StartUp
{
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using EPiServer.Framework;
    using EPiServer.Framework.Initialization;
    using EPiServer.ServiceLocation;
    using EPiServer.Web;
    using JonDJones.Website.Core.Dependencies;
    using JonDJones.Website.Core.Dependencies.RepositoryDependencies.Interfaces;
    using JonDJones.Website.Interfaces.Enums;

    [InitializableModule]
    [ModuleDependency(typeof(EPiServer.Web.InitializationModule))]
    public class DataInitialization : IInitializableModule
    {
        internal Injected<WebsiteDependencies> WebsiteDependencies { get; set; }

        internal Injected<IEpiserverContentRepositories> EpiserverContentRepositories { get; set; }

        internal Injected<DisplayOptions> Options { get; set; }

        public void Initialize(InitializationEngine context)
        {
            context.InitComplete += InitCompleteHandler;

            GlobalFilters.Filters.Add(ServiceLocator.Current.GetInstance<BasePageDataSetup>());
            SetDisplayOptions();
        }

        public void Uninitialize(InitializationEngine context)
        {
        }

        public void Preload(string[] parameters)
        {
        }

        private void InitCompleteHandler(object sender, EventArgs e)
        {
        }

        private void SetDisplayOptions()
        {
            var options = Options.Service;
            foreach (var optionId in options.Select(x => x.Id).ToArray())
            {
                options.Remove(optionId);
            }

            options
                .Add("full", DisplayOptionTags.FullWidth, DisplayOptionEnum.Full.ToString(), string.Empty, "epi-icon__layout--full")
                .Add("wide", DisplayOptionTags.TwoThirdsWidth, DisplayOptionEnum.TwoThirds.ToString(), string.Empty, "epi-icon__layout--two-thirds")
                .Add("half", DisplayOptionTags.HalfWidth, DisplayOptionEnum.Half.ToString(), string.Empty, "epi-icon__layout--half")
                .Add("narrow", DisplayOptionTags.OneThirdWidth, DisplayOptionEnum.OneThird.ToString(), string.Empty, "epi-icon__layout--one-third")
                .Add("quarter", DisplayOptionTags.OneFourthWidth, DisplayOptionEnum.OneFourth.ToString(), string.Empty, "epi-icon__layout--one-fourth");
        }
    }
}