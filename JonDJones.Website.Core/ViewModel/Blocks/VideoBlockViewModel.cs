namespace JonDJones.Website.Core.ViewModel.Blocks
{
    using JonDJones.Website.Core.Blocks;
    using JonDJones.Website.Core.ViewModel.Base;

    using JonDJones.Website.Interfaces;
    using JonDJones.Website.Interfaces.Enums;
    using JonDJones.Website.Shared.Resources;
    
    public class VideoBlockViewModel : BlockViewModel<VideoBlock>
    {
        public VideoBlockViewModel(
            VideoBlock currentBlock,
            IWebsiteDependencies _websiteDependencies,
            DisplayOptionEnum displayOptionTag)
            : base(currentBlock, _websiteDependencies, displayOptionTag)
        {
        }

        public int AutoPlay => CurrentBlock.AutoPlay ? GlobalConstants.Video.On : GlobalConstants.Video.Off;

        public int ShowRelatedVideos => CurrentBlock.ShowRelatedVideos ? GlobalConstants.Video.On : GlobalConstants.Video.Off;

    }
}