namespace JonDJones.Website.Core.UnitTests.Controllers.Base
{
    using System.Collections.Generic;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Routing;

    using EPiServer.ServiceLocation;

    using FluentAssertions;

    using Moq;

    using NUnit.Framework;

    using JonDJones.Website.TestShared.Base;

    using JonDJones.Website.Core.Blocks;
    using JonDJones.Website.Core.Controllers.Base;
    using JonDJones.Website.Core.Dependencies.RepositoryDependencies.Interfaces;
    using JonDJones.Website.Interfaces;
    using JonDJones.Website.Interfaces.Enums;
    using JonDJones.Website.Shared.Resources;

    [TestFixture]
    public class When_I_Instantiate_BaseBlockController_ : TestBaseClass
    {
        private BaseBlockController<RichTextBlock> richTextBlockController;

        private ControllerContext controllerContext;

        private Mock<HttpRequestBase> request;

        private Mock<HttpContextBase> mockHttpContext;

        private Mock<ControllerBase> controllerBase;

        [SetUp]
        public void Setup()
        {
            var mockS = new Mock<IServiceLocator>();
            ServiceLocator.SetLocatorProvider(() => mockS.Object);

            var _websiteDependencies = new Mock<IWebsiteDependencies>(MockBehavior.Strict);
            var episerverContentRepositories = new Mock<IPageTypeServices>(MockBehavior.Strict);

            mockS.Setup(x => x.GetInstance<IWebsiteDependencies>()).Returns(_websiteDependencies.Object);
            mockS.Setup(x => x.GetInstance<IPageTypeServices>()).Returns(episerverContentRepositories.Object);

            request = new Mock<HttpRequestBase>();
            request.Setup(r => r.HttpMethod).Returns("GET");

            mockHttpContext = new Mock<HttpContextBase>();
            mockHttpContext.Setup(c => c.Request).Returns(request.Object);

            controllerBase = new Mock<ControllerBase>();
        }

        [Test]
        public void WebsiteDependencies_Sets_Correctly()
        {
            richTextBlockController = new BaseBlockController<RichTextBlock>();
            richTextBlockController.WebsiteDependencies.Should().NotBeNull();
        }

        [Test]
        public void EpiserverContentRepositories_Sets_Correctly()
        {
            richTextBlockController = new BaseBlockController<RichTextBlock>();
            richTextBlockController.EpiserverContentRepositories.Should().NotBeNull();
        }
        

        [Test]
        [TestCase(DisplayOptionEnum.Full)]
        [TestCase(DisplayOptionEnum.Half)]
        [TestCase(DisplayOptionEnum.OneFourth)]
        [TestCase(DisplayOptionEnum.TwoThirds)]
        [TestCase(DisplayOptionEnum.OneThird)]
        public void GetDisplayOptionTag_Returns_TheCorrect_Enum(DisplayOptionEnum enumValue)
        {
            richTextBlockController = new BaseBlockController<RichTextBlock>();

            var classToMatch = enumValue.ToString();

            var result =
                richTextBlockController.GetDisplayOptionTag(classToMatch);

            result.Should().Be(enumValue);
        }

        [Test]
        public void DisplayOptionEnum_Should_Be_Unknown_When_Not_set()
        {
            controllerContext = new ControllerContext(mockHttpContext.Object, new RouteData(), controllerBase.Object);

            richTextBlockController = new BaseBlockController<RichTextBlock>();
            richTextBlockController.ControllerContext = controllerContext;

            richTextBlockController.DisplayOption.Should().Be(DisplayOptionEnum.Unknown);
        }

        [Test]
        public void DisplayOptionEnum_With_Empty_Tag_Routedata_Should_Be_Unknown()
        {
            var routeData = new RouteData();
            routeData.Values.Add(GlobalConstants.ViewBagProperties.RenderSettings, new Dictionary<string, object>());

            controllerContext = new ControllerContext(mockHttpContext.Object, routeData, controllerBase.Object);

            richTextBlockController = new BaseBlockController<RichTextBlock>();
            richTextBlockController.ControllerContext = controllerContext;

            richTextBlockController.DisplayOption.Should().Be(DisplayOptionEnum.Unknown);
        }

        [Test]
        [TestCase(DisplayOptionEnum.Full)]
        [TestCase(DisplayOptionEnum.Half)]
        [TestCase(DisplayOptionEnum.OneFourth)]
        [TestCase(DisplayOptionEnum.TwoThirds)]
        [TestCase(DisplayOptionEnum.OneThird)]
        public void DisplayOptionEnum_With_Correct_Tag_Routedata_Should_Set_Correctly(DisplayOptionEnum enumValue)
        {
            var dictionary = new Dictionary<string, object>();
            dictionary.Add(GlobalConstants.ViewBagProperties.Tag, enumValue.ToString());

            var routeData = new RouteData();
            routeData.Values.Add(GlobalConstants.ViewBagProperties.RenderSettings, dictionary);

            controllerContext = new ControllerContext(mockHttpContext.Object, routeData, controllerBase.Object);

            richTextBlockController = new BaseBlockController<RichTextBlock>();
            richTextBlockController.ControllerContext = controllerContext;

            richTextBlockController.DisplayOption.Should().Be(enumValue);
        }
    }
}