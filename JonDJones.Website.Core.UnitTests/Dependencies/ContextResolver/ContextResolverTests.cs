namespace JonDJones.Website.Core.UnitTests.Dependencies.ContextResolver
{
    using EPiServer;
    using EPiServer.Core;
    using EPiServer.Web.Routing;

    using FluentAssertions;

    using Moq;

    using NUnit.Framework;

    using JonDJones.Website.TestShared.Base;

    using JonDJones.Website.Core.Dependencies.ContextResolver;
    using JonDJones.Website.UnitTests.Helper;

    [TestFixture]
    public class When_I_Instantiate_ContextResolver_ : TestBaseClass
    {
        private Mock<IContentRepository> mockContentRepository;

        private Mock<IPageRouteHelper> pageRouteHelper;

        [SetUp]
        public void Setup()
        {
            mockContentRepository = new Mock<IContentRepository>();
            pageRouteHelper = new Mock<IPageRouteHelper>();
        }

        [Test]
        public void The_Constructor_Should_Not_Accept_Null_Constructor_Arguments()
        {
            ParameterValidationHelper.ShouldNotAcceptNullConstructorArguments(typeof(ContextResolver));
        }

        [Test]
        public void The_Constructor_Must_Throw_An_ArgumentNullException_With_No_Parameters()
        {
            ParameterValidationHelper.ConstructorMustThrowArgumentNullException(typeof(ContextResolver));
        }

        [Test]
        public void SiteAssetsFolder_Should_Be_Correctly_Set()
        {
            mockContentRepository
                .Setup(x => x.GetDefault<ContentFolder>(It.IsAny<ContentReference>()))
                .Returns(new ContentFolder());

            var contextResolver = new ContextResolver(mockContentRepository.Object, pageRouteHelper.Object);
            contextResolver.SiteAssetsFolder.Should().NotBeNull();
        }

        [Test]
        public void CurrentPage_Should_Be_Correctly_Set()
        {
            var pageData = new Mock<PageData>();

            pageRouteHelper.Setup(x => x.Page).Returns(pageData.Object);

            mockContentRepository
                .Setup(x => x.GetDefault<ContentFolder>(It.IsAny<ContentReference>()))
                .Returns(new ContentFolder());

            var contextResolver = new ContextResolver(mockContentRepository.Object, pageRouteHelper.Object);
            contextResolver.CurrentPage.Should().NotBeNull();
        }
    }
}
