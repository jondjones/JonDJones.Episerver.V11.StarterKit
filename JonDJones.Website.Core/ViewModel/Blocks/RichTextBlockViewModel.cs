namespace JonDJones.Website.Core.ViewModel.Blocks
{
    using JonDJones.Website.Core.Blocks;
    using JonDJones.Website.Core.ViewModel.Base;
    using JonDJones.Website.Interfaces;
    using JonDJones.Website.Interfaces.Enums;

    public class RichTextBlockViewModel : BlockViewModel<RichTextBlock>
    {
        public RichTextBlockViewModel(
            RichTextBlock currentBlock,
            IWebsiteDependencies _websiteDependencies,
            DisplayOptionEnum displayOptionTag)
            : base(currentBlock, _websiteDependencies, displayOptionTag)
        {
        }
    }
}
