namespace JonDJones.Website.Core.UnitTests.Repository
{
    using EPiServer;
    using EPiServer.Core;
    using EPiServer.DataAccess;
    using EPiServer.Security;

    using FluentAssertions;

    using Moq;

    using NUnit.Framework;

    using JonDJones.Website.TestShared.AutoFixutre.AutoData.NUnit3;
    using JonDJones.Website.TestShared.Base;

    using JonDJones.Website.Core.Pages;
    using JonDJones.Website.Core.Repository;
    using JonDJones.Website.Interfaces;
    using JonDJones.Website.UnitTests.Helper;

    [TestFixture]
    public class When_I_Instantiate_StartPageRepository_ : TestBaseClass
    {
        private Mock<StartPage> mockStartPage;

        private Mock<IContentRepository> mockContentRepository;

        private Mock<IContextResolver> mockContextResolver;

        [SetUp]
        public void SetUp()
        {
            mockStartPage = new Mock<StartPage>();
            mockContentRepository = new Mock<IContentRepository>();
            mockContextResolver = new Mock<IContextResolver>();
        }

        [Test]
        public void The_Constructor_Should_Not_Accept_Null_Constructor_Arguments()
        {
            ParameterValidationHelper.ShouldNotAcceptNullConstructorArguments(typeof(StartPageRepository));
        }

        [Test]
        public void The_Constructor_Must_Throw_An_ArgumentNullException_With_No_Parameters()
        {
            ParameterValidationHelper.ConstructorMustThrowArgumentNullException(typeof(StartPageRepository));
        }

        [Test]
        public void ContentRepository_Save_Shouldnt_Be_Called_If_Modified_Is_False()
        {
            var startPageRepository = new StartPageRepository(mockContentRepository.Object, mockContextResolver.Object);
            startPageRepository.Save(mockStartPage.Object);

            mockContentRepository.Verify(x => x.Save(It.IsAny<IContent>(), It.IsAny<SaveAction>(), It.IsAny<AccessLevel>()), Times.Never());
        }

        [Test]
        public void ContentRepository_Save_Should_Be_Called_If_Content_Modified()
        {
            mockContextResolver.Setup(x => x.IsPageModified(It.IsAny<PageData>())).Returns(true);
            var startPageRepository = new StartPageRepository(mockContentRepository.Object, mockContextResolver.Object);
            startPageRepository.Save(mockStartPage.Object);

            mockContentRepository.Verify(x => x.Save(It.IsAny<IContent>(), It.IsAny<SaveAction>(), It.IsAny<AccessLevel>()));
        }

        [Test, AutoMoqData]
        public void Startpage_Should_Set_Correctly(int contentId)
        {
            var pageReference = new PageReference(contentId);
            var mockStartPage = new Mock<StartPage>();
            mockContextResolver.Setup(x => x.StartPage).Returns(pageReference);
            var startPageRepository = new StartPageRepository(mockContentRepository.Object, mockContextResolver.Object);

            mockContentRepository
                .Setup(x => x.Get<StartPage>(pageReference))
                .Returns(mockStartPage.Object);

            startPageRepository.StartPage.Should().NotBeNull();
        }

        [Test]
        public void Null_Homepage_Returns_Null()
        {
            var startPageRepository = new StartPageRepository(mockContentRepository.Object, mockContextResolver.Object);
            mockContentRepository
                .Setup(x => x.Get<StartPage>(It.IsAny<ContentReference>()))
                .Returns((StartPage)null);

            startPageRepository.StartPage.Should().BeNull();
        }
    }
}
