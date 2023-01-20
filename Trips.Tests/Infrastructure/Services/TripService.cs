using NUnit.Framework;
using Trips.Application.Common.Interfaces;
using Trips.Application.Trips.Interfaces;
using Trips.Application.Trips.Models;

namespace Trips.Tests.Infrastructure.Services
{
    [TestFixture]
    public class TripService_CreateTrip
    {
        ITripService? tripService;

        [SetUp]
        public void SetUp()
        {
            tripService = ServiceResolver.ResolveService<ITripService>();
        }

        [TestCase("Salty Adventure", ExpectedResult = true)]
        [TestCase("Vintage Experience", ExpectedResult = false)]
        [TestCase("Dark Ages", ExpectedResult = true)]
        [TestCase("Abcdefghijkl", ExpectedResult = true)]
        public async Task<bool> CreateTrip(string uniqueName)
        {
            TripDetailsRequestModel newTrip = new()
            {
                Name = uniqueName,
            };

            IConveyOperationResult res = await tripService!.CreateTripAsync(newTrip);

            return res.IsSuccessful;
        }
    }

    [TestFixture]
    public class TripService_UpdateTrip
    {
        ITripService? tripService;

        [SetUp]
        public void SetUp()
        {
            tripService = ServiceResolver.ResolveService<ITripService>();
        }

        [TestCase("Vintage Experience", null, ExpectedResult = false)]
        [TestCase("Mountain Dew", "Mountain Dew", ExpectedResult = true)]
        public async Task<bool> UpdateTrip(string uniqueName, string outdatedDataName)
        {
            TripDetailsRequestModel newTrip = new()
            {
                Name = uniqueName
            };

            IConveyOperationResult res = await tripService!.UpdateTripAsync(outdatedDataName, newTrip);

            return res.IsSuccessful;
        }
    }

    [TestFixture]
    public class TripService_DeleteTrip
    {
        ITripService? tripService;

        [SetUp]
        public void SetUp()
        {
            tripService = ServiceResolver.ResolveService<ITripService>();
        }

        [TestCase("Abcdefghijkl", ExpectedResult = true)]
        [TestCase("Abcdefghijk", ExpectedResult = false)]
        public async Task<bool> DeleteTrip(string uniqueName)
        {
            IConveyOperationResult res = await tripService!.DeleteTripAsync(uniqueName);

            return res.IsSuccessful;
        }
    }

    [TestFixture]
    public class TripService_ReisterForTrip
    {
        ITripService? tripService;

        [SetUp]
        public void SetUp()
        {
            tripService = ServiceResolver.ResolveService<ITripService>();
        }

        [TestCase("user@mail.com", "Salty Adventure", ExpectedResult = true)]
        [TestCase("user1@mail.com", "Salty Adventure", ExpectedResult = true)]
        [TestCase("user1@mail.com", "NonExistent", ExpectedResult = false)]
        public async Task<bool> RegisterForTrip(string mailAddress, string tripName)
        {
            ParticipantRequestModel participant = new()
            {
                MailAddress = mailAddress,
                TripName = tripName
            };

            IConveyOperationResult res = await tripService!.RegisterParticipantAsync(participant);

            return res.IsSuccessful;
        }
    }

    [TestFixture]
    public class TripService_GetTrips
    {
        ITripService? tripService;

        [SetUp]
        public void SetUp()
        {
            tripService = ServiceResolver.ResolveService<ITripService>();
        }

        [Test]
        public async Task GetTripsAsync()
        {
            IEnumerable<TripResponseModel> resultSet = await tripService!.GetTripsAsync();

            Assert.That(resultSet is not null);
        }

        [TestCase("Switzerland")]
        [TestCase("Spain")]
        public async Task GetTripsBySearchTermAsync_Exists(string searchTerm)
        {
            IEnumerable<TripResponseModel> resultSet = await tripService!.GetTripsBySearchTermAsync(searchTerm);

            Assert.That(resultSet.Any());
        }

        [TestCase("Switzerlan")]
        [TestCase("Spai")]
        [TestCase("")]
        public async Task GetTripsBySearchTermAsync_NotExists(string searchTerm)
        {
            IEnumerable<TripResponseModel> resultSet = await tripService!.GetTripsBySearchTermAsync(searchTerm);

            Assert.That(!resultSet.Any());
        }
    }

    [TestFixture]
    public class TripService_GetTripDetails
    {
        ITripService? tripService;

        [SetUp]
        public void SetUp()
        {
            tripService = ServiceResolver.ResolveService<ITripService>();
        }

        [TestCase("Vintage Experience", ExpectedResult = true)]
        [TestCase("Mountain Dew", ExpectedResult = true)]
        [TestCase("", ExpectedResult = false)]
        [TestCase("Sapin", ExpectedResult = false)]
        public async Task<bool> GetTripDetailsAsync(string uniqueNameIdentifier)
        {
            TripDetailsResponseModel? res = await tripService!.GetTripDetailsAsync(uniqueNameIdentifier);

            return res is not null;
        }
    }
}
