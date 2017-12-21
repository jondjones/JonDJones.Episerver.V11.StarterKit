namespace JonDJones.Website.Core.UnitTests.ViewModels.Factory
{
    using System.Collections.Generic;

    using EPiServer.Core;

    using FluentAssertions;

    using Moq;

    using NUnit.Framework;

    using JonDJones.Website.TestShared.AutoFixutre.AutoData.NUnit3;
    using JonDJones.Website.TestShared.Base;

    using JonDJones.Website.Core.Dependencies.RepositoryDependencies.Interfaces;
    using JonDJones.Website.Core.Pages;
    using JonDJones.Website.Core.ViewModel.Factory;
    using JonDJones.Website.Core.ViewModel.Factory.Interfaces;
    using JonDJones.Website.Interfaces;
    using JonDJones.Website.UnitTests.Helper;

    [TestFixture]
    public class When_I_Instantiate_LayoutViewModelFactory_ : TestBaseClass
    {
        private LayoutViewModelFactory layoutViewModelFactory;

        private PageData pageData;

        private Mock<StartPage> mockStartPage;

        private Mock<SiteSettingsPage> mockSiteSettingsPage;

        private Mock<IPageTypeServices> mockEpiserverContentRepositories;

        [SetUp]
        public void SetUp()
        {
            pageData = new PageData();

            mockStartPage = new Mock<StartPage>();
            mockSiteSettingsPage = new Mock<SiteSettingsPage>();

            mockEpiserverContentRepositories = new Mock<IPageTypeServices>();
            mockEpiserverContentRepositories.Setup(x => x.StartPageService.Homepage).Returns(mockStartPage.Object);
            mockEpiserverContentRepositories.Setup(x => x.SiteSettingsService.SiteSettingsPage).Returns(mockSiteSettingsPage.Object);
        }

        [Test]
        public void The_Constructor_Should_Not_Accept_Null_Constructor_Arguments()
        {
            ParameterValidationHelper.ShouldNotAcceptNullConstructorArguments(typeof(LayoutViewModelFactory));
        }

        [Test]
        public void The_Constructor_Must_Throw_An_ArgumentNullException_With_No_Parameters()
        {
            ParameterValidationHelper.ConstructorMustThrowArgumentNullException(typeof(LayoutViewModelFactory));
        }

        [Test, AutoMoqData]
        public void Sitename_Populates_Correctly(
            string pageName,
            IWebsiteDependencies dependencies,
            IHeaderViewModelFactory headerViewModelFactory)
        {
            mockStartPage.Setup(x => x.PageName).Returns(pageName);
            mockEpiserverContentRepositories.Setup(x => x.StartPageService.Homepage).Returns(mockStartPage.Object);

            mockEpiserverContentRepositories.Setup(x => x.StartPageService.Homepage).Returns(mockStartPage.Object);
            layoutViewModelFactory = new LayoutViewModelFactory(dependencies, mockEpiserverContentRepositories.Object, headerViewModelFactory);

            var viewModel = layoutViewModelFactory.CreateLayoutViewModel(pageData);
            viewModel.SiteName.Should().Be(pageName);
        }


        [Test, AutoMoqData]
        public void A_Valid_Header_Data_Is_Created(IWebsiteDependencies _websiteDependencies, IPageTypeServices episerverContentRepositories, IHeaderViewModelFactory headerViewModelFactory)
        {
            layoutViewModelFactory = new LayoutViewModelFactory(_websiteDependencies, episerverContentRepositories, headerViewModelFactory);

            var viewModel = layoutViewModelFactory.CreateLayoutViewModel(pageData);
            viewModel.HeaderProperties.Should().NotBeNull();
        }

        [Test, AutoMoqData]
        public void Valid_Footer_ViewModel_Is_Created(IWebsiteDependencies _websiteDependencies, IPageTypeServices episerverContentRepositories, IHeaderViewModelFactory headerViewModelFactory)
        {
            layoutViewModelFactory = new LayoutViewModelFactory(_websiteDependencies, episerverContentRepositories, headerViewModelFactory);

            var viewModel = layoutViewModelFactory.CreateLayoutViewModel(pageData);
            viewModel.FooterProperties.Should().NotBeNull();
        }

        [Test, AutoMoqData]
        public void Valid_Footer_Data_Is_Created(IWebsiteDependencies _websiteDependencies, IPageTypeServices episerverContentRepositories, IHeaderViewModelFactory headerViewModelFactory)
        {
            layoutViewModelFactory = new LayoutViewModelFactory(_websiteDependencies, episerverContentRepositories, headerViewModelFactory);

            var viewModel = layoutViewModelFactory.CreateLayoutViewModel(pageData);
            viewModel.FooterProperties.Current.Should().NotBeNull();
        }

        [Test, AutoMoqData]
        public void Valid_MetaDataProperties_Is_Created(IWebsiteDependencies _websiteDependencies, IPageTypeServices episerverContentRepositories, IHeaderViewModelFactory headerViewModelFactory)
        {
            layoutViewModelFactory = new LayoutViewModelFactory(_websiteDependencies, episerverContentRepositories, headerViewModelFactory);

            var viewModel = layoutViewModelFactory.CreateLayoutViewModel(pageData);
            viewModel.MetaDataProperties.Should().NotBeNull();
        }

        [Test, AutoMoqData]
        public void Valid_MetaDataProperties_PageDataProperties_Is_Created(IWebsiteDependencies _websiteDependencies, IPageTypeServices episerverContentRepositories, IHeaderViewModelFactory headerViewModelFactory)
        {
            layoutViewModelFactory = new LayoutViewModelFactory(_websiteDependencies, episerverContentRepositories, headerViewModelFactory);

            var viewModel = layoutViewModelFactory.CreateLayoutViewModel(pageData);
            viewModel.MetaDataProperties.Current.Should().NotBeNull();
        }

        [Test, AutoMoqData]
        public void MetaDataProperties_PageDataProperties_SeoTitle_Data_Should_Be_Set_Correctly(
            IWebsiteDependencies _websiteDependencies,
            IPageTypeServices episerverContentRepositories,
            IHeaderViewModelFactory headerViewModelFactory,
            string seoTitle)
        {
            var startPage = new Mock<StartPage>();
            startPage.Setup(x => x.SeoTitle).Returns(seoTitle);
            layoutViewModelFactory = new LayoutViewModelFactory(_websiteDependencies, episerverContentRepositories, headerViewModelFactory);
            var viewModel = layoutViewModelFactory.CreateLayoutViewModel(startPage.Object);
            viewModel.MetaDataProperties.Current.SeoTitle.Should().Be(seoTitle);
        }

        [Test, AutoMoqData]
        public void FooterDataProperties_PageDataProperties_SeoTitleData_Should_Be_Set_Correctly(IWebsiteDependencies _websiteDependencies, IPageTypeServices episerverContentRepositories, IHeaderViewModelFactory headerViewModelFactory)
        {
            layoutViewModelFactory = new LayoutViewModelFactory(_websiteDependencies, episerverContentRepositories, headerViewModelFactory);

            var viewModel = layoutViewModelFactory.CreateLayoutViewModel(pageData);
            viewModel.FooterProperties.Current.Should().NotBeNull();
        }
    }
}
