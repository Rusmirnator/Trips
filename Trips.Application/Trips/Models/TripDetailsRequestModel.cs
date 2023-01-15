using System.ComponentModel.DataAnnotations;

namespace Trips.Application.Trips.Models
{
    public class TripDetailsRequestModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Name cannot be empty!")]
        [StringLength(50, ErrorMessage = "Name cannot be longer than 50 letters!")]
        public string? Name { get; set; }
        [StringLength(20, ErrorMessage = "Country cannot be longer than 20 letters!")]
        public string? Country { get; set; }
        public DateTime? StartDate { get; set; }
        public string? Description { get; set; }
        [Range(0, 100, ErrorMessage = "Number of seats must have a value between 0 and 100!")]
        public int? NumberOfSeats { get; set; }
        public TripRequestModel? Origin { get; set; }
    }
}
