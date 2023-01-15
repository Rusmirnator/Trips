using System.ComponentModel.DataAnnotations;

namespace Trips.Application.Trips.Models
{
    public class TripRequestModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Trip name cannot be empty!")]
        public string? Name { get; set; }
    }
}
