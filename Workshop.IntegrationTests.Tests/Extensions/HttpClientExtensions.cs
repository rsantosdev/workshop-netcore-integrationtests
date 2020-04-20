using System.Net.Http;

namespace Workshop.IntegrationTests.Tests.Extensions
{
    public static class HttpClientExtensions
    {
        public static HttpClient AsAnonymous(this HttpClient client)
        {
            client.DefaultRequestHeaders.Remove("Authorization");
            return client;
        }
    }
}
