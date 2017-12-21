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
using JonDJones.Website.Interfaces;
using JonDJones.Website.UnitTests.Helper;
using JonDJones.Website.Core.ViewModel.Base;
using JonDJones.Website.Core.ViewModel.Pages;

namespace JonDJones.Website.Core.UnitTests.Controllers.Base
{
    [TestFixture]
    public class When_I_Instantiate_PreviewControllerTests_ : TestBaseClass
    {
        private Mock<IWebsiteDependencies> _websiteDependenciesMock;

        private Mock<IPageTypeServices> _pageTypeServicesMock;

        private Mock<DisplayOptions> _displayOptionsMock;

        private Mock<TemplateResolver> _templateResolverMock;

        [SetUp]
        public void Setup()
        {
            _websiteDependenciesMock = new Mock<IWebsiteDependencies>(MockBehavior.Strict);
            _pageTypeServicesMock = new Mock<IPageTypeServices>(MockBehavior.Strict);
            _displayOptionsMock = new Mock<DisplayOptions>();
            _templateResolverMock = new Mock<TemplateResolver>();

            _pageTypeServicesMock
                .Setup(x => x.StartPageService.GetStartPageAdditionalProperties(It.IsAny<StartPage>()))
                .Returns(new StartPageAdditionalProperties());
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
            _pageTypeServicesMock.Setup(x => x.StartPageService.Homepage).Returns(mockStartPage.Object);

            var test = new PreviewController(
                _displayOptionsMock.Object,
                _templateResolverMock.Object,
                _websiteDependenciesMock.Object,
                _pageTypeServicesMock.Object);

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
            _templateResolverMock.Setup(x => x.Resolve(It.IsAny<HttpContextBase>(), It.IsAny<Type>(), It.IsAny<object>(), It.IsAny<TemplateTypeCategories>(), It.IsAny<string>())).Returns(mockTemplateModel.Object);
            _pageTypeServicesMock.Setup(x => x.StartPageService.Homepage).Returns(mockStartPage.Object);

            _websiteDependenciesMock.Setup(x => x.ContextResolver.AddContentAreaItem(It.IsAny<ContentArea>(), It.IsAny<ContentAreaItem>())).Returns(new ContentArea());

            var test = new PreviewController(
                displayOptions,
                _templateResolverMock.Object,
                _websiteDependenciesMock.Object,
                _pageTypeServicesMock.Object);

            var actionResult = test.Index(content.Object);
            var viewModel = ((ViewResult)actionResult).ViewData.Model;
            ((PreviewViewModel<StartPage, StartPageAdditionalProperties>)viewModel).Areas.Count().Should().BeGreaterThan(0);
        }
    }
}