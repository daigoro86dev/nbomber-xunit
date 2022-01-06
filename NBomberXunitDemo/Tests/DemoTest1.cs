using FluentAssertions;
using NBomber.Contracts;
using NBomber.CSharp;
using NBomberXunitDemo.Factories;
using NBomberXunitDemo.Scenarios;
using NBomberXunitDemo.Steps;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using Xunit;
using Xunit.Abstractions;

namespace NBomberXunitDemo.Tests
{
    public class DemoTest1
    {
        private readonly IDemoStep _stepBuilder;
        private readonly IDemoScenario _scenarioBuilder;
        private readonly IDemoHttpClientFactory _clientFactory;
        private readonly ITestOutputHelper _outputHelper;

        public DemoTest1(IDemoStep stepBuilder, IDemoScenario scenarioBuilder, IDemoHttpClientFactory clientFactory, ITestOutputHelper outputHelper)
        {
            _stepBuilder = stepBuilder;
            _scenarioBuilder = scenarioBuilder;
            _clientFactory = clientFactory;
            _outputHelper = outputHelper;
        }

        Scenario SetupScenario()
        {
            var values = new Dictionary<string, string>{
                { "title", "foo" },
                { "body", "bar" },
                { "userId", "1" },
            };

            var json = JsonConvert.SerializeObject(values, Formatting.Indented);

            var stringContent = new StringContent(json);

            var step = Step.Create("Post Todo", clientFactory: _clientFactory.CreateHttpClient(), execute: async context =>
            {
                var response = await context.Client.PostAsync("https://jsonplaceholder.typicode.com/posts", stringContent, context.CancellationToken);

                _outputHelper.WriteLine(response.ToString());

                return response.IsSuccessStatusCode
                    ? Response.Ok(statusCode: (int)response.StatusCode)
                    : Response.Fail(statusCode: (int)response.StatusCode);
            });


            return _scenarioBuilder
                .BuildScenario("Demo Scenario", step)
                .WithWarmUpDuration(TimeSpan.FromSeconds(1))
                .WithLoadSimulations(new[]
                {
                    Simulation.InjectPerSec(rate: 100, during: TimeSpan.FromSeconds(2))
                })
                .WithoutWarmUp();
        }

        [Fact]
        public void Test1()
        {
            var scenario = SetupScenario();
            var nodeStats = NBomberRunner.RegisterScenarios(scenario).Run();
            var stepStats = nodeStats.ScenarioStats[0].StepStats[0];

            _outputHelper.WriteLine(stepStats.ToString());

            stepStats.Ok.Request.Count.Should().Be(200);
        }
    }
}