using CodingDemoTFL.Setup;
using OpenQA.Selenium;
using System;

namespace CodingDemoTFL.Pages
{
    public class BasePage 
    {
        private IWebDriver _driver;
       
        public BasePage(IWebDriver driver)
        {
            _driver = driver;
        }

        public TPage GetPage<TPage>(IWebDriver driver) where TPage : new()
        {
            _driver ??= driver;
            var pageInstance = new TPage();
            return pageInstance;
        }

        public string Title => _driver.Title;

        public static string BaseUrl { get; set; }

        public static void NavigateToBase()
        {
            WebDriverSupport.SupportDriver().Navigate().GoToUrl(BaseUrl);
        }

        public static void NavigateToUrl(string url)
        {
           WebDriverSupport.SupportDriver().Navigate().GoToUrl(url);
        }

        public string GetPageSource()
        {
            return WebDriverSupport.SupportDriver().PageSource;
        }

    }
}
