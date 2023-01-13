using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trips.Application.Models
{
    public class TripDetailsModel : TripModel
    {
        public string? Description { get; set; }
        public int NumberOfSeats { get; set; }
    }
}
