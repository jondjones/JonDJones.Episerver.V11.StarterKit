namespace JonDJones.Website.Core.Controllers.Blocks
{
    using System.Web.Mvc;

    using JonDJones.Website.Core.Blocks;
    using JonDJones.Website.Core.Controllers.Base;
    using JonDJones.Website.Core.ViewModel.Blocks;

    public class RichTextBlockController : BaseBlockController<RichTextBlock>
    {
        public override ActionResult Index(RichTextBlock currentBlock)
        {
            return PartialView("Index", new RichTextBlockViewModel(currentBlock, WebsiteDependencies, DisplayOption));
        }
    }
}