namespace JonDJones.Website.Shared.Wrapper
{
    using System.Web;

    using JonDJones.Website.Shared.Interfaces;

    public class CookieManager : ICookieManager
    {
        public string GetCookieValue(string cookieName)
        {
            return HttpContext.Current.Request.Cookies[cookieName] != null
                       ? HttpContext.Current.Request.Cookies[cookieName].Value
                       : string.Empty;
        }

        public void SetCookie(HttpCookie userCookie)
        {
            HttpContext.Current.Response.SetCookie(userCookie);
            HttpContext.Current.Response.Cookies.Add(userCookie);
        }
    }
}