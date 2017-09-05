namespace JonDJones.Website.Core.UnitTests.Controllers.Base
{
    using EPiServer.ServiceLocation;

    using FluentAssertions;

    using Moq;

    using NUnit.Framework;

    using JonDJones.Website.TestShared.Base;

    using JonDJones.Website.Core.Controllers.Base;
    using JonDJones.Website.Core.Dependencies.RepositoryDependencies.Interfaces;
    using JonDJones.Website.Core.Pages;
    using JonDJones.Website.Interfaces;

    [TestFixture]
    public class When_I_Instantiate_BasePageController_ : TestBaseClass
    {
        private BasePageController<StartPage> startPage;

        [SetUp]
        public void Setup()
        {
            var mockS = new Mock<IServiceLocator>();
            ServiceLocator.SetLocatorProvider(() => mockS.Object);

            var _websiteDependencies = new Mock<IWebsiteDependencies>(MockBehavior.Strict);
            var episerverContentRepositories = new Mock<IEpiserverContentRepositories>(MockBehavior.Strict);

            mockS.Setup(x => x.GetInstance<IWebsiteDependencies>()).Returns(_websiteDependencies.Object);
            mockS.Setup(x => x.GetInstance<IEpiserverContentRepositories>()).Returns(episerverContentRepositories.Object);
        }

        [Test]
        public void WebsiteDependencies_Sets_Correctly()
        {
            startPage = new BasePageController<StartPage>();
            startPage.WebsiteDependencies.Should().NotBeNull();
        }

        [Test]
        public void EpiserverRepositoryDependencies_Sets_Correctly()
        {
            startPage = new BasePageController<StartPage>();
            startPage.EpiserverRepositoryDependencies.Should().NotBeNull();
        }
    }
}