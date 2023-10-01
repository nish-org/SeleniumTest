using CodingDemoTFL.Pages;
using OpenQA.Selenium;

namespace CodingDemoTFL.Pages
{
    public class PageObjectFactory : BasePage
    {
        private readonly IWebDriver _pageDriver;

        public PageObjectFactory(IWebDriver webDriver) : base(webDriver)
        {
            _pageDriver = webDriver;
        }

        public HomePage HomePage()
        {          
            return GetPage<HomePage>(_pageDriver);
        }

        public JourneyResultPage JourneyResultPage()
        {
            return GetPage<JourneyResultPage>(_pageDriver);
        }

        public RecentTabPage RecentTabPage()
        {
            return GetPage<RecentTabPage>(_pageDriver);
        }
    }
}
