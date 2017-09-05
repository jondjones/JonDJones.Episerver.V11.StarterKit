namespace JonDJones.Website.Core.UnitTests.Controllers.Base
{
    using System;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using EPiServer.Core;
    using EPiServer.DataAbstraction;
    using EPiServer.Framework.Web;
    using EPiServer.Web;

    using FluentAssertions;

    using Moq;

    using NUnit.Framework;

    using JonDJones.Website.TestShared.AutoFixutre.AutoData.NUnit3;
    using JonDJones.Website.TestShared.Base;

    using JonDJones.Website.Core.Controllers.Base;
    using JonDJones.Website.Core.Dependencies.RepositoryDependencies.Interfaces;
    using JonDJones.Website.Core.Pages;
    using JonDJones.Website.Core.ViewModel.Base;
    using JonDJones.Website.Interfaces;
    using JonDJones.Website.UnitTests.Helper;

    [TestFixture]
    public class When_I_Instantiate_PreviewControllerTests_ : TestBaseClass
    {
        private Mock<IWebsiteDependencies> mockWebsiteDependencies;

        private Mock<IEpiserverContentRepositories> mockEpiserverContentRepositories;

        private Mock<DisplayOptions> mockDisplayOptions;

        private Mock<TemplateResolver> templateResolver;

        [SetUp]
        public void Setup()
        {
            mockWebsiteDependencies = new Mock<IWebsiteDependencies>(MockBehavior.Strict);
            mockEpiserverContentRepositories = new Mock<IEpiserverContentRepositories>(MockBehavior.Strict);
            mockDisplayOptions = new Mock<DisplayOptions>();
            templateResolver = new Mock<TemplateResolver>();
        }

        [Test]
        public void The_Constructor_Should_Not_Accept_Null_Constructor_Arguments()
        {
            ParameterValidationHelper.ShouldNotAcceptNullConstructorArguments(typeof(PreviewController));
        }

        [Test]
        public void The_Constructor_Must_Throw_An_ArgumentNullException_With_No_Parameters()
        {
            ParameterValidationHelper.ConstructorMustThrowArgumentNullException(typeof(PreviewController));
        }

        [Test, AutoMoqData]
        public void Action_Returns_Correct_Result(IContent content)
        {
            var mockStartPage = new Mock<StartPage>();
            mockEpiserverContentRepositories.Setup(x => x.StartPageRepository.StartPage).Returns(mockStartPage.Object);

            var test = new PreviewController(
                mockDisplayOptions.Object,
                templateResolver.Object,
                mockWebsiteDependencies.Object,
                mockEpiserverContentRepositories.Object);

            test.Index(content).Should().NotBeNull();
        }

        [Test]
        public void Action_Returns_Correct_Result()
        {
            var mockStartPage = new Mock<StartPage>();

            var content = new Mock<IContent>();
            var displayOptions = new DisplayOptions
            {
                new DisplayOption
                    {
                        Id = "Full",
                        Name = "Full",
                        Tag = "Full"
                    }
            };

            var mockTemplateModel = new Mock<TemplateModel>();
            templateResolver.Setup(x => x.Resolve(It.IsAny<HttpContextBase>(), It.IsAny<Type>(), It.IsAny<object>(), It.IsAny<TemplateTypeCategories>(), It.IsAny<string>())).Returns(mockTemplateModel.Object);
            mockEpiserverContentRepositories.Setup(x => x.StartPageRepository.StartPage).Returns(mockStartPage.Object);

            mockWebsiteDependencies.Setup(x => x.ContextResolver.AddContentAreaItem(It.IsAny<ContentArea>(), It.IsAny<ContentAreaItem>())).Returns(new ContentArea());

            var test = new PreviewController(
                displayOptions,
                templateResolver.Object,
                mockWebsiteDependencies.Object,
                mockEpiserverContentRepositories.Object);

            var actionResult = test.Index(content.Object);
            var viewModel = ((ViewResult)actionResult).ViewData.Model;
            ((PreviewViewModel)viewModel).Areas.Count().Should().BeGreaterThan(0);
        }
    }
}