using NBomber.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBomberXunitDemo.Scenarios
{
    public interface IDemoScenario
    {
        Scenario BuildScenario(string scenarioName, params IStep[] steps);
    }
}
