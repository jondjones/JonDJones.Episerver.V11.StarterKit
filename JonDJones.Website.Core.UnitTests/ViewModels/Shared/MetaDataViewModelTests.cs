namespace JonDJones.Website.Core.UnitTests.ViewModels.Shared
{
    using FluentAssertions;

    using Moq;

    using NUnit.Framework;

    using JonDJones.Website.TestShared.AutoFixutre.AutoData.NUnit3;
    using JonDJones.Website.TestShared.Base;

    using JonDJones.Website.Core.ViewModel.Shared;
    using JonDJones.Website.Interfaces;
    using JonDJones.Website.UnitTests.Helper;

    [TestFixture]
    public class When_I_Instantiate_MetaDataViewModelTests_ : TestBaseClass
    {
        [Test]
        public void The_Constructor_Should_Not_Accept_Null_Constructor_Arguments()
        {
            ParameterValidationHelper.ShouldNotAcceptNullConstructorArguments(typeof(MetaDataViewModel));
        }

        [Test]
        public void The_Constructor_Must_Throw_An_ArgumentNullException_With_No_Parameters()
        {
            ParameterValidationHelper.ConstructorMustThrowArgumentNullException(typeof(MetaDataViewModel));
        }

        [Test, AutoMoqData]
        public void Current_Should_Be_Set_Correctly(IPageMetaDataProperties pageMetaDataProperties)
        {
            var metaDataViewModel = new MetaDataViewModel(pageMetaDataProperties);
            metaDataViewModel.Current.Should().NotBeNull();
        }

        [Test, AutoMoqData]
        public void SEOTitle_Should_Be_Set_Correctly(string seoTitle)
        {
            var mockDependency = new Mock<IPageMetaDataProperties>();
            mockDependency.Setup(x => x.SeoTitle).Returns(seoTitle);

            var metaDataViewModel = new MetaDataViewModel(mockDependency.Object);
            metaDataViewModel.HasTitle.Should().BeTrue();
        }

        [Test, AutoMoqData]
        public void SEOTitle_Should_Be_False_When_Not_Set(IPageMetaDataProperties pageMetaDataProperties)
        {
            var metaDataViewModel = new MetaDataViewModel(pageMetaDataProperties);
            metaDataViewModel.HasTitle.Should().BeFalse();
        }
    }
}