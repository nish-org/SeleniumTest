using System;
using System.Diagnostics;
using System.Linq;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.ErrorHandling;

namespace PamsCommon.Hooks
{
    [Binding]
    public class CommonHooks
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly FeatureContext _featureContext;
        private readonly IErrorProvider _errorProvider;

        public CommonHooks(ScenarioContext scenarioContext, FeatureContext featureContext, IErrorProvider errorProvider)
        {

            _scenarioContext = scenarioContext;
            _featureContext = featureContext;
            _errorProvider = errorProvider;
        }

        [BeforeScenario(Order = 0)]
        public void BeforeScenario()
        {
            var featureTitle = _featureContext.FeatureInfo.Title;
            var tags = _featureContext.FeatureInfo.Tags;

            foreach (var tag in tags)
            {
                Console.WriteLine($"Tag:{tag}");
                Debug.WriteLine($"Tag:{tag}");
            }

            Console.WriteLine($"Feature Name: {featureTitle}");
            Console.WriteLine($"Scenario Name: {_scenarioContext.ScenarioInfo.Title}");
        }

        [AfterScenario(Order = 0)]
        public void AfterScenario()
        {
            if (_scenarioContext.ScenarioExecutionStatus == ScenarioExecutionStatus.UndefinedStep)
            {
                var error = _errorProvider.GetMissingStepDefinitionError();
                _errorProvider.ThrowPendingError(ScenarioExecutionStatus.UndefinedStep, $"Undefined steps : {error}");
            }
            if (_scenarioContext.ScenarioExecutionStatus == ScenarioExecutionStatus.BindingError)
            {
                var error =_errorProvider.GetMissingStepDefinitionError();
                _errorProvider.ThrowPendingError(ScenarioExecutionStatus.BindingError, $"Binding Error : {error}");
            }

            if (_scenarioContext.ScenarioExecutionStatus == ScenarioExecutionStatus.StepDefinitionPending)
            {
                var error = _errorProvider.GetPendingStepDefinitionError();
                _errorProvider.ThrowPendingError(ScenarioExecutionStatus.StepDefinitionPending, $"Step definition is not implemented :{error}");
            }

            if (_scenarioContext.ScenarioExecutionStatus == ScenarioExecutionStatus.Skipped)
            {
               var error = _errorProvider.GetMissingStepDefinitionError();
               _errorProvider.ThrowPendingError(ScenarioExecutionStatus.BindingError, $"Step Skipped : {error}");
            }
        }
    }
}
