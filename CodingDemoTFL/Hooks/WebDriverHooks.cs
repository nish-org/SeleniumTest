using BoDi;
using CodingDemoTFL.Helpers;
using CodingDemoTFL.Pages;
using CodingDemoTFL.Setup;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Diagnostics;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace CodingDemoTFL.Hooks
{
    [Binding]
    public class WebDriverHooks
    {
        [ThreadStatic] static IWebDriver? _driver;

        private readonly IObjectContainer _objectContainer;
        private readonly ScenarioContext _scenarioContext;
        private readonly WebDriverSupport _webDriverSupport;
               
        public WebDriverHooks(IObjectContainer objectContainer, ScenarioContext scenarioContext, WebDriverSupport webDriverSupport)
        {
            _objectContainer = objectContainer;
            _scenarioContext = scenarioContext;
            _webDriverSupport = webDriverSupport;
        }


        [BeforeScenario(Order = 1)]
        public void RunBeforeBrowserScenario()
        {             
            _driver = _webDriverSupport.LaunchDriver();
            _objectContainer.RegisterInstanceAs<IWebDriver>(_driver);
            BasePage.BaseUrl = AppConfigManager.GetWebBaseUrl;
        }

        [AfterScenario]
        public void RunAfterBrowserScenario()
        {
            if (!_objectContainer.IsRegistered<IWebDriver>()) return;
            _driver = _objectContainer.Resolve<IWebDriver>();

            try
            {
                ErrorCheck(_driver);
                _driver.Close();
                _driver.Quit();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

                try
                {
                    foreach (var process in Process.GetProcesses())
                    {
                        if (process.ProcessName == "chromedriver")
                        {
                            process.Kill();
                        }
                    }
                }
                catch (Exception a)
                {
                    Debug.WriteLine(a.Message);
                }
            }
        }

        private void ErrorCheck(IWebDriver driver)
        {
             
            if (_scenarioContext.TestError != null)
            {
                TakeScreenshot(driver);
                var error = _scenarioContext.TestError;

                Debug.WriteLine("An error occurred:" + error.Message + error.StackTrace);
                Debug.WriteLine("It was of type:" + error.GetType().Name);

                Console.WriteLine("An error occurred:" + error.Message + error.StackTrace);
                Console.WriteLine("It was of type:" + error.GetType().Name);
            }
        }

        private void TakeScreenshot(IWebDriver driver)
        {
            if (_scenarioContext.TestError != null)
            {
                var screenshotWebDriver = (ITakesScreenshot)driver;
                if (screenshotWebDriver != null)
                {
                    var path = Directory.GetCurrentDirectory();
                    var screenshotFile = $"{path}{DateTime.Now:dd.MM.yyyy-HH.mm.ss.ff}.png";
                    File.WriteAllBytes(screenshotFile, screenshotWebDriver.GetScreenshot().AsByteArray);
                    TestContext.AddTestAttachment(screenshotFile);
                    Console.WriteLine($"Path:{screenshotFile}");
                }
            }
        }
    }
}
