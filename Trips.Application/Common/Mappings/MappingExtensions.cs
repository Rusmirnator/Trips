using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trips.Application.Common.Models;
using Trips.Domain.Entities;

namespace Trips.Application.Common.Mappings
{
    public static class MappingExtensions
    {
        public static TripModel? ToModel(this Trip entity)
        {
            if (entity is null)
            {
                return null;
            }

            return new TripModel
            {
                Name = entity.Name,
                Country = entity.Country,
                StartDate = entity.StartDate
            };
        }

        public static TripDetailsModel? ToDetailedModel(this Trip entity)
        {
            if (entity is null)
            {
                return null;
            }

            return new TripDetailsModel
            {
                Name = entity.Name,
                Country = entity.Country,
                StartDate = entity.StartDate,
                Description = entity.Description,
                NumberOfSeats = entity.NumberOfSeats
            };
        }
    }
}
