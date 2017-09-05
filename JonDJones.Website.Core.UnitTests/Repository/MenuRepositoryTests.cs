namespace JonDJones.Website.Core.UnitTests.Repository
{
    using System.Collections.Generic;
    using System.Linq;

    using EPiServer;
    using EPiServer.Core;

    using FluentAssertions;

    using Moq;

    using NUnit.Framework;

    using JonDJones.Website.TestShared.AutoFixutre.AutoData.NUnit3;
    using JonDJones.Website.TestShared.Base;

    using JonDJones.Website.Core.Pages;
    using JonDJones.Website.Core.Pages.MetaPages.Menu;
    using JonDJones.Website.Core.Repository;
    using JonDJones.Website.Interfaces;
    using JonDJones.Website.UnitTests.Helper;

    [TestFixture]
    public class When_I_Instantiate_MenuRepository_ : TestBaseClass
    {
        private Mock<MenuPage> menuPageMock;

        private Mock<IWebsiteDependencies> dependencies;

        [SetUp]
        public void SetUp()
        {
            menuPageMock = new Mock<MenuPage>();
            dependencies = new Mock<IWebsiteDependencies>();
        }

        [Test]
        public void The_Constructor_Should_Not_Accept_Null_Constructor_Arguments()
        {
            ParameterValidationHelper.ShouldNotAcceptNullConstructorArguments(typeof(MenuPageRepository));
        }

        [Test]
        public void The_Constructor_Must_Throw_An_ArgumentNullException_With_No_Parameters()
        {
            ParameterValidationHelper.ConstructorMustThrowArgumentNullException(typeof(MenuPageRepository));
        }

        [Test, AutoMoqData]
        public void GetMegaNavigationItems_Should_Be_Empty_When_Not_Set(IWebsiteDependencies dependencies)
        {
            var menuRepository = new MenuPageRepository(dependencies);
            var result = menuRepository.GetMenuItems(null);

            result.Should().BeEmpty();
        }

        [Test, AutoMoqData]
        public void GetMegaNavigationItems_Should_Be_Empty_When_PrimaryNavigationContainerPage_Not_Set(IWebsiteDependencies dependencies)
        {
            var mockStartPage = new Mock<StartPage>();

            var menuRepository = new MenuPageRepository(dependencies);
            var result = menuRepository.GetMenuItems(mockStartPage.Object);

            result.Should().BeEmpty();
        }

        [Test, AutoMoqData]
        public void GetMegaNavigationItems_Should_Be_Empty_When_No_Menu_Items_Exist(IWebsiteDependencies dependencies, int contentId)
        {
            var mockStartPage = GetStartPage(contentId);
            var menuRepository = new MenuPageRepository(dependencies);
            var result = menuRepository.GetMenuItems(mockStartPage.Object);

            result.Should().BeEmpty();
        }

        [Test, AutoMoqData]
        public void GetMegaNavigationItems_Should_Return_Items_When_They_Exist(int contentId, string testUrl)
        {
            var mockStartPage = GetStartPage(contentId);
            SetupMockData(testUrl);

            var menuRepository = new MenuPageRepository(dependencies.Object);
            var result = menuRepository.GetMenuItems(mockStartPage.Object);

            result.Count.Should().Be(1);
        }

        [Test, AutoMoqData]
        public void GetMegaNavigationItems_Should_Return_SubMenItems_When_They_Exist(int contentId, string testUrl)
        {
            var mockStartPage = GetStartPage(contentId);
            SetupMockData(testUrl);
            SetupSubMenuItemsMockData(testUrl);

            var menuRepository = new MenuPageRepository(dependencies.Object);
            var result = menuRepository.GetMenuItems(mockStartPage.Object);

            result.First().SubMenuItems.Count().Should().Be(1);
        }

        [Test, AutoMoqData]
        public void GetMegaNavigationItems_Should_Return_SubMenItems_With_The_Correct_Data(int contentId, string testUrl)
        {
            var mockStartPage = GetStartPage(contentId);
            SetupMockData(testUrl);
            SetupSubMenuItemsMockData(testUrl);

            var menuRepository = new MenuPageRepository(dependencies.Object);
            var results = menuRepository.GetMenuItems(mockStartPage.Object);

            var result = results.First();
            result.Link.Should().Be(testUrl);
            result.Name.Should().Be(testUrl);
        }

        [Test, AutoMoqData]
        public void GetNavigationItems_Should_Return_Null_When_Not_Set(IWebsiteDependencies dependencies)
        {
            var menuRepository = new MenuPageRepository(dependencies);
            var result = menuRepository.GetNavigationItems(null);

            result.Should().BeEmpty();
        }

        [Test, AutoMoqData]
        public void GetNavigationItems_Should_Return_The_Correct_Number_Of_Results(PageReference pageReference, string testUrl)
        {
            SetupMockData(testUrl);

            var menuRepository = new MenuPageRepository(dependencies.Object);
            var result = menuRepository.GetNavigationItems(pageReference);

            result.Count.Should().Be(1);
        }

        [Test, AutoMoqData]
        public void GetNavigationItems_Should_Return_The_Correct_Data(PageReference pageReference, string testUrl)
        {
            SetupMockData(testUrl);

            var menuRepository = new MenuPageRepository(dependencies.Object);
            var results = menuRepository.GetNavigationItems(pageReference);

            var result = results.FirstOrDefault();
            result.Link.Should().Be(testUrl);
            result.SubMenuTitle.Should().Be(testUrl);
            result.Name.Should().Be(testUrl);
        }

        [Test, AutoMoqData]
        public void GetNavigationItems_Should_Return_The_CorrectImageUrl(PageReference pageReference, string testUrl)
        {
            SetupMockData(testUrl);
            menuPageMock.Setup(x => x.MenuImageUrl).Returns(new ContentReference());

            var menuRepository = new MenuPageRepository(dependencies.Object);
            var results = menuRepository.GetNavigationItems(pageReference);

            var result = results.FirstOrDefault();
            result.ImageUrl.Should().Be(testUrl);
        }

        private Mock<StartPage> GetStartPage(int contentId)
        {
            var mockStartPage = new Mock<StartPage>();
            mockStartPage.Setup(x => x.PrimaryNavigationContainerPage).Returns(new PageReference(contentId));

            return mockStartPage;
        }

        private void SetupMockData(string testUrl)
        {
            var mockContentReference = new Mock<ContentReference>();
            mockContentReference.Setup(x => x.ToString()).Returns(testUrl);

            menuPageMock.Setup(x => x.MainMenuTitle).Returns(testUrl);
            menuPageMock.Setup(x => x.SubMenuTitle).Returns(testUrl);
            menuPageMock.Setup(x => x.MenuImageUrl).Returns(mockContentReference.Object);

            var menuPages = new List<MenuPage> { menuPageMock.Object };
            dependencies.Setup(x => x.ContentRepository.GetChildren<MenuPage>(It.IsAny<ContentReference>())).Returns(menuPages);
            dependencies.Setup(x => x.LinkResolver.GetFriendlyUrl(It.IsAny<Url>())).Returns(testUrl);
            dependencies.Setup(x => x.LinkResolver.GetFriendlyUrl(It.IsAny<ContentReference>())).Returns(testUrl);
        }

        private void SetupSubMenuItemsMockData(string testUrl)
        {
            var mockContentReference = new Mock<ContentReference>();
            mockContentReference.Setup(x => x.ToString()).Returns(testUrl);

            var mockMenuItemPage = new Mock<MenuItemPage>();
            mockMenuItemPage.Setup(x => x.LinkUrl).Returns(testUrl);
            mockMenuItemPage.Setup(x => x.Name).Returns(testUrl);

            var menuItemPages = new List<MenuItemPage> { mockMenuItemPage.Object };
            dependencies.Setup(x => x.ContentRepository.GetChildren<MenuItemPage>(It.IsAny<ContentReference>())).Returns(menuItemPages);
        }
    }
}
