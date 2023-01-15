using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Trips.Domain.Entities
{
    [Table("Trips")]
    public class Trip
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(50)]
        [Required(AllowEmptyStrings = false)]
        public string? Name { get; set; }
        [MaxLength(20)]
        public string? Country { get; set; }
        public string? Description { get; set; }
        public DateTime? StartDate { get; set; }
        public int? NumberOfSeats { get; set; }
        public virtual ICollection<TripParticipant> TripParticipants { get; set; }

        public Trip()
        {
            TripParticipants = new HashSet<TripParticipant>();
        }
    }
}