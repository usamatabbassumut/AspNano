using AspNano.Core.Entities;
using AspNano.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNano.Entities.Entities
{
    [Table("Venue")]
    public class VenueEntity: AuditableEntity
    {
        public string VenueName { get; set; }
        public string VenueDescription { get; set; }
        public VenueTypeEnum VenueType { get; set; }
        [ForeignKey("Tenant")] 
        public Guid TenantId { get; set; }
        public virtual TenantEntity Tenant { get; set; }
    }
}
