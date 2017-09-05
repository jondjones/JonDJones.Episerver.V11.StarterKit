namespace JonDJones.Website.Core.Controllers.Base
{
    using EPiServer.Core;
    using EPiServer.Web.Mvc;

    public class BasePartialController<T> : PartialContentController<T>
        where T : IContentData
    {
    }
}