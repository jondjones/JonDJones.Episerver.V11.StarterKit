namespace JonDJones.Website.Core.UnitTests.ViewModels.Factory
{
    #region Using

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
    using JonDJones.Website.Interfaces;
    using JonDJones.Website.UnitTests.Helper;

    #endregion

    [TestFixture]
    public class When_I_Instantiate__HeaderViewModelFactory_ : TestBaseClass
    {
        private Mock<StartPage> mockStartPage;

        private Mock<SiteSettingsPage> mockSiteSettingsPage;

        private Mock<IPageTypeServices> mockEpiserverContentRepositories;

        [SetUp]
        public void SetUp()
        {
            mockStartPage = new Mock<StartPage>();
            mockSiteSettingsPage = new Mock<SiteSettingsPage>();

            mockEpiserverContentRepositories = new Mock<IPageTypeServices>();
            mockEpiserverContentRepositories.Setup(x => x.StartPageService.Homepage).Returns(mockStartPage.Object);
            mockEpiserverContentRepositories.Setup(x => x.SiteSettingsService.SiteSettingsPage)
                .Returns(mockSiteSettingsPage.Object);
        }

        [Test, AutoMoqData]
        public void A_Valid_Header_ViewModel_Is_Created(
            IMenuService menuPageRepository)
        {
            var headerViewModelFactory = new HeaderViewModelFactory(menuPageRepository);

            var headerViewModel = headerViewModelFactory.CreateHeaderProperties(
                mockStartPage.Object,
                mockSiteSettingsPage.Object);
            headerViewModel.Should().NotBeNull();
        }

        [Test, AutoMoqData]
        public void Header_SiteSettingsData_Should_Be_Set_Correctly(
            IMenuService menuPageRepository)
        {
            var headerViewModelFactory = new HeaderViewModelFactory(menuPageRepository);

            var headerViewModel = headerViewModelFactory.CreateHeaderProperties(
                mockStartPage.Object,
                mockSiteSettingsPage.Object);
            headerViewModel.SiteSettingsProperties.Should().NotBeNull();
        }

        [Test, AutoMoqData]
        public void Header_Data_Should_Be_Set_Correctly(
            IMenuService menuPageRepository)
        {
            var headerViewModelFactory = new HeaderViewModelFactory(menuPageRepository);

            var headerViewModel = headerViewModelFactory.CreateHeaderProperties(
                mockStartPage.Object,
                mockSiteSettingsPage.Object);
            headerViewModel.Current.Should().NotBeNull();
        }

        [Test, AutoMoqData]
        public void Header_MenuData_Should_Be_Set_Correctly(
                    IMenuService menuPageRepository)
        {
            var headerViewModelFactory = new HeaderViewModelFactory(menuPageRepository);

            var headerViewModel = headerViewModelFactory.CreateHeaderProperties(
                mockStartPage.Object,
                mockSiteSettingsPage.Object);
            headerViewModel.MenuProperties.Should().NotBeNull();
        }

        [Test, AutoMoqData]
        public void Header_Menu_PrimaryNavigationData_Should_Be_Set_Correctly(
                    IMenuService menuPageRepository)
        {
            var headerViewModelFactory = new HeaderViewModelFactory(menuPageRepository);
            var navigationItems = new Mock<List<INavigationItem>>();
            var menuProperties = new Mock<IHeaderMenuProperties>();
            menuProperties.Setup(x => x.MenuItems).Returns(navigationItems.Object);

            var headerViewModel = headerViewModelFactory.CreateHeaderProperties(
                mockStartPage.Object,
                mockSiteSettingsPage.Object);
            headerViewModel.MenuProperties = menuProperties.Object;
            headerViewModel.MenuProperties.HasPrimaryNavigationItems.Should().Be(false);
        }

        [Test]
        public void The_Constructor_Must_Throw_An_ArgumentNullException_With_No_Parameters()
        {
            ParameterValidationHelper.ConstructorMustThrowArgumentNullException(typeof(HeaderViewModelFactory));
        }

        [Test]
        public void The_Constructor_Should_Not_Accept_Null_Constructor_Arguments()
        {
            ParameterValidationHelper.ShouldNotAcceptNullConstructorArguments(typeof(HeaderViewModelFactory));
        }
    }
}