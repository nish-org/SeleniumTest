using CodingDemoTFL.Pages;
using NUnit.Framework;

namespace CodingDemoTFL.Steps
{
    [Binding]
    public class HomePageSteps
    {
        private readonly PageObjectFactory _page;
        private ScenarioContext _scenarioContext;   
              
        public HomePageSteps(PageObjectFactory page, ScenarioContext scenarioContext)
        {
            _page = page;
            _scenarioContext = scenarioContext;
        }

        [Given(@"User is navigate to jouney planner page")]
        public void GivenUserIsNavigateToJouneyPlannerPage()
        {
            BasePage.NavigateToBase();
            _page.HomePage().WaitUntilLoadingComplete();
            Assert.IsTrue(_page.HomePage().IsHomePageHeaderDisplayed, "Home Page is not displayed");
            _page.HomePage().AcceptAllCokies();
        }

        [Given(@"I enter a journey from '(.*)'  to '(.*)'")]
        public void GivenIEnterAJourneyFromTo(string from, string to)
        {
            EnterJourneyData(from, to);
        }

        [Given(@"I enter no data to journey from and to field")]
        public void GivenIEnterNoDataToJourneyFromAndToField()
        {
            EnterJourneyData("", "");
        }

        [Given(@"I plan journey from '([^']*)'  to '([^']*)'")]
        public void GivenIPlanJourneyFromTo(string from, string to)
        {
            EnterJourneyData(from, to);
            ClickMyPlanJourney();
            _page.JourneyResultPage().WaitForResults();
        }

        [Given(@"I plan first journey from '([^']*)'  to '([^']*)'")]
        public void GivenIPlanFirstJourneyFromTo(string from, string to)
        {
            PlanJourney(from, to);
        }
        
        [Given(@"I navigate to home page")]
        [Given(@"I navigate to home page to plan a new journey")]
        public void GivenINavigateToHomePageToPlanANewJourney()
        {
            _page.JourneyResultPage().ClickHomeLink();
        }

        [When(@"I plan my journey now")]
        public void WhenIPlanMyJourneyNow()
        {
            ClickMyPlanJourney();
        }

        [Then(@"the validation error triggered for From field as '([^']*)'")]
        public void ThenTheValidationErrorTriggeredForFromFieldAs(string expectedErrorMsg)
        {
            Assert.AreEqual(expectedErrorMsg, _page.HomePage().GetErrorMsgFromInput);
        }

        [Then(@"the validation error triggered for To field as '([^']*)'")]
        public void ThenTheValidationErrorTriggeredForToFieldAs(string expectedErrorMsg)
        {
            Assert.AreEqual(expectedErrorMsg, _page.HomePage().GetErrorMsgToInput);
        }
         
        private void EnterJourneyData(string from, string to)
        {
            _scenarioContext.Add("From", from);
            _scenarioContext.Add("To", to);
            _page.HomePage().EnterJourneyDetails(from, to);
        }

        private void PlanJourney(string from, string to)
        {
            _page.HomePage().EnterJourneyDetails(from, to);
            ClickMyPlanJourney();
            _page.JourneyResultPage().VerifyJourneyDetails(from, to);
        }

        private void ClickMyPlanJourney()
        {
            _page.HomePage().ClickPlanMyJourney();
        }
    }
}
