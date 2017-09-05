namespace JonDJones.Website.TestShared.Factory
{
    using Enums;

    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;
    using OpenQA.Selenium.Firefox;
    using OpenQA.Selenium.IE;

    public static class WebDriverFactory
    {
        public static IWebDriver GetWebDriver(BrowserTypeEnum browserType)
        {
            switch (browserType)
            {
                case BrowserTypeEnum.Firefox: return new FirefoxDriver();
                case BrowserTypeEnum.Chrome: return new ChromeDriver();
                case BrowserTypeEnum.Ie: return new InternetExplorerDriver();
            }

            return new ChromeDriver();
        }
    }
}
