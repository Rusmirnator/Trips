using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Net.Http.Headers;
using System.Text;
using Trips.API;

namespace Trips.Tests.Base
{
    /// <summary>
    /// Base class for all http involved TestFixtures. Contains common methods and overridable setup.
    /// </summary>
    public class HttpTestFixtureBase
    {
        protected HttpClient client;

        [SetUp]
        protected virtual void SetUp()
        {
            var application = new WebApplicationFactory<Program>()
                .WithWebHostBuilder(builder => { });

            client = application.CreateClient();
        }

        protected static StringContent PrepareHttpContent<T>(T model)
        {
            return new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
        }

        protected static HttpRequestMessage PrepareHeaders(HttpRequestMessage req)
        {
            req.Headers.Accept.Clear();
            req.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return req;
        }
    }
}
