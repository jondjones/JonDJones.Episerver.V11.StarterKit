namespace JonDJones.Website.Core.UnitTests.ViewModels.Base
{
    using FluentAssertions;

    using Moq;

    using NUnit.Framework;

    using JonDJones.Website.TestShared.AutoFixutre.AutoData.NUnit3;
    using JonDJones.Website.TestShared.Base;

    using JonDJones.Website.Core.Blocks;
    using JonDJones.Website.Core.Extensions;
    using JonDJones.Website.Core.ViewModel.Base;
    using JonDJones.Website.Interfaces;
    using JonDJones.Website.Interfaces.Attributes;
    using JonDJones.Website.Interfaces.Enums;
    using JonDJones.Website.UnitTests.Helper;

    [TestFixture]
    public class When_I_Instantiate_BaseBlockModel_ : TestBaseClass
    {
        private BlockViewModel<RichTextBlock> baseViewModel;

        private RichTextBlock dummyBlock;

        [SetUp]
        public void SetUp()
        {
            dummyBlock = new RichTextBlock();
        }

        [Test]
        public void The_Constructor_Must_Throw_An_ArgumentNullException_With_No_Parameters()
        {
            ParameterValidationHelper.ShouldNotAcceptNullConstructorArguments(typeof(BlockViewModel<RichTextBlock>));
        }

        [Test, AutoMoqData]
        public void CurrentBlock_Should_Be_Set_Correctly(IWebsiteDependencies dependencies)
        {
            baseViewModel = new BlockViewModel<RichTextBlock>(dummyBlock, dependencies, DisplayOptionEnum.Full);
            baseViewModel.CurrentBlock.Should().NotBeNull();
        }

        [Test, AutoMoqData]
        public void Dependencies_Should_Be_Set_Correctly(IWebsiteDependencies dependencies)
        {
            baseViewModel = new BlockViewModel<RichTextBlock>(dummyBlock, dependencies, DisplayOptionEnum.Full);
            baseViewModel.WebsiteDependencies.Should().NotBeNull();
        }

        [Test]
        [TestCase(DisplayOptionEnum.Full)]
        [TestCase(DisplayOptionEnum.Half)]
        [TestCase(DisplayOptionEnum.OneFourth)]
        [TestCase(DisplayOptionEnum.OneThird)]
        public void DisplayOptions_Works(DisplayOptionEnum param)
        {
            var mockDependencies = new Mock<IWebsiteDependencies>();

            baseViewModel = new BlockViewModel<RichTextBlock>(dummyBlock, mockDependencies.Object, param);
            baseViewModel.DisplayOptionTag.Should().Be(param);
        }

        [Test]
        [TestCase(DisplayOptionEnum.Full)]
        [TestCase(DisplayOptionEnum.Half)]
        [TestCase(DisplayOptionEnum.OneFourth)]
        [TestCase(DisplayOptionEnum.OneThird)]
        public void DisplayOptions_Class_Name_Works(DisplayOptionEnum param)
        {
            var mockDependencies = new Mock<IWebsiteDependencies>();

            baseViewModel = new BlockViewModel<RichTextBlock>(dummyBlock, mockDependencies.Object, param);
            baseViewModel.GetBootstrapClass().Should()
                .Be(param.GetAttributeOfEnumValue<BootstrapClassAttribute>().Name);
        }
    }
}
