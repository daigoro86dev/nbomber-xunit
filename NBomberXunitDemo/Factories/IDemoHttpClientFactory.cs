using NBomber.Contracts;
using System.Net.Http;

namespace NBomberXunitDemo.Factories
{
    public interface IDemoHttpClientFactory
    {
        IClientFactory<HttpClient> CreateHttpClient();
    }
}
