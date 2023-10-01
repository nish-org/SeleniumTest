using CodingDemoTFL.Pages;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TechTalk.SpecFlow.Assist;

namespace CodingDemoTFL.Steps
{
    [Binding]
    public class RecentTabSteps
    {
        private readonly PageObjectFactory _page;

        public RecentTabSteps(PageObjectFactory page)
        {
            _page = page;
        }

        [When(@"I view recent tab")]
        public void WhenIViewRecentTab()
        {
            _page.JourneyResultPage().ClickHomeLink();
           _page.HomePage().SelectRecentTab();
        }

        [Then(@"I should be able to see both my journey in recent tab")]
        public void ThenIShouldBeAbleToSeeBothMyJourneyInRecentTab(Table table)
        {
            var recentJouneyList = _page.RecentTabPage().GetRecentJourney();
                dynamic tab = table.CreateDynamicSet();
            foreach (var row in tab)
            {
                var journey = row.Journey;
                Assert.True(recentJouneyList.Contains(journey), 
                    $"Recent jouney list is not included journey : {journey} => Journey List : {recentJouneyList.ToString()}"); 
            }

        }
    }
}
