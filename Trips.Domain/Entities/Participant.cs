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
        [ForeignKey(nameof(TripId))]
        [InverseProperty(nameof(Entities.Trip.Participants))]
        public virtual Trip? Trip { get; set; }
    }
}