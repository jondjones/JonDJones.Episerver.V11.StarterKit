namespace JonDJones.Website.Shared.Interfaces
{
    using System.Web;

    public interface ICookieManager
    {
        string GetCookieValue(string cookieName);

        void SetCookie(HttpCookie userCookie);
    }
}