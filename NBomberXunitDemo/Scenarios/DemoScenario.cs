using NBomber.Contracts;
using NBomber.CSharp;

namespace NBomberXunitDemo.Scenarios
{
    public class DemoScenario : IDemoScenario
    {
        public Scenario BuildScenario(string scenarioName, params IStep[] steps)
        {
            return ScenarioBuilder.CreateScenario(scenarioName, steps);
        }
    }
}
