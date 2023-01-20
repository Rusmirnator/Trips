using Microsoft.AspNetCore.Mvc;
using Trips.Application.Common.Interfaces;
using Trips.Application.Trips.Interfaces;
using Trips.Application.Trips.Models;

namespace Trips.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParticipantsController : ControllerBase
    {
        #region Fields & Properties
        private readonly ITripService tripService;
        #endregion

        #region CTOR
        public ParticipantsController(ITripService tripService)
        {
            this.tripService = tripService;
        }
        #endregion

        #region Request Handlers

        #region POST
        /// <summary>
        /// Registers provided user to provided trip.
        /// </summary>
        /// <param name="participantData">A register request model.</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RegisterForTripAsync([FromBody] ParticipantRequestModel participantData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (participantData.MailAddress!.Contains(' ', StringComparison.Ordinal))
            {
                ModelState.AddModelError(nameof(participantData.MailAddress), "E-mail address cannot contain whitespaces!");

                return BadRequest(ModelState);
            }

            IConveyOperationResult result = await tripService.RegisterParticipantAsync(participantData);

            if (!result.IsSuccessful)
            {
                return BadRequest(result.Message);
            }

            return Ok();
        }
        #endregion

        #endregion
    }
}
