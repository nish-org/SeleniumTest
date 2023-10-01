using CodingDemoTFL.Enums;
using CodingDemoTFL.Helpers;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;


namespace CodingDemoTFL.Setup
{
    public class WebDriverSupport
    {
        [ThreadStatic] private static IWebDriver _supportDriver;

        public static IWebDriver SupportDriver()
        {
            return _supportDriver;
        }

        public WebDriverWait GetWebDriverWait(int second = 60)
        {
            _supportDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(0);
            var wait = new WebDriverWait(_supportDriver, TimeSpan.FromSeconds(second));
            return wait;
        }

        public void WaitUntilLoadingComplete(int timeout = 25)
        {
            _supportDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(0);
            IWait<IWebDriver> wait = new WebDriverWait(_supportDriver, TimeSpan.FromSeconds(timeout));
            bool WebPageLoaded(IWebDriver driver) => ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState")
                .Equals("complete");
            wait.Until(WebPageLoaded);
            _supportDriver.Manage().Timeouts().ImplicitWait = AppConfigManager.GetImplicitWait;
        }

        public IWebDriver LaunchDriver()
        {
            _supportDriver = BrowserFactory.InitBrowser((BrowserType)Enum.Parse(typeof(BrowserType), AppConfigManager.GetBrowser));
            return _supportDriver;
        }

        public void ScrollIntoView(IWebElement element)
        {
            ((IJavaScriptExecutor)_supportDriver).ExecuteScript("arguments[0].scrollIntoView(true);", element);
        }

        public void ScrollToMiddle(IWebElement element)
        {
            ((IJavaScriptExecutor)_supportDriver).ExecuteScript("arguments[0].scrollIntoView({block: 'center', inline: 'nearest'})", element);
        }
    }
}
