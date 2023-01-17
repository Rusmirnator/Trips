using NUnit.Framework;
using Trips.Application.Trips.Models;
using System.Net;
using Trips.IntegrationTests.Base;

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

        [TestCase("Sandy Experience")]
        [TestCase("Vintage Experience")]
        public async Task GetTripDetailsAsync_OkOrNotFound(string uniqueNameIdentifier)
        {
            var endpoint = $"/api/trips/detail?{nameof(uniqueNameIdentifier)}={WebUtility.UrlEncode(uniqueNameIdentifier)}";

            var response = await client.GetAsync(endpoint);

            Assert.That(response?.StatusCode is HttpStatusCode.OK or HttpStatusCode.NotFound, Is.EqualTo(true));
        }

        [TestCase(null)]
        [TestCase("")]
        public async Task GetTripDetailsAsync_BadRequest(string uniqueNameIdentifier)
        {
            var endpoint = $"/api/trips/detail?{nameof(uniqueNameIdentifier)}={WebUtility.UrlEncode(uniqueNameIdentifier)}";

            var response = await client.GetAsync(endpoint);

            Assert.That(response?.StatusCode is HttpStatusCode.BadRequest, Is.EqualTo(true));
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

            Assert.That(response?.StatusCode is HttpStatusCode.UnprocessableEntity or HttpStatusCode.BadRequest, Is.EqualTo(true));
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
                NumberOfSeats = 0,
                OutdatedData = new TripRequestModel
                {
                    Name = tripName
                }
            };

            var endpoint = $"/api/trips";

            var response = await client.PutAsync(endpoint, PrepareHttpContent(newData));

            string message = await response.Content.ReadAsStringAsync();

            Assert.That(response?.StatusCode is HttpStatusCode.Accepted or HttpStatusCode.BadRequest, message);
        }

        [TestCase("ValidNameValidNameValidNameValidNameValidNameValidNameValidNameValidNameValidName", "ValidCountry", 100)]
        [TestCase("ValidName", "ValidCountry1ValidCountry1ValidCountry1ValidCountry1ValidCountry1ValidCountry1ValidCountry1", 0)]
        [TestCase("", "ValidCountry1", 0)]
        [TestCase("ValidName1", "ValidCountry", -1)]
        [TestCase(null, "ValidCountry", -1)]
        public async Task UpdateTripAsync_InvalidModel(string tripName, string country, int? numberOfSeats)
        {
            TripDetailsRequestModel newData = new()
            {
                Name = tripName,
                Country = country,
                StartDate = DateTime.Now,
                Description = Guid.NewGuid().ToString(),
                NumberOfSeats = numberOfSeats,
                OutdatedData = new TripRequestModel
                {
                    Name = tripName
                }
            };

            var endpoint = $"/api/trips";

            var response = await client.PutAsync(endpoint, PrepareHttpContent(newData));

            Assert.That(response?.StatusCode is HttpStatusCode.UnprocessableEntity or HttpStatusCode.BadRequest, Is.EqualTo(true));
        }
    }

    [TestFixture]
    public class TripsController_PATCH : HttpTestFixtureBase
    {
        [TestCase("Vintage Experience", "user@gmail.com")]
        [TestCase("Mountain Dew", "user@gmail.com")]
        [TestCase("Mountain Dew", "user1@gmail.com")]
        public async Task RegisterForTripAsync_ValidModel(string tripName, string email)
        {
            ParticipantRequestModel newData = new()
            {
                TripName = tripName,
                MailAddress = email
            };

            var endpoint = $"/api/trips";

            var response = await client.PatchAsync(endpoint, PrepareHttpContent(newData));

            string message = await response.Content.ReadAsStringAsync();

            Assert.That(response?.StatusCode is HttpStatusCode.Accepted or HttpStatusCode.BadRequest, message);
        }

        [TestCase("Vintage Experience", "usergmail.com")]
        [TestCase("Mountain Dew", "             user@gmail.com")]
        [TestCase("Mountain Dew", "")]
        [TestCase("Mountain Dew", null)]
        [TestCase(null, "user1@gmail.com")]
        public async Task RegisterForTripAsync_InvalidModel(string tripName, string email)
        {
            ParticipantRequestModel newData = new()
            {
                TripName = tripName,
                MailAddress = email
            };

            var endpoint = $"/api/trips";

            var response = await client.PatchAsync(endpoint, PrepareHttpContent(newData));

            Assert.That(response?.StatusCode is HttpStatusCode.UnprocessableEntity or HttpStatusCode.BadRequest, Is.EqualTo(true));
        }
    }

    [TestFixture]
    public class TripsController_DELETE : HttpTestFixtureBase
    {
        [TestCase("Vintage Experience")]
        [TestCase("Mountain Dew")]
        [TestCase("a")]
        public async Task DeleteTripAsync_ValidModel(string uniqueNameIdentifier)
        {
            HttpRequestMessage request = new(HttpMethod.Delete, $"/api/trips?{nameof(uniqueNameIdentifier)}={uniqueNameIdentifier}");

            var response = await client.SendAsync(PrepareHeaders(request));

            string message = await response.Content.ReadAsStringAsync();

            Assert.That(response?.StatusCode is HttpStatusCode.Accepted or HttpStatusCode.NotFound, message);
        }

        [TestCase(null)]
        [TestCase("")]
        public async Task DeleteTripAsync_InvalidModel(string uniqueNameIdentifier)
        {
            HttpRequestMessage request = new(HttpMethod.Delete, $"/api/trips?{nameof(uniqueNameIdentifier)}={uniqueNameIdentifier}");

            var response = await client.SendAsync(PrepareHeaders(request));

            string message = await response.Content.ReadAsStringAsync();

            Assert.That(response?.StatusCode is HttpStatusCode.UnprocessableEntity or HttpStatusCode.BadRequest, message);
        }
    }
}
