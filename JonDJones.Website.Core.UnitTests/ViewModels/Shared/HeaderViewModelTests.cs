namespace JonDJones.Website.Core.UnitTests.ViewModels.Shared
{
    using FluentAssertions;

    using Moq;

    using NUnit.Framework;

    using JonDJones.Website.TestShared.AutoFixutre.AutoData.NUnit3;
    using JonDJones.Website.TestShared.Base;

    using JonDJones.Website.Core.Entities;
    using JonDJones.Website.Core.Pages;
    using JonDJones.Website.Core.ViewModel.Shared;
    using JonDJones.Website.Interfaces;
    using JonDJones.Website.UnitTests.Helper;

    [TestFixture]
    public class When_I_Instantiate_HeaderViewModelTests_ : TestBaseClass
    {
        [Test]
        public void The_Constructor_Should_Not_Accept_Null_Constructor_Arguments()
        {
            ParameterValidationHelper.ShouldNotAcceptNullConstructorArguments(typeof(HeaderViewModel));
        }

        [Test]
        public void The_Constructor_Must_Throw_An_ArgumentNullException_With_No_Parameters()
        {
            ParameterValidationHelper.ConstructorMustThrowArgumentNullException(typeof(HeaderViewModel));
        }

        [Test, AutoMoqData]
        public void Header_Data_Should_Be_Set_Correctly(IHeaderProperties headerProperties)
        {
            var headerViewModel = new HeaderViewModel(headerProperties);
            headerViewModel.Current.Should().NotBeNull();
        }

        [Test, AutoMoqData]
        public void Header_Menu_Data_Should_Be_Set_Correctly(IHeaderProperties headerProperties)
        {
            var headerMenuProperties = new Mock<HeaderMenuProperties>();
            var headerViewModel = new HeaderViewModel(headerProperties) { MenuProperties = headerMenuProperties.Object };
            headerViewModel.MenuProperties.Should().NotBeNull();
        }

        [Test, AutoMoqData]
        public void Header_Settings_Data_Should_Be_Set_Correctly(IHeaderProperties headerProperties)
        {
            var siteSettingsPage = new Mock<SiteSettingsPage>();
            var headerViewModel = new HeaderViewModel(headerProperties) { SiteSettingsProperties = siteSettingsPage.Object };
            headerViewModel.SiteSettingsProperties.Should().NotBeNull();
        }
    }
}