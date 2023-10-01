using CodingDemoTFL.Extensions;
using CodingDemoTFL.Setup;
using OpenQA.Selenium;

namespace CodingDemoTFL.Pages
{
    public class HomePage : WebDriverSupport
    {

        #region Elements

        By AccptCookies = By.XPath("//button/strong[text()='Accept all cookies']");
        By PageHeader = By.XPath("//div[text()='Plan a journey']");

        By FromTxt = By.Id("InputFrom");
        By ToTxt = By.Id("InputTo");
        By PlanMyJournetBtn = By.Id("plan-journey-button");

        By RecentsTab = By.XPath("//div[@id='recent-journeys']/ul/li/a[text()='Recents']");

        By InputError = By.Id("InputFrom-error");
        By OutputError = By.Id("InputTo-error");
        #endregion

        #region Page Actions

        public string GetErrorMsgFromInput => InputError.GetText();
        public string GetErrorMsgToInput => OutputError.GetText();
        public bool IsHomePageHeaderDisplayed => PageHeader.IsElementDisplayed();
        public void ClickPlanMyJourney() => PlanMyJournetBtn.ClickElement();
        public void SelectRecentTab()
        {
            RecentsTab.GetAllElements().First().ClickElement();
        }

        public void EnterJourneyDetails(string from, string to)
        {
           FromTxt.EnterText(from);
           ToTxt.EnterText(to);
        }

        public void AcceptAllCokies()
        {
            if (AccptCookies.GetAllElements(count: 1, timeoutInSecond: 10).Count == 1)
            {
                AccptCookies.ClickElement();
            }
        }
       
        #endregion
    }
}
