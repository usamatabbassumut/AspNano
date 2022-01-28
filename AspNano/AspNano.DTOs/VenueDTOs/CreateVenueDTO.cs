using AspNano.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNano.DTOs.VenueDTOs
{
    public class CreateVenueDTO
    {
        public string VenueName { get; set; }
        public string VenueDescription { get; set; }
        public VenueTypeEnum VenueType { get; set; }
        public Guid TenantId { get; set; }
    }
}
