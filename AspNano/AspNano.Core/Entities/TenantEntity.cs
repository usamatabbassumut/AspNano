using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNano.Core.Entities
{
    [Table("Tenant")]
    public class TenantEntity: AuditableEntity
    {
        public string Key { get; set; }

    }
}
