using NUnit.Framework;
using Trips.Application.Trips.Models;
using System.Net;
using Trips.IntegrationTests.Base;
using Microsoft.AspNetCore.Http;

namespace Trips.IntegrationTests.API.Controllers
{
    [TestFixture]
    public class TripsController_GET : HttpTestFixtureBase
    {
        [TestCase(null)]
        [TestCase("")]
        [TestCase("Switzerland")]
        public async Task GetTripsAsync_Ok(string searchTerm)
        {
            var endpoint = $"/api/trips?{nameof(searchTerm)}={WebUtility.UrlEncode(searchTerm)}";

            var response = await client.GetAsync(endpoint);

            Assert.That(response?.StatusCode is HttpStatusCode.OK, Is.EqualTo(true));
        }

        [TestCase("ToFind")]
        [TestCase("ToFindTwo")]
        public async Task GetTripDetailsAsync_Ok(string uniqueNameIdentifier)
        {
            var endpoint = $"/api/trips/{WebUtility.UrlEncode(uniqueNameIdentifier)}/details";

            var response = await client.GetAsync(endpoint);

            Assert.That(response?.StatusCode is HttpStatusCode.OK, Is.EqualTo(true));
        }

        [TestCase("Sandy Experice")]
        [TestCase("Vintage Experince")]
        [TestCase(null)]
        [TestCase("")]
        public async Task GetTripDetailsAsync_NotFound(string uniqueNameIdentifier)
        {
            var endpoint = $"/api/trips/{WebUtility.UrlEncode(uniqueNameIdentifier)}/details";

            var response = await client.GetAsync(endpoint);

            Assert.That(response?.StatusCode is HttpStatusCode.NotFound, Is.EqualTo(true));
        }
    }

    [TestFixture]
    public class TripsController_POST : HttpTestFixtureBase
    {
        [TestCase("a", 3)]
        [TestCase("b", 100)]
        [TestCase("c", 0)]
        public async Task CreateTripAsync_ValidModel(string country, int? numberOfSeats)
        {
            TripDetailsRequestModel newData = new()
            {
                Name = Guid.NewGuid().ToString(),
                Country = country,
                NumberOfSeats = numberOfSeats
            };

            var endpoint = $"/api/trips";

            var response = await client.PostAsync(endpoint, PrepareHttpContent(newData));

            Assert.That(response?.StatusCode is HttpStatusCode.Created, Is.EqualTo(true));
        }

        [TestCase("ValidNameValidNameValidNameValidNameValidNameValidNameValidNameValidNameValidName", "ValidCountry", 100)]
        [TestCase("ValidName", "ValidCountry1ValidCountry1ValidCountry1ValidCountry1ValidCountry1ValidCountry1ValidCountry1", 0)]
        [TestCase("", "ValidCountry1", 0)]
        [TestCase("ValidName1", "ValidCountry", -1)]
        [TestCase(null, "ValidCountry", -1)]
        public async Task CreateTripAsync_InvalidModel(string tripName, string country, int? numberOfSeats)
        {
            TripDetailsRequestModel newData = new()
            {
                Name = tripName,
                Country = country,
                NumberOfSeats = numberOfSeats
            };

            var endpoint = $"/api/trips";

            var response = await client.PostAsync(endpoint, PrepareHttpContent(newData));

            Assert.That(response?.StatusCode is HttpStatusCode.BadRequest, Is.EqualTo(true));
        }
    }

    [TestFixture]
    public class TripsController_PUT : HttpTestFixtureBase
    {
        [TestCase("Vintage Experience")]
        [TestCase("Mountain Dew")]
        public async Task UpdateTripAsync_ValidModel(string tripName)
        {
            TripDetailsRequestModel newData = new()
            {
                Name = tripName,
                Country = "abc",
                StartDate = DateTime.Now,
                Description = Guid.NewGuid().ToString(),
                NumberOfSeats = 0
            };

            var endpoint = $"/api/trips/{WebUtility.UrlEncode(tripName)}";

            var response = await client.PutAsync(endpoint, PrepareHttpContent(newData));

            string message = await response.Content.ReadAsStringAsync();

            Assert.That(response?.StatusCode is HttpStatusCode.NoContent, message);
        }

        [TestCase("ValidNameValidNameValidNameValidNameValidNameValidNameValidNameValidNameValidName", "ValidCountry", 100)]
        [TestCase("ValidName", "ValidCountry1ValidCountry1ValidCountry1ValidCountry1ValidCountry1ValidCountry1ValidCountry1", 0)]
        [TestCase("ValidName1", "ValidCountry", -1)]
        public async Task UpdateTripAsync_InvalidModel(string tripName, string country, int? numberOfSeats)
        {
            TripDetailsRequestModel newData = new()
            {
                Name = tripName,
                Country = country,
                StartDate = DateTime.Now,
                Description = Guid.NewGuid().ToString(),
                NumberOfSeats = numberOfSeats
            };

            var endpoint = $"/api/trips/{WebUtility.UrlEncode(tripName)}";

            var response = await client.PutAsync(endpoint, PrepareHttpContent(newData));

            Assert.That(response?.StatusCode is HttpStatusCode.BadRequest, Is.EqualTo(true));
        }

        [TestCase(null, "ValidCountry", -1)]
        [TestCase("", "ValidCountry1", 0)]
        public async Task UpdateTripAsync_InvalidRoute(string tripName, string country, int? numberOfSeats)
        {
            TripDetailsRequestModel newData = new()
            {
                Name = tripName,
                Country = country,
                StartDate = DateTime.Now,
                Description = Guid.NewGuid().ToString(),
                NumberOfSeats = numberOfSeats
            };

            var endpoint = $"/api/trips/{WebUtility.UrlEncode(tripName)}";

            var response = await client.PutAsync(endpoint, PrepareHttpContent(newData));

            Assert.That(response?.StatusCode is HttpStatusCode.MethodNotAllowed, Is.EqualTo(true));
        }
    }

    [TestFixture]
    public class TripsController_DELETE : HttpTestFixtureBase
    {
        [TestCase("ToDelete")]
        [TestCase("ToDeleteTwo")]
        public async Task DeleteTripAsync_ValidRoute(string uniqueNameIdentifier)
        {
            HttpRequestMessage request = new(HttpMethod.Delete, $"/api/trips/{WebUtility.UrlEncode(uniqueNameIdentifier)}");

            var response = await client.SendAsync(PrepareHeaders(request));

            string message = await response.Content.ReadAsStringAsync();

            Assert.That(response?.StatusCode is HttpStatusCode.NoContent, message);
        }

        [TestCase(null)]
        [TestCase("")]
        public async Task DeleteTripAsync_InvalidRoute(string uniqueNameIdentifier)
        {
            HttpRequestMessage request = new(HttpMethod.Delete, $"/api/trips/{WebUtility.UrlEncode(uniqueNameIdentifier)}");

            var response = await client.SendAsync(PrepareHeaders(request));

            string message = await response.Content.ReadAsStringAsync();

            Assert.That(response?.StatusCode is HttpStatusCode.MethodNotAllowed, message);
        }

        [TestCase("sad")]
        [TestCase("zx")]
        public async Task DeleteTripAsync_NotFound(string uniqueNameIdentifier)
        {
            HttpRequestMessage request = new(HttpMethod.Delete, $"/api/trips/{WebUtility.UrlEncode(uniqueNameIdentifier)}");

            var response = await client.SendAsync(PrepareHeaders(request));

            string message = await response.Content.ReadAsStringAsync();

            Assert.That(response?.StatusCode is HttpStatusCode.NotFound, message);
        }
    }
}
