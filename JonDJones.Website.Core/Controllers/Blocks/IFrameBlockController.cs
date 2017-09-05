namespace JonDJones.Website.Core.Controllers.Blocks
{
    using System.Web.Mvc;

    using JonDJones.Website.Core.Blocks;
    using JonDJones.Website.Core.Controllers.Base;
    using JonDJones.Website.Core.ViewModel.Blocks;

    public class IFrameBlockController : BaseBlockController<IFrameBlock>
    {
        public override ActionResult Index(IFrameBlock currentBlock)
        {
            var frameBlockViewModel = new IFrameBlockViewModel(currentBlock, WebsiteDependencies, DisplayOption)
            {
                QueryStringNameValueCollection = Request.QueryString
            };

            return PartialView("Index", frameBlockViewModel);
        }
    }
}