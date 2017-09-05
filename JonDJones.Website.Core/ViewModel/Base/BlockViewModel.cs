namespace JonDJones.Website.Core.ViewModel.Base
{
    using EPiServer;
    using EPiServer.Core;
    using JonDJones.Website.Core.Extensions;
    using JonDJones.Website.Interfaces;
    using JonDJones.Website.Interfaces.Attributes;
    using JonDJones.Website.Interfaces.Enums;
    using JonDJones.Website.Shared.Helpers;

    public class BlockViewModel<T> : IBlockViewModel<T>
        where T : BlockData
    {
        public BlockViewModel(
            T currentBlock,
            IWebsiteDependencies _websiteDependencies,
            DisplayOptionEnum displayOptionTag)
        {
            Guard.ValidateObject(currentBlock);
            Guard.ValidateObject(_websiteDependencies);
            Guard.ValidateObject(displayOptionTag);

            WebsiteDependencies = _websiteDependencies;
            DisplayOptionTag = displayOptionTag;
            CurrentBlock = currentBlock;
        }

        public IWebsiteDependencies WebsiteDependencies { get; }

        public T CurrentBlock { get; }

        public DisplayOptionEnum DisplayOptionTag { get; set; }

        public string GetBootstrapClass()
        {
            return DisplayOptionTag.GetAttributeOfEnumValue<BootstrapClassAttribute>().IfNotDefault(x => x.Name);
        }
    }
}