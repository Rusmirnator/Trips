using Microsoft.AspNetCore.Mvc;
using Trips.Application.Common.Interfaces;
using Trips.Application.Trips.Interfaces;
using Trips.Application.Trips.Models;

namespace Trips.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TripsController : ControllerBase
    {
        #region Fields & Properties
        private readonly ITripService tripService;
        #endregion

        #region CTOR
        public TripsController(ITripService tripService)
        {
            this.tripService = tripService;
        }
        #endregion

        #region Request Handlers

        #region GET
        [HttpGet]
        [Route("trips")]
        public async Task<IActionResult> GetTripsAsync([FromQuery] string? searchTerm = null)
        {
            IEnumerable<TripResponseModel> trips =
                string.IsNullOrEmpty(searchTerm)
                    ? await tripService.GetTripsAsync() : await tripService.GetTripsBySearchTermAsync(searchTerm);

            return Ok(trips);
        }

        [HttpGet]
        [Route("trips/details")]
        public async Task<IActionResult> GetTripDetailsAsync(
            [FromQuery] string uniqueNameIdentifier)
        {
            TripDetailsResponseModel? selectedTrip = await tripService.GetTripDetailsAsync(uniqueNameIdentifier);

            if (selectedTrip is null)
            {
                return NotFound();
            }

            return Ok(selectedTrip);
        }
        #endregion

        #region POST
        [HttpPost]
        [Route("trips")]
        public async Task<IActionResult> CreateTripAsync(
            [FromBody] TripDetailsRequestModel newData)
        {
            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }

            IConveyOperationResult result = await tripService.CreateTripAsync(newData);

            if (!result.IsSuccessful)
            {
                return BadRequest(result.Message);
            }

            return CreatedAtAction(nameof(CreateTripAsync), newData);
        }
        #endregion

        #region PUT
        [HttpPut]
        [Route("trips")]
        public async Task<IActionResult> UpdateTripAsync([FromBody] TripDetailsRequestModel updatedData)
        {
            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }

            IConveyOperationResult result = await tripService.UpdateTripAsync(updatedData);

            if (!result.IsSuccessful)
            {
                return BadRequest(result.Message);
            }

            return AcceptedAtAction(nameof(UpdateTripAsync), updatedData);
        }
        #endregion

        #region PATCH
        [HttpPatch]
        [Route("trips")]
        public async Task<IActionResult> RegisterForTripAsync([FromBody] ParticipantRequestModel patchData)
        {
            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }

            IConveyOperationResult result = await tripService.RegisterParticipantAsync(patchData);

            if (!result.IsSuccessful)
            {
                return BadRequest(result.Message);
            }

            return AcceptedAtAction(nameof(RegisterForTripAsync), patchData);
        }
        #endregion

        #region DELETE
        [HttpDelete]
        [Route("trips")]
        public async Task<IActionResult> DeleteTripAsync([FromBody] TripRequestModel deletedData)
        {
            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }

            IConveyOperationResult result = await tripService.DeleteTripAsync(deletedData);

            if (!result.IsSuccessful)
            {
                return BadRequest(result.Message);
            }

            return AcceptedAtAction(nameof(DeleteTripAsync), deletedData);
        }
        #endregion

        #endregion
    }
}
