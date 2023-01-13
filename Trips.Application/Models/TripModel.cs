using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trips.Application.Models
{
    public class TripModel
    {
        public string? Name { get; set; }
        public string? Country { get; set; }
        public DateTime StartDate { get; set; }
    }
}
