namespace Trips.Application.Trips.Models
{
    public class TripDetailsRequestModel
    {
        public string? Name { get; set; }
        public string? Country { get; set; }
        public DateTime? StartDate { get; set; }
        public string? Description { get; set; }
        public int? NumberOfSeats { get; set; }
        public TripRequestModel? Origin { get; set; }
    }
}
