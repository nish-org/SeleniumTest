using CodingDemoTFL.Extensions;
using CodingDemoTFL.Helpers;
using CodingDemoTFL.Setup;
using Dynamitey;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Linq;

namespace CodingDemoTFL.Pages
{
    public class JourneyResultPage : WebDriverSupport
    {
        #region Elements
               
        By NumberOfResults = By.XPath("//div[@class='expandable-box click-through auto-expand not-bus publictransport-box  show-me']");
        By CyclingAndOtherOptions = By.XPath("//h2[text()='Cycling and other options']/following-sibling::div");
        By FisrtJourneyTotalTime = By.XPath("//div[@class='journey-time no-map']/span");
        By JouenryTimeFromAndTo = By.XPath("//div/span[@class='time']/span");
        By ViewDetailsBtn = By.XPath("//div[@id='option-1-content']/div/div/div/a[text()='View details']");
        By MapviewBtn = By.XPath("//div[@id='option-1-content']/div/div/div/a[text()='Map view']");
        By ErrorMessage = By.XPath("//div[contains(@class,'r ajax-response')]/ul/li");
        By EditJourney = By.XPath("//span[text()='Edit journey']");
        By InputFrom = By.Id("InputFrom");
        By InputTo = By.Id("InputTo");
        By UpdateJourney = By.Id("plan-journey-button");
        By HomeLnk = By.XPath("//a/span[text()='Home']");
        By EditJourneyPreference = By.XPath("//a[text()='Edit preferences']");
        By DeselectAll = By.XPath("//a[text()='deselect all']");
        By SelectTube = By.XPath("//label[text()='Tube']");
        By UpdatePreference = By.XPath("//div[@id='more-journey-options']//input[@value='Update journey']");

        By FromDetails(string stationName) => By.XPath($"//div[@id='option-1-content']/div/div/div//span[contains(text(),'Transfer to {stationName}')]");
        By ToDetails(string stationName) => By.XPath($"//div[@id='option-1-content']/div[1]/div[4]/div/div[2]/span[2]/span[text()='{stationName}']");

        #endregion

        #region Page Actions

        public void ClickHomeLink() => HomeLnk.ClickElement();

        public void ClickUpdateJourney() => UpdateJourney.ClickElement();

        public void SelectTubePreference()
        {
            WaitForResults();
            EditJourneyPreference.ClickElement();
            DeselectAll.ClickElement();
            SelectTube.ClickElement();
            UpdatePreference.ClickElement();
        }

        public void EditJourneyFromResult(string from, String to)
        {
            EditJourney.ClickElement();

            //if 'from' or 'to' is null then not going to edit station
            if (from != null)
            {
                InputFrom.EnterText(from, true);
            }
            if (to != null)
            {
                InputTo.EnterText(to, true);
            }
        }           

        public string GetAnErrorMessage() => ErrorMessage.GetText(5);

        public int GetNumberOfJourneyResults()
        {
           WaitForResults();
           return NumberOfResults.GetElementsCount();
        }

        public int GetOtherOptionDisplayed()
        {
            WaitForResults();
            return CyclingAndOtherOptions.GetElementCount();
        }

        public void VerifyJourneyDetails(string from, string to)
        {
            SelectTubePreference();
            from = GetStringAfterSplint(from);
            to = GetStringAfterSplint(to);
            WaitForResults();
            Assert.IsTrue(MapviewBtn.IsElementDisplayed(15), "Map View button is not displayed for the result");
            Assert.IsTrue(ViewDetailsBtn.IsElementDisplayed(15), "View details button is not displayed for the result");
            Assert.IsTrue(FromDetails(from).GetElementCount() >= 1, $"First Journey result is not contains from station : {from}");
            Assert.IsTrue(ToDetails(to).GetElementCount() >= 1, $"First Journey result is not contains to station : {to}");
            Assert.IsTrue(FisrtJourneyTotalTime.GetElementCount() >= 1, $"journey's total time is not displayed");
            Assert.IsTrue(JouenryTimeFromAndTo.GetElementCount() >= 1, $"journey's from and to time is not displayed");
        }

        //public void VerifyJourneyFromAndToLocation(string from, string to)
        //{
        //    WaitForResults();
        //    Assert.IsTrue(FromDetails(from).GetElementCount() >= 1, $"First Journey result is not contains from station : {from}");
        //    Assert.IsTrue(ToDetails(to).GetElementCount() >= 1, $"First Journey result is not contains to station : {to}");
        //    Assert.IsTrue(FisrtJourneyTotalTime.GetElementCount() >= 1, $"journey's total time is not displayed");
        //    Assert.IsTrue(JouenryTimeFromAndTo.GetElementCount() >= 1, $"journey's from and to time is not displayed");
        //}

        public void WaitForResults()
        {
            var wait = GetWebDriverWait();
            wait.Until(r => CyclingAndOtherOptions.GetElementCount() >= 2);
            WebDriverSupport.SupportDriver().Manage().Timeouts().ImplicitWait = AppConfigManager.GetImplicitWait;
        }

        private string GetStringAfterSplint(string str)
        {
            var idx = str.Split(" ");
            var fromPart = idx[0] + " " + idx[1];
            return fromPart;
        }
        #endregion
    }
}
