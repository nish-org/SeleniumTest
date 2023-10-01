using CodingDemoTFL.Helpers;
using CodingDemoTFL.Setup;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Reflection;

namespace CodingDemoTFL.Extensions
{
    public static class ByExtensions
    {
        public static IWebElement WaitForElementToDisplay(this By element, int timeoutInSecond = 30)
        {
            var _supportDriver = WebDriverSupport.SupportDriver();
            _supportDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(0);
            var wait = GetDriverWait(_supportDriver, timeoutInSecond);
            try
            {
                wait.Until(x => x.FindElement(element).Displayed);
                _supportDriver.Manage().Timeouts().ImplicitWait = AppConfigManager.GetImplicitWait;
                return _supportDriver.FindElement(element);
            }
            catch (Exception e)
            {
                _supportDriver.Manage().Timeouts().ImplicitWait = AppConfigManager.GetImplicitWait;
                throw new ElementNotVisibleException(
                    $"Element:'{element}' is not displayed after timeout : '{timeoutInSecond}' seconds, \\n Exception:{e}");
            }
        }


        public static IWebElement WaitForElementToClickable(this By element, int timeoutInSecond = 30)
        {
            var _supportDriver = WebDriverSupport.SupportDriver();
            var ele = WaitForElementToDisplay(element, timeoutInSecond);

            _supportDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(0);
            var wait = GetDriverWait(_supportDriver, timeoutInSecond);
            wait.Until(x => x.FindElement(element).Enabled);

            _supportDriver.Manage().Timeouts().ImplicitWait = AppConfigManager.GetImplicitWait;
            return _supportDriver.FindElement(element);
        }

        public static void WaitForAllElementDisplayed(this By element, int count = 1, int timeoutInSecond = 60)
        {
            var _supportDriver = WebDriverSupport.SupportDriver();
            _supportDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(0);
            var wait = GetDriverWait(_supportDriver, timeoutInSecond);
            try
            {
                Func<IWebDriver, bool> waitForElement = new Func<IWebDriver, bool>((IWebDriver Web) =>
                {
                    if (_supportDriver.FindElements(element).Count >= count)
                    {
                        return true;
                    }

                    return false;
                });

                wait.Until(waitForElement);
                _supportDriver.Manage().Timeouts().ImplicitWait = AppConfigManager.GetImplicitWait;
            }
            catch (Exception e)
            {
                _supportDriver.Manage().Timeouts().ImplicitWait = AppConfigManager.GetImplicitWait;
                throw new ElementNotVisibleException($"Element:'{element}' is not displayed after timeout : '{timeoutInSecond}' seconds, \\n Error:{e}");
            }
        }

        

        public static IList<IWebElement> GetAllElements(this By element, int count = 1, int timeoutInSecond = 60)
        {
            var _supportDriver = WebDriverSupport.SupportDriver();
            _supportDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(0);
            WaitForAllElementDisplayed(element, count, timeoutInSecond);
            var allElements = _supportDriver.FindElements(element);
            _supportDriver.Manage().Timeouts().ImplicitWait = AppConfigManager.GetImplicitWait;
            return allElements;
        }

       
        public static int GetElementsCount(this By locator, int expectedCount = 1, int timeoutInSeconds = 60)
        {
            var _supportDriver = WebDriverSupport.SupportDriver();
            int count;
            _supportDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(0);
            var wait = GetDriverWait(_supportDriver, timeoutInSeconds);
            try
            {
                wait.Until(x => GetElementCount(locator) >= expectedCount);
                count = GetElementCount(locator);
            }
            catch
            {
                count = GetElementCount(locator);
                _supportDriver.Manage().Timeouts().ImplicitWait = AppConfigManager.GetImplicitWait;
                return count;
            }

            _supportDriver.Manage().Timeouts().ImplicitWait = AppConfigManager.GetImplicitWait;
            return count;
        }

        public static int GetElementCount(this By locator)
        {
            var _supportDriver = WebDriverSupport.SupportDriver();
            try
            {
                var list = _supportDriver.FindElements(locator);
                var count = list.Count;
                return count;
            }
            catch
            {
                return 0;
            }
        }
       
        public static bool IsElementDisplayed(this By locator, int timeoutInSecond = 30)
        {
            try
            {
                return WaitForElementToDisplay(locator, timeoutInSecond).Displayed;
            }
            catch
            {
                return false;
            }
        }

        public static void EnterText(this By locator, string text, bool clearPopulatedField = false, int timeoutInSecond = 30)
        {
            var element = WaitForElementToClickable(locator, timeoutInSecond);
            var driver = WebDriverSupport.SupportDriver();
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSecond));
            if(clearPopulatedField)
            {
                element.EnterText(text, clearPopulatedField, wait);
            }
            else
            {
                element.EnterText(text, clearPopulatedField);
            }
        }

        public static void ClickElement(this By locator, bool scroll = true, int timeoutInSecond = 30)
        {
            var ele = WaitForElementToClickable(locator, timeoutInSecond);

            if (scroll)
            {
                ScrollToMiddle(locator, timeoutInSecond);
            }

            try
            {
                ele.Click();
            }
            catch (TargetInvocationException)
            {
                IJavaScriptExecutor executor = (IJavaScriptExecutor)WebDriverSupport.SupportDriver();
                executor.ExecuteScript("arguments[0].click();", ele);
            }
            catch (ElementClickInterceptedException)
            {
                IJavaScriptExecutor executor = (IJavaScriptExecutor)WebDriverSupport.SupportDriver();
                executor.ExecuteScript("arguments[0].click();", ele);
            }
            catch (StaleElementReferenceException)
            {
                IJavaScriptExecutor executor = (IJavaScriptExecutor)WebDriverSupport.SupportDriver();
                executor.ExecuteScript("arguments[0].click();", ele);
            }
        }

        public static void ScrollToMiddle(this By locator, int timeoutInSecond = 30)
        {
            WebDriverSupport driver = new WebDriverSupport();
            var element = WaitForElementToDisplay(locator, timeoutInSecond);
            driver.ScrollToMiddle(element);
        }
        
        public static string GetText(this By locator, int timeoutInSecond = 30)
        {
            return WaitForElementToDisplay(locator, timeoutInSecond).Text;
        }
        
        //string attributeType = "value" or "Class" or "Name"
        public static string GetAttribute(this By locator, string attributeType = "value", int timeoutInSecond = 30)
        {
            return WaitForElementToDisplay(locator, timeoutInSecond).GetAttribute(attributeType);
        }

        private static WebDriverWait GetDriverWait(IWebDriver driver, int timeoutInSecond)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSecond));

            wait.IgnoreExceptionTypes(typeof(NoSuchFrameException),
                typeof(WebDriverException),
                typeof(StaleElementReferenceException),
                typeof(NoSuchElementException),
                typeof(ElementNotVisibleException),
                typeof(ElementNotInteractableException),
                typeof(ElementClickInterceptedException),
                typeof(ElementNotSelectableException));

            return wait;
        }
    }
}
