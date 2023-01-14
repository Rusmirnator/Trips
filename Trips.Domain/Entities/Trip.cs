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
        [Required(AllowEmptyStrings = false, ErrorMessage = "Trip name cannot be empty!")]
        public string? Name { get; set; }
        [MaxLength(20)]
        public string? Country { get; set; }
        public string? Description { get; set; }
        public DateTime? StartDate { get; set; }
        [Range(0,100, ErrorMessage = "Number of seats must have a value between 0 and 100!")]
        public int? NumberOfSeats { get; set; }
        [InverseProperty(nameof(Participant.Trip))]
        public virtual ICollection<Participant>? Participants { get; set; }

        public Trip()
        {
            Participants = new HashSet<Participant>();
        }
    }
}