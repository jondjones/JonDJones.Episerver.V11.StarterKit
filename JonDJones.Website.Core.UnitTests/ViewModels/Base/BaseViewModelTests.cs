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
    using JonDJones.Website.Core.ViewModel.Pages;

    [TestFixture]
    public class When_I_Instantiate_PageViewModel_ : TestBaseClass
    {
        private PageViewModel<StartPage, StartPageAdditionalProperties> baseViewModel;

        private StartPage startPage;

        [SetUp]
        public void SetUp()
        {
            startPage = new StartPage();
        }

        [Test, AutoMoqData]
        public void CurrentPage_Should_Be_Set_Correctly(IWebsiteDependencies dependencies)
        {
            baseViewModel = new PageViewModel<StartPage, StartPageAdditionalProperties>(startPage, null);
            baseViewModel.CurrentPage.Should().NotBeNull();
        }

        [Test, AutoMoqData]
        public void AdditionalProperties_Should_Be_Set_Correctly(IWebsiteDependencies dependencies)
        {
            var test = new StartPageAdditionalProperties();
            baseViewModel = new PageViewModel<StartPage, StartPageAdditionalProperties>(startPage, test);
            baseViewModel.AdditionalProperties.Should().NotBeNull();
        }
    }
}
