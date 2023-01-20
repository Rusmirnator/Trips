using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net;
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
        /// <param name="fullCountryName">Optional search term - needs to be provided by whole word.</param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TripResponseModel>))]
        public async Task<IActionResult> GetTripsAsync([FromQuery] string? fullCountryName = null)
        {
            IEnumerable<TripResponseModel> trips =
                string.IsNullOrEmpty(fullCountryName)
                    ? await tripService.GetTripsAsync() : await tripService.GetTripsBySearchTermAsync(fullCountryName);

            return Ok(trips);
        }

        /// <summary>
        /// Gets detailed information about single trip.
        /// </summary>
        /// <param name="uniqueNameIdentifier">Name identifier of the desired trip.</param>
        /// <returns></returns>
        [HttpGet("{uniqueNameIdentifier}/details")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TripDetailsResponseModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetTripDetailsAsync([FromRoute][Required] string uniqueNameIdentifier)
        {
            TripDetailsResponseModel? selectedTrip =
                await tripService.GetTripDetailsAsync(WebUtility.UrlDecode(uniqueNameIdentifier));

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
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(TripDetailsRequestModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateTripAsync([FromBody] TripDetailsRequestModel newData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
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
        /// <param name="uniqueNameIdentifier">Unique identifier of the trip to be updated.</param>
        /// <param name="updatedData">An update request model for trip.</param>
        /// <returns></returns>
        [HttpPut("{uniqueNameIdentifier}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateTripAsync(
            [FromRoute] string uniqueNameIdentifier, [FromBody] TripDetailsRequestModel updatedData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IConveyOperationResult result =
                await tripService.UpdateTripAsync(WebUtility.UrlDecode(uniqueNameIdentifier), updatedData);

            if (!result.IsSuccessful)
            {
                return BadRequest(result.Message);
            }

            return NoContent();
        }
        #endregion

        #region DELETE
        /// <summary>
        /// Deletes desired trip.
        /// </summary>
        /// <param name="uniqueNameIdentifier">Name identifier of trip to be deleted.</param>
        /// <returns></returns>
        [HttpDelete("{uniqueNameIdentifier}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteTripAsync([FromRoute][Required] string uniqueNameIdentifier)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IConveyOperationResult result = await tripService.DeleteTripAsync(WebUtility.UrlDecode(uniqueNameIdentifier));

            if (!result.IsSuccessful)
            {
                return NotFound(result.Message);
            }

            return NoContent();
        }
        #endregion

        #endregion
    }
}
