namespace JonDJones.Website.Core.UnitTests.ViewModels.Base
{
    using FluentAssertions;

    using Moq;

    using NUnit.Framework;

    using JonDJones.Website.TestShared.AutoFixutre.AutoData.NUnit3;
    using JonDJones.Website.TestShared.Base;

    using JonDJones.Website.Core.Pages;
    using JonDJones.Website.Core.ViewModel.Base;
    using JonDJones.Website.Interfaces;
    using JonDJones.Website.UnitTests.Helper;

    [TestFixture]
    public class When_I_Instantiate_BaseViewModel_ : TestBaseClass
    {
        private BaseViewModel<StartPage> baseViewModel;

        private StartPage startPage;

        [SetUp]
        public void SetUp()
        {
            startPage = new StartPage();
        }

        [Test]
        public void The_Constructor_Must_Throw_An_ArgumentNullException_With_No_Parameters()
        {
            ParameterValidationHelper.ConstructorMustThrowArgumentNullException(typeof(BaseViewModel<StartPage>));
        }

        [Test, AutoMoqData]
        public void CurrentPage_Should_Be_Set_Correctly(IWebsiteDependencies dependencies)
        {
            baseViewModel = new BaseViewModel<StartPage>(startPage, dependencies);
            baseViewModel.CurrentPage.Should().NotBeNull();
        }

        [Test, AutoMoqData]
        public void Dependencies_Should_Be_Set_Correctly(IWebsiteDependencies dependencies)
        {
            baseViewModel = new BaseViewModel<StartPage>(startPage, dependencies);
            baseViewModel.WebsiteDependencies.Should().NotBeNull();
        }
    }
}
