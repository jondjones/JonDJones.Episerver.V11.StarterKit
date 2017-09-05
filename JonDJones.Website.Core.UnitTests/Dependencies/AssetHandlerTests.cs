namespace JonDJones.Website.Core.UnitTests.Dependencies
{
    using System.Collections.Generic;

    using EPiServer;
    using EPiServer.Core;
    using EPiServer.DataAbstraction;
    using EPiServer.Framework.Blobs;

    using FluentAssertions;

    using Moq;

    using NUnit.Framework;

    using JonDJones.Website.TestShared.Base;

    using JonDJones.Website.Core.Dependencies;
    using JonDJones.Website.Core.EpiserverConfiguration.Media;
    using JonDJones.Website.Interfaces;
    using JonDJones.Website.UnitTests.Helper;

    [TestFixture]
    public class When_I_Instantiate_AssetHandler_ : TestBaseClass
    {
        private Mock<IContentRepository> mockContentRepository;

        private Mock<IBlobFactory> mockBlobFactory;

        private Mock<IContextResolver> mockContextResolver;

        private Mock<ILinkResolver> mockLinkResolver;

        private Mock<IContentTypeRepository> mockContentTypeRepository;

        private Mock<ContentMediaResolver> mockContentMediaResolver;

        private AssetHandler assetHandler;

        public static IEnumerable<TestCaseData> InputParameterTestData
        {
            get
            {
                yield return new TestCaseData(null, null, null, null);
                yield return new TestCaseData(new ContentReference(), null, null, null);
                yield return new TestCaseData(null, "test", null, null);
                yield return new TestCaseData(null, null, "test", null);
            }
        }

        [SetUp]
        public void Setup()
        {
            mockContentRepository = new Mock<IContentRepository>();
            mockBlobFactory = new Mock<IBlobFactory>();
            mockContextResolver = new Mock<IContextResolver>();
            mockLinkResolver = new Mock<ILinkResolver>();
            mockContentTypeRepository = new Mock<IContentTypeRepository>();
            mockContentMediaResolver = new Mock<ContentMediaResolver>();

            assetHandler = new AssetHandler(
                mockContentRepository.Object,
                mockBlobFactory.Object,
                mockContextResolver.Object,
                mockLinkResolver.Object,
                mockContentTypeRepository.Object,
                mockContentMediaResolver.Object);
        }

        [Test]
        public void The_Constructor_Should_Not_Accept_Null_Constructor_Arguments()
        {
            ParameterValidationHelper.ShouldNotAcceptNullConstructorArguments(typeof(AssetHandler));
        }

        [Test]
        public void The_Constructor_Must_Throw_An_ArgumentNullException_With_No_Parameters()
        {
            ParameterValidationHelper.ConstructorMustThrowArgumentNullException(typeof(AssetHandler));
        }

        [Test]
        [TestCaseSource("InputParameterTestData")]
        public void InsertMediaByUrl_Handles_Null_Arguments_Correctly(
            ContentReference pageToStore,
            string fileName,
            string url,
            string imageExtension)
        {
            var result = assetHandler.InsertMediaByUrl<ImageFile>(pageToStore, fileName, url, imageExtension);
            result.Should().BeNull();
        }

        [Test]
        [TestCaseSource("InputParameterTestData")]
        public void InsertMediaByPath_Handles_Null_Arguments_Correctly(
            ContentReference pageToStore,
            string fileName,
            string url,
            string imageExtension)
        {
            var result = assetHandler.InsertMediaByPath<ImageFile>(pageToStore, fileName, url, imageExtension);
            result.Should().BeNull();
        }
    }
}