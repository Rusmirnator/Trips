using System.ComponentModel.DataAnnotations.Schema;

namespace Trips.Domain.Entities
{
    [Table("TripParticipants")]
    public class TripParticipant
    {
        public int TripId { get; set; }
        public int ParticipantId { get; set; }
        public Trip? Trip { get; set; }
        public Participant? Participant { get; set; }
    }
}
