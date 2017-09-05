namespace JonDJones.Website.Core.UnitTests.Dependencies
{
    using EPiServer;
    using EPiServer.Core;

    using FluentAssertions;

    using Moq;

    using NUnit.Framework;

    using JonDJones.Website.TestShared.AutoFixutre.AutoData.NUnit3;
    using JonDJones.Website.TestShared.Base;

    using JonDJones.Website.Core.Dependencies;
    using JonDJones.Website.Core.Dependencies.RepositoryDependencies.Interfaces;
    using JonDJones.Website.Core.Pages;
    using JonDJones.Website.Interfaces;
    using JonDJones.Website.Shared.Interfaces;
    using JonDJones.Website.UnitTests.Helper;

    [TestFixture]
    public class When_I_Instantiate_WebsiteDependencies_ : TestBaseClass
    {
        private Mock<IContentRepository> mockContentRepository;

        private Mock<SiteSettingsPage> mockSiteSettingPage;

        private Mock<IContextResolver> mockContextResolver;

        private WebsiteDependencies _websiteDependencies;

        private Mock<ISiteSettingsPageRepository> mockSiteSettingsPageRepository;

        [SetUp]
        public void SetUp()
        {
            mockContentRepository = new Mock<IContentRepository>();
            mockSiteSettingPage = new Mock<SiteSettingsPage>();
            mockContextResolver = new Mock<IContextResolver>();
            mockSiteSettingsPageRepository = new Mock<ISiteSettingsPageRepository>();
        }

        [Test]
        public void The_Constructor_Should_Not_Accept_Null_Constructor_Arguments()
        {
            ParameterValidationHelper.ShouldNotAcceptNullConstructorArguments(typeof(WebsiteDependencies));
        }

        [Test]
        public void The_Constructor_Must_Throw_An_ArgumentNullException_With_No_Parameters()
        {
            ParameterValidationHelper.ConstructorMustThrowArgumentNullException(typeof(WebsiteDependencies));
        }

        [Test, AutoMoqData]
        public void SiteSettings_Should_Be_Correctly_Set(
            ICacheManager cacheManager,
            ICookieManager cookieManager,
            IAssetHandler assetHandler,
            int contentId)
        {
            mockSiteSettingsPageRepository.Setup(x => x.SiteSettingsPage)
                .Returns(mockSiteSettingPage.Object);

            _websiteDependencies = new WebsiteDependencies(
                mockContentRepository.Object,
                mockContextResolver.Object,
                cacheManager,
                assetHandler,
                mockSiteSettingsPageRepository.Object);

            _websiteDependencies.SiteSettings.Should().NotBeNull();
            mockSiteSettingsPageRepository.Verify(x => x.SiteSettingsPage, Times.AtLeastOnce());
        }
    }
}
