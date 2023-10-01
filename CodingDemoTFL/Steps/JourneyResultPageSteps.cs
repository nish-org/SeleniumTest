using CodingDemoTFL.Pages;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingDemoTFL.Steps
{
    [Binding]
    public class JourneyResultPageSteps
    {
        private readonly PageObjectFactory _page;
        private ScenarioContext _scenarioContext;

        public JourneyResultPageSteps(PageObjectFactory page, ScenarioContext scenarioContext)
        {
            _page = page;
            _scenarioContext = scenarioContext;
        }

        [When(@"I edit the journey for destination station to '([^']*)'")]
        public void WhenIEditTheJourneyForDestinationStationTo(string destination)
        {
            _page.JourneyResultPage().EditJourneyFromResult(null, destination);
        }

        [When(@"I update the journey")]
        public void WhenIUpdateTheJourney()
        {
            _page.JourneyResultPage().ClickUpdateJourney();
        }

        [Then(@"I should be able to view journey from '([^']*)'  to '([^']*)'")]
        public void ThenIShouldBeAbleToViewJourneyFromTo(string from, string to)
        {
           // from = GetStringAfterSplint(from);
          //  to = GetStringAfterSplint(to);

            Assert.Multiple(() => {
                _page.JourneyResultPage().VerifyJourneyDetails(from, to);
            });
        }


        [Then(@"I should see an error message '(.*)'")]
        public void ThenIShouldSeeAnErrorMessage(string expectedErrorMessage)
        {
            var actualMessage = _page.JourneyResultPage().GetAnErrorMessage();
            Assert.AreEqual(expectedErrorMessage, actualMessage, "Error message is not displayed ");
        }

        [Then(@"the result page should have atleast (.*) journey")]
        public void ThenTheResultPageShouldHaveAtleastJourney(int numnerOfResult)
        {
            var actualResult = _page.JourneyResultPage().GetNumberOfJourneyResults();
            Assert.IsTrue(actualResult >= numnerOfResult, "Journey page is not contains atleast single journey");
        }

        [Then(@"the result page should include journey options, journey details and journey time")]
        public void ThenTheResultPageShouldIncludeJourneyOptionsJourneyDetailsAndJourneyTime()
        {
            var from = _scenarioContext.Get<string>("From");
            var to = _scenarioContext.Get<string>("To");
            
            Assert.Multiple(() => {
                _page.JourneyResultPage().VerifyJourneyDetails(from, to);
            });
        }

        

    }
}
