using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
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
        /// <summary>
        /// Get all trips, or filtered if parameter is provided.
        /// </summary>
        /// <param name="searchTerm">Optional search term - needs to be provided by whole word.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetTripsAsync([FromQuery] string? searchTerm = null)
        {
            IEnumerable<TripResponseModel> trips =
                string.IsNullOrEmpty(searchTerm)
                    ? await tripService.GetTripsAsync() : await tripService.GetTripsBySearchTermAsync(searchTerm);

            return Ok(trips);
        }

        /// <summary>
        /// Gets detailed information about single trip.
        /// </summary>
        /// <param name="uniqueNameIdentifier">Name identifier of the desired trip.</param>
        /// <returns></returns>
        [HttpGet("detail")]
        public async Task<IActionResult> GetTripDetailsAsync([FromQuery] string uniqueNameIdentifier)
        {
            TripDetailsResponseModel? selectedTrip = await tripService.GetTripDetailsAsync(uniqueNameIdentifier);

            if (selectedTrip is null)
            {
                return NotFound(uniqueNameIdentifier);
            }

            return Ok(selectedTrip);
        }
        #endregion

        #region POST
        /// <summary>
        /// Creates new trip.
        /// </summary>
        /// <param name="newData">A create request model for trip.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateTripAsync([FromBody] TripDetailsRequestModel newData)
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
        /// <summary>
        /// Updates provided detailed trip data.
        /// </summary>
        /// <param name="updatedData">An update request model for trip.</param>
        /// <returns></returns>
        [HttpPut]
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
        /// <summary>
        /// Registers provided user to provided trip.
        /// </summary>
        /// <param name="patchData">A register request model</param>
        /// <returns></returns>
        [HttpPatch]
        public async Task<IActionResult> RegisterForTripAsync([FromBody] ParticipantRequestModel patchData)
        {
            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }
            /// seems better to place it here than in service
            if (patchData.MailAddress!.Contains(' ', StringComparison.Ordinal))
            {
                ModelState.AddModelError(nameof(patchData.MailAddress), "E-mail address cannot contain whitespaces!");

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
        /// <summary>
        /// Deletes desired trip.
        /// </summary>
        /// <param name="uniqueNameIdentifier">Name identifier of trip to be deleted.</param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> DeleteTripAsync([FromQuery][Required] string uniqueNameIdentifier)
        {
            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }

            IConveyOperationResult result = await tripService.DeleteTripAsync(uniqueNameIdentifier);

            if (!result.IsSuccessful)
            {
                return NotFound(result.Message);
            }

            return Accepted();
        }
        #endregion

        #endregion
    }
}
