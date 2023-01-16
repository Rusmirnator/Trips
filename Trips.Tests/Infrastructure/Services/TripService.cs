using NUnit.Framework;
using Trips.Application.Common.Interfaces;
using Trips.Application.Trips.Interfaces;
using Trips.Application.Trips.Models;

namespace Trips.Tests.Infrastructure.Services
{
    [TestFixture]
    public class TripService_CreateTrip
    {
        ITripService? tripsService;

        [SetUp]
        public void SetUp()
        {
            tripsService = ServiceResolver.ResolveService<ITripService>();
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

            IConveyOperationResult res = await tripsService!.CreateTripAsync(newTrip);

            return res.IsSuccessful;
        }
    }

    [TestFixture]
    public class TripService_UpdateTrip
    {
        ITripService? tripsService;

        [SetUp]
        public void SetUp()
        {
            tripsService = ServiceResolver.ResolveService<ITripService>();
        }

        [TestCase("Salty Adventure", "Vintage Experience", ExpectedResult = false)]
        [TestCase("Vintage Experience", "Mountain Dew", ExpectedResult = false)]
        [TestCase("Shocking Experience", "Mountain Dew", ExpectedResult = true)]
        [TestCase("Refreshing Walk", null, ExpectedResult = false)]
        public async Task<bool> UpdateTrip(string uniqueName, string outdatedDataName)
        {
            TripDetailsRequestModel newTrip = new()
            {
                Name = uniqueName,
                OutdatedData = new TripRequestModel
                {
                    Name = outdatedDataName
                }
            };

            IConveyOperationResult res = await tripsService!.UpdateTripAsync(newTrip);

            return res.IsSuccessful;
        }
    }

    [TestFixture]
    public class TripService_DeleteTrip
    {
        ITripService? tripsService;

        [SetUp]
        public void SetUp()
        {
            tripsService = ServiceResolver.ResolveService<ITripService>();
        }

        [TestCase("Abcdefghijkl", ExpectedResult = true)]
        [TestCase("Abcdefghijk", ExpectedResult = false)]
        public async Task<bool> DeleteTrip(string uniqueName)
        {
            TripRequestModel newTrip = new()
            {
                Name = uniqueName
            };

            IConveyOperationResult res = await tripsService!.DeleteTripAsync(newTrip);

            return res.IsSuccessful;
        }
    }

    [TestFixture]
    public class TripService_ReisterForTrip
    {
        ITripService? tripsService;

        [SetUp]
        public void SetUp()
        {
            tripsService = ServiceResolver.ResolveService<ITripService>();
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

            IConveyOperationResult res = await tripsService!.RegisterParticipantAsync(participant);

            return res.IsSuccessful;
        }
    }

    [TestFixture]
    public class TripService_GetTrips
    {
        ITripService? tripsService;

        [SetUp]
        public void SetUp()
        {
            tripsService = ServiceResolver.ResolveService<ITripService>();
        }

        [Test]
        public async Task GetTripsAsync()
        {
            IEnumerable<TripResponseModel> resultSet = await tripsService!.GetTripsAsync();

            Assert.That(resultSet is not null);
        }

        [TestCase("Switzerland")]
        [TestCase("Spain")]
        public async Task GetTripsBySearchTermAsync_Exists(string searchTerm)
        {
            IEnumerable<TripResponseModel> resultSet = await tripsService!.GetTripsBySearchTermAsync(searchTerm);

            Assert.That(resultSet.Any());
        }

        [TestCase("Switzerlan")]
        [TestCase("Spai")]
        [TestCase("")]
        public async Task GetTripsBySearchTermAsync_NotExists(string searchTerm)
        {
            IEnumerable<TripResponseModel> resultSet = await tripsService!.GetTripsBySearchTermAsync(searchTerm);

            Assert.That(!resultSet.Any());
        }
    }

    [TestFixture]
    public class TripService_GetTripDetails
    {
        ITripService? tripsService;

        [SetUp]
        public void SetUp()
        {
            tripsService = ServiceResolver.ResolveService<ITripService>();
        }

        [TestCase("Vintage Experience", ExpectedResult = true)]
        [TestCase("Mountain Dew", ExpectedResult = true)]
        [TestCase("", ExpectedResult = false)]
        [TestCase("Sapin", ExpectedResult = false)]
        public async Task<bool> GetTripDetailsAsync(string uniqueNameIdentifier)
        {
            TripDetailsResponseModel? res = await tripsService!.GetTripDetailsAsync(uniqueNameIdentifier);

            return res is not null;
        }
    }
}
