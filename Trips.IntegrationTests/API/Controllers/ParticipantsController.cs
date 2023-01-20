using NUnit.Framework;
using System.Net;
using Trips.Application.Trips.Models;
using Trips.IntegrationTests.Base;

namespace Trips.IntegrationTests.API.Controllers
{
    [TestFixture]
    public class ParticipantsController_POST : HttpTestFixtureBase
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

            var endpoint = $"/api/participants";

            var response = await client.PostAsync(endpoint, PrepareHttpContent(newData));

            string message = await response.Content.ReadAsStringAsync();

            Assert.That(response?.StatusCode is HttpStatusCode.OK, message);
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

            var endpoint = $"/api/participants";

            var response = await client.PostAsync(endpoint, PrepareHttpContent(newData));

            Assert.That(response?.StatusCode is HttpStatusCode.BadRequest, Is.EqualTo(true));
        }
    }
}
