using AspNano.Domain.Entities.Common;
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
    public class VenueEntity: AuditableEntity,IMustHaveTenant
    {
        public string Name { get; set; } //change to Name -> done
        public string Description { get; set; } //change to Description (leave off the entity names by convention please) -> done
        public VenueTypeEnum Type { get; set; } //same here 
        //[ForeignKey("Tenant")] 
        //public Guid TenantId { get; set; }
        //public virtual TenantEntity Tenant { get; set; }
        public string TenantId { get; set; } //-> removed foreign key relation

    }
}
