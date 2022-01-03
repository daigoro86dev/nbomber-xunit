using NBomber.Contracts;
using NBomber.Plugins.Http.CSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace NBomberXunitDemo.Factories
{
    public class DemoHttpClientFactory : IDemoHttpClientFactory
    {
        public IClientFactory<HttpClient> CreateHttpClient()
        {
            return HttpClientFactory.Create();
        }
    }
}
