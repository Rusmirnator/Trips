using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Trips.Domain.Entities
{
    [Table("Participants")]
    public class Participant
    {
        [Key]
        public int Id { get; set; }
        public string? MailAddress { get; set; }
        public int TripId { get; set; }
        public virtual ICollection<TripParticipant> TripParticipants { get; set; }

        public Participant()
        {
            TripParticipants = new HashSet<TripParticipant>();
        }
    }
}