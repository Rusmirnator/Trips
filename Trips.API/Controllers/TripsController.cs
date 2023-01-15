using Microsoft.AspNetCore.Mvc;
using Trips.Application.Trips.Interfaces;
using Trips.Application.Trips.Models;

namespace Trips.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TripsController : ControllerBase
    {
        private readonly ITripService tripService;

        public TripsController(ITripService tripService)
        {
            this.tripService = tripService;
        }

        [HttpGet]
        [Route("trips")]
        public async Task<ActionResult<IEnumerable<TripResponseModel>>> GetTripsAsync()
        {
            IEnumerable<TripResponseModel> trips = await tripService.GetTripsAsync();

            return Ok(trips);
        }
    }
}
