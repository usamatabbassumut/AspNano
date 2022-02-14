using AspNano.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNano.Domain.Entities
{
    [Table("Venue")]
    public class VenueEntity: AuditableEntity
    {
        public string VenueName { get; set; } //change to Name
        public string VenueDescription { get; set; } //change to Description (leave off the entity names by convention please)
        public VenueTypeEnum VenueType { get; set; } //same here
        [ForeignKey("Tenant")] 
        public Guid TenantId { get; set; }
        public virtual TenantEntity Tenant { get; set; }
    }
}
