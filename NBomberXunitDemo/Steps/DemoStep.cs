using NBomber.Contracts;
using NBomber.CSharp;
using NBomberXunitDemo.Factories;

namespace NBomberXunitDemo.Steps
{
    public class DemoStep : IDemoStep
    {
        private readonly IDemoHttpClientFactory _httpClientFactory;

        public DemoStep(IDemoHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public IStep CreateStep(string stepName, string url)
        {
            return Step.Create(stepName, clientFactory: _httpClientFactory.CreateHttpClient(), execute: async context =>
            {
                var response = await context.Client.GetAsync(url, context.CancellationToken);

                return response.IsSuccessStatusCode
                    ? Response.Ok(statusCode: (int)response.StatusCode)
                    : Response.Fail(statusCode: (int)response.StatusCode);
            });
        }
    }
}
