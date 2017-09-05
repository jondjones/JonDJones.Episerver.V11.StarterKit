namespace TSC.Website.UnitTests.ViewModels.Blocks
{
    using System.Collections.Specialized;

    using Core.Blocks;
    using Core.Dependencies.RepositoryDependencies.Interfaces;
    using Core.Pages;
    using Core.ViewModel.Blocks;

    using EPiServer;
    using EPiServer.Core;

    using FluentAssertions;

    using Helper;

    using Interfaces;
    using Interfaces.Enums;

    using Moq;

    using NUnit.Framework;

    using Tsc.Website.TestShared.Base;
    
    [TestFixture]
    public class When_I_Instantiate_IFrameBlockViewModel_ : TestBaseClass
    {
        private IFrameBlockViewModel iframeBlockViewModel;

        private Mock<IFrameBlock> mockIFrameBlock;

        private Mock<IWebsiteDependencies> dependencies;

        private Mock<ILinkResolver> mockLinkResolver;

        private Mock<IEpiserverContentRepositories> episerverRepositoryDependencies;

        [SetUp]
        public void SetUp()
        {
            mockIFrameBlock = new Mock<IFrameBlock>();
            dependencies = new Mock<IWebsiteDependencies>();
            mockLinkResolver = new Mock<ILinkResolver>();

            episerverRepositoryDependencies = new Mock<IEpiserverContentRepositories>();
        }

        [Test]
        public void The_Constructor_Should_Not_Accept_Null_Constructor_Arguments()
        {
            ParameterValidationHelper.ShouldNotAcceptNullConstructorArguments(typeof(CarouselSlideBlockViewModel));
        }

        [Test]
        public void The_Decode_Disabled_Boolean_Works()
        {
            var mock = new Mock<StartPage>();
            mock.Setup(x => x.ContentLink).Returns(new ContentReference());

            string url = "https://www.share.com/preference-centre";
            var mockLinkResolver = new Mock<ILinkResolver>();
            mockLinkResolver
                .Setup(x => x.GetFriendlyUrl(It.IsAny<Url>()))
                .Returns(url);

            dependencies.Setup(x => x.LinkResolver).Returns(mockLinkResolver.Object);

            NameValueCollection queryString = new NameValueCollection();
            queryString.Add("crnmail", "bQ7H5PmJeJHPTKJaSUwHP3VgSvah%2FyF0fs597dz0vmzWkTJOMVqAVo%2BJS9Neknh");

            iframeBlockViewModel = new IFrameBlockViewModel(
                mockIFrameBlock.Object,
                dependencies.Object,
                DisplayOptionEnum.Full,
                episerverRepositoryDependencies.Object);

            mockIFrameBlock.Setup(x => x.DecodingDisabled).Returns(true);
            mockIFrameBlock.Setup(x => x.Hyperlink).Returns(url);
            iframeBlockViewModel.QueryStringNameValueCollection = queryString;

            iframeBlockViewModel.LinkUrl.Should().Contain(url);
        }
    }
}
