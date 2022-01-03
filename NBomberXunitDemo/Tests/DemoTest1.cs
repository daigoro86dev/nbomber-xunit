using FluentAssertions;
using NBomber.Contracts;
using NBomber.CSharp;
using NBomberXunitDemo.Scenarios;
using NBomberXunitDemo.Steps;
using System;
using Xunit;
using Xunit.Abstractions;

namespace NBomberXunitDemo.Tests
{
    public class DemoTest1
    {
        private readonly IDemoStep _stepBuilder;
        private readonly IDemoScenario _scenarioBuilder;
        private readonly ITestOutputHelper outputHelper;

        public DemoTest1(IDemoStep stepBuilder, IDemoScenario scenarioBuilder, ITestOutputHelper outputHelper)
        {
            _stepBuilder = stepBuilder;
            _scenarioBuilder = scenarioBuilder;
            this.outputHelper = outputHelper;
        }

        Scenario SetupScenario()
        {
            var step = _stepBuilder.CreateStep("Fetch all products", "http://localhost:5000/Product/GetProducts");

            return _scenarioBuilder
                .BuildScenario("Demo Scenario", step)
                .WithWarmUpDuration(TimeSpan.FromSeconds(2))
                .WithLoadSimulations(new[]
                {
                    Simulation.InjectPerSec(rate: 100, during: TimeSpan.FromSeconds(5))
                })
                .WithoutWarmUp();
        }

        [Fact]
        public void Test1()
        {
            var scenario = SetupScenario();
            var nodeStats = NBomberRunner.RegisterScenarios(scenario).Run();
            var stepStats = nodeStats.ScenarioStats[0].StepStats[0];

            outputHelper.WriteLine(stepStats.ToString());

            stepStats.Ok.Request.Count.Should().BeGreaterThan(450);
        }
    }
}