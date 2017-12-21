namespace JonDJones.Website.Core.UnitTests.Controllers.Blocks
{
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Routing;

    using EPiServer.ServiceLocation;

    using FluentAssertions;

    using Moq;

    using NUnit.Framework;

    using JonDJones.Website.TestShared.AutoFixutre.AutoData.NUnit3;
    using JonDJones.Website.TestShared.Base;

    using JonDJones.Website.Core.Blocks;
    using JonDJones.Website.Core.Controllers.Blocks;
    using JonDJones.Website.Core.Dependencies.RepositoryDependencies.Interfaces;
    using JonDJones.Website.Core.ViewModel.Blocks;
    using JonDJones.Website.Interfaces;
    using JonDJones.Website.UnitTests.Helper;

    [TestFixture]
    public class When_I_Instantiate_iFrameController_ : TestBaseClass
    {
        [SetUp]
        public void Setup()
        {
            var mockServiceLocator = new Mock<IServiceLocator>();
            ServiceLocator.SetLocatorProvider(() => mockServiceLocator.Object);

            var _websiteDependencies = new Mock<IWebsiteDependencies>(MockBehavior.Strict);
            var episerverContentRepositories = new Mock<IPageTypeServices>(MockBehavior.Strict);

            mockServiceLocator.Setup(x => x.GetInstance<IWebsiteDependencies>()).Returns(_websiteDependencies.Object);
            mockServiceLocator.Setup(x => x.GetInstance<IPageTypeServices>()).Returns(episerverContentRepositories.Object);
        }

        [Test]
        public void The_Constructor_Must_Throw_An_ArgumentNullException_With_No_Parameters()
        {
            ParameterValidationHelper.ConstructorMustThrowArgumentNullException(typeof(IFrameBlockController));
        }

        [Test]
        public void The_Constructor_Should_Not_Accept_Null_Constructor_Arguments()
        {
            ParameterValidationHelper.ShouldNotAcceptNullConstructorArguments(typeof(IFrameBlockController));
        }

        [Test, AutoMoqData]
        public void TestQueryString(string queryStringParameter)
        {
            var iframeBlock = new Mock<IFrameBlock>();

            var request = new Mock<HttpRequestBase>();
            request.SetupGet(r => r.QueryString).Returns(HttpUtility.ParseQueryString($"?{queryStringParameter}={queryStringParameter}"));

            var context = new Mock<HttpContextBase>();
            context.SetupGet(x => x.Request).Returns(request.Object);

            var controller = new IFrameBlockController();
            controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);

            var actionResult = (PartialViewResult)controller.Index(iframeBlock.Object);
            var viewModel = (IFrameBlockViewModel)actionResult.Model;
            viewModel.QueryStringNameValueCollection.Keys.Should().Contain(queryStringParameter);
        }
    }
}