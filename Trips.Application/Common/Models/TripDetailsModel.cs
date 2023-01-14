namespace Trips.Application.Common.Models
{
    public class TripDetailsModel : TripModel
    {
        public string? Description { get; set; }
        public int? NumberOfSeats { get; set; }
    }
}
