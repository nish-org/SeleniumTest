using CodingDemoTFL.Setup;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Reflection;

namespace CodingDemoTFL.Extensions
{
    public static class WebElementExtensions
    {
        public static void EnterText(this IWebElement e, string text, bool clearPopulatedField = false, WebDriverWait wait = null)
        {
            // Field has text and calling element intends to clear
            if (clearPopulatedField)
            {
                wait.Until(ClearTextField(e));
            }
            e.SendKeys(text);
        }

        private static Func<IWebDriver, bool> ClearTextField(IWebElement e)
        {
            return (x) =>
            {
                e.Clear();
                return (e.GetAttribute("value") == string.Empty) || (e.GetAttribute("value") == null);
            };
        }       

        public static void ClickElement(this IWebElement e, bool scroll = false)
        {
            if (scroll)
            {
                IJavaScriptExecutor executor = (IJavaScriptExecutor)WebDriverSupport.SupportDriver();
                executor.ExecuteScript("arguments[0].scrollIntoView(true);", e);
            }

            try
            {
                e.Click();
            }
            catch (TargetInvocationException)
            {
                IJavaScriptExecutor executor = (IJavaScriptExecutor)WebDriverSupport.SupportDriver();
                executor.ExecuteScript("arguments[0].click();", e);
            }
            catch (ElementClickInterceptedException)
            {
                IJavaScriptExecutor executor = (IJavaScriptExecutor)WebDriverSupport.SupportDriver();
                executor.ExecuteScript("arguments[0].click();", e);
            }
        }
    }
}
