namespace Trips.Application.Trips.Models
{
    public class TripDetailsResponseModel
    {
        public string? Name { get; set; }
        public string? Country { get; set; }
        public DateTime? StartDate { get; set; }
        public string? Description { get; set; }
        public int? NumberOfSeats { get; set; }
    }
}
