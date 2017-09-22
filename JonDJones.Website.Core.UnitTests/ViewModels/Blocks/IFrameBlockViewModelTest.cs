using System.Collections.Specialized;
using EPiServer;
using EPiServer.Core;
using FluentAssertions;
using JonDJones.Website.Core.Blocks;
using JonDJones.Website.Core.Dependencies.RepositoryDependencies.Interfaces;
using JonDJones.Website.Core.Pages;
using JonDJones.Website.Core.ViewModel.Blocks;
using JonDJones.Website.Interfaces;
using JonDJones.Website.Interfaces.Enums;
using JonDJones.Website.TestShared.AutoFixutre.AutoData.NUnit3;
using JonDJones.Website.TestShared.Base;
using JonDJones.Website.UnitTests.Helper;
using Moq;
using NUnit.Framework;

namespace JonDJones.Website.Core.UnitTests.ViewModels.Blocks
{
    [TestFixture]
    public class When_I_Instantiate_IFrameBlockViewModel_ : TestBaseClass
    {
        private IFrameBlockViewModel iframeBlockViewModel;

        private Mock<IFrameBlock> mockIFrameBlock;

        private Mock<IWebsiteDependencies> dependencies;

        [SetUp]
        public void SetUp()
        {
            mockIFrameBlock = new Mock<IFrameBlock>();
            dependencies = new Mock<IWebsiteDependencies>();
        }

        [Test]
        public void The_Constructor_Should_Not_Accept_Null_Constructor_Arguments()
        {
            ParameterValidationHelper.ShouldNotAcceptNullConstructorArguments(typeof(IFrameBlock));
        }

        [Test, AutoMoqData]
        public void The_Decode_Disabled_Boolean_Works(string url)
        {
            var mock = new Mock<StartPage>();
            mock.Setup(x => x.ContentLink).Returns(new ContentReference());

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
                DisplayOptionEnum.Full);
            
            mockIFrameBlock.Setup(x => x.Hyperlink).Returns(url);
            iframeBlockViewModel.QueryStringNameValueCollection = queryString;

            iframeBlockViewModel.LinkUrl.Should().Contain(url);
        }
    }
}
