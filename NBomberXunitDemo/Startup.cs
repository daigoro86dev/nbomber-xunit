using Microsoft.Extensions.DependencyInjection;
using NBomberXunitDemo.Factories;
using NBomberXunitDemo.Scenarios;
using NBomberXunitDemo.Steps;

namespace NBomberXunitDemo
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IDemoHttpClientFactory, DemoHttpClientFactory>();
            services.AddScoped<IDemoStep, DemoStep>();
            services.AddScoped<IDemoScenario, DemoScenario>();
        }
    }
}