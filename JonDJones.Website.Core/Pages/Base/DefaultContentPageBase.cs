namespace JonDJones.Website.Core.Pages.Base
{
    using System.ComponentModel.DataAnnotations;

    using EPiServer.Core;
    using EPiServer.DataAbstraction;

    public class DefaultContentPageBase : GlobalBasePage
    {
        [Display(
            Name = "Main Content Area",
            Description = "Region where content blocks can be placed",
            GroupName = SystemTabNames.Content,
            Order = 200)]
        public virtual ContentArea MainContentArea { get; set; }
    }
}
