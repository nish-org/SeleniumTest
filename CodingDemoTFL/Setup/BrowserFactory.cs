using CodingDemoTFL.Enums;
using CodingDemoTFL.Helpers;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using System.Reflection;

namespace CodingDemoTFL.Setup
{
    public class BrowserFactory
    {
        public static ThreadLocal<IWebDriver> Driver = new ThreadLocal<IWebDriver>();
      
        public static IWebDriver InitBrowser(BrowserType? browserType)
        {
            browserType = browserType ?? BrowserType.Chrome ;

            switch (browserType)
            {
                case BrowserType.Chrome:
                    Driver.Value = ChromeDriver();
                    break;
                case BrowserType.Firefox:
                    Driver.Value = FireFoxDriver();
                    break;
                case BrowserType.Ie:
                    Driver.Value = InternetExplorerDriver();
                    break;
                default:
                    Driver.Value = ChromeDriver();
                    break;
            }
            Driver.Value.Manage().Timeouts().ImplicitWait = AppConfigManager.GetImplicitWait;
            Driver.Value.Manage().Timeouts().PageLoad = AppConfigManager.GetImplicitWait;
            Driver.Value.Manage().Window.Maximize();

            return Driver.Value;
        }
        
        #region Browser configurations

        private static IWebDriver ChromeDriver()
        {
            var chromeOptions = new ChromeOptions();
            if (AppConfigManager.IsRunHeadless)
            {
                chromeOptions.AddArgument("headless");
                chromeOptions.AddArgument("disable-gpu");
                chromeOptions.AddArgument("no-sandbox");
                chromeOptions.AddArguments("window-size=1920,1080");
            }

            Driver.Value = new ChromeDriver(chromeOptions);

            return Driver.Value;
        }

        private static IWebDriver FireFoxDriver()
        {
            var options = new FirefoxOptions();
            options.AddArgument("-incognito");
            options.AddArgument("--no-sandbox");
            Driver.Value = new FirefoxDriver(options);
           return Driver.Value;
        }

        private static IWebDriver InternetExplorerDriver()
        {
           var dir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var fileLocation = Path.Combine(dir ?? "", @"Drivers");

            var options = new InternetExplorerOptions { IgnoreZoomLevel = true, RequireWindowFocus = false, EnablePersistentHover = false };

            //options.AddAdditionalCapability("ignoreZoomSetting",true);
            //options.AddAdditionalCapability("EnableNativeEvents",false);
            Driver.Value = new InternetExplorerDriver(fileLocation, options);

            return Driver.Value;
        }

        #endregion

    }
}
