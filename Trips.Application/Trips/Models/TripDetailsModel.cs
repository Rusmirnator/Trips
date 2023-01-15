namespace Trips.Application.Trips.Models
{
    public class TripDetailsModel : TripModel
    {
        public string? Description { get; set; }
        public int? NumberOfSeats { get; set; }
    }
}
