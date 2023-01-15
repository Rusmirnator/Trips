using System.ComponentModel.DataAnnotations;

namespace Trips.Application.Trips.Models
{
    public class ParticipantRequestModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "E-mail address cannot be empty!")]
        [EmailAddress(ErrorMessage = "Provided e-mail address is invalid!")]
        public string? MailAddress { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Trip name cannot be empty!")]
        public string? TripName { get; set; }
    }
}
