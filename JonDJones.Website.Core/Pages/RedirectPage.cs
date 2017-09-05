namespace JonDJones.Website.Core.Pages
{
    using System.ComponentModel.DataAnnotations;

    using EPiServer.Core;
    using EPiServer.DataAbstraction;
    using EPiServer.DataAnnotations;

    using JonDJones.Website.Shared.Resources;

    [ContentType(
        DisplayName = "Redirect Page",
        GUID = "56df299a-bf45-4c43-aa76-5c8984a8db43",
        Description = "Redirects users to parent page or Custom Redirect if defined",
        GroupName = GlobalConstants.GroupNames.Functional)]
    public class RedirectPage : PageData
    {
        [Display(
            Name = "Custom Redirect",
            Description = "Overrides redirect to parent and sets where the page should be redirected to",
            GroupName = SystemTabNames.Content,
            Order = 100)]
        public virtual PageReference CustomRedirect { get; set; }
    }
}