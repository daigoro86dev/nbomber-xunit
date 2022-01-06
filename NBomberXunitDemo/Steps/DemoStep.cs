using NBomber.Contracts;
using NBomber.CSharp;
using NBomberXunitDemo.Factories;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using Xunit.Abstractions;

namespace NBomberXunitDemo.Steps
{
    public class DemoStep : IDemoStep
    {
        private readonly IDemoHttpClientFactory _httpClientFactory;

        public DemoStep(IDemoHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public IStep CreateStep(string stepName)
        {
            var values = new Dictionary<string, string>{
                { "title", "foo" },
                { "body", "bar" },
                { "userId", "1" },
            };

            var json = JsonConvert.SerializeObject(values, Formatting.Indented);

            var stringContent = new StringContent(json);

            return Step.Create(stepName, clientFactory: _httpClientFactory.CreateHttpClient(), execute: async context =>
            {
                var response = await context.Client.PostAsync("https://jsonplaceholder.typicode.com/posts", stringContent, context.CancellationToken);

                context.Logger.Verbose(response.ToString());

                return response.IsSuccessStatusCode
                    ? Response.Ok(statusCode: (int)response.StatusCode)
                    : Response.Fail(statusCode: (int)response.StatusCode);
            });
        }
    }
}
