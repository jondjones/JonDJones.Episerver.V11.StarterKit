namespace JonDJones.Website.Core.Pages.Base
{
    using EPiServer.Core;
    using EPiServer.DataAnnotations;

    [AvailableContentTypes(Include = new[]
                                         {
                                             typeof(StartPage)
                                         })]
    [ContentType(
        AvailableInEditMode = false,
        Description = "This class uses the same GUID as the Episerver CMS SysRoot,and it allows us to overriding some of the behaviours of the Episerver CMS SysRoot.",
        GUID = "3415B788-8359-4BF0-9375-2FE9BA90719B")]
    public class SysRoot : PageData
    {
    }
}