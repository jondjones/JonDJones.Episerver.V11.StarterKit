namespace JonDJones.Website.Interfaces
{
    using EPiServer.Core;

    public interface IPageViewModel<out T> where T : PageData
    {
        ILayoutViewModel Layout { get; set; }

        T CurrentPage { get; }
    }
}