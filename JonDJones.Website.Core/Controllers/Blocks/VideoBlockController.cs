using System.Web.Mvc;

using JonDJones.Website.Core.Blocks;
using JonDJones.Website.Core.Controllers.Base;
using JonDJones.Website.Core.ViewModel.Blocks;

namespace JonDJones.Website.Core.Controllers.Blocks
{
    public class VideoBlockController : BaseBlockController<VideoBlock>
    {
        public override ActionResult Index(VideoBlock currentBlock)
        {
            return PartialView("Index", new VideoBlockViewModel(currentBlock, WebsiteDependencies, DisplayOption));
        }
    }
}