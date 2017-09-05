namespace JonDJones.Website.Core.UnitTests.ViewModels.Shared
{
    using FluentAssertions;

    using NUnit.Framework;

    using JonDJones.Website.TestShared.AutoFixutre.AutoData.NUnit3;
    using JonDJones.Website.TestShared.Base;
    
    using JonDJones.Website.Core.ViewModel.Shared;
    using JonDJones.Website.Interfaces;
    using JonDJones.Website.UnitTests.Helper;

    [TestFixture]
    public class When_I_Instantiate_FooterViewModelTests_ : TestBaseClass
    {
        [Test]
        public void The_Constructor_Should_Not_Accept_Null_Constructor_Arguments()
        {
            ParameterValidationHelper.ShouldNotAcceptNullConstructorArguments(typeof(FooterViewModel));
        }

        [Test]
        public void The_Constructor_Must_Throw_An_ArgumentNullException_With_No_Parameters()
        {
            ParameterValidationHelper.ConstructorMustThrowArgumentNullException(typeof(FooterViewModel));
        }

        [Test, AutoMoqData]
        public void Footer_Data_Should_Be_Set_Correctly(IFooterProperties footerProperties)
        {
            var metaDataViewModel = new FooterViewModel(footerProperties);
            metaDataViewModel.Current.Should().NotBeNull();
        }
    }
}