using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNano.Domain.Entities
{
    [Table("Tenant")]
    public class TenantEntity : BaseEntity
    {

        public string Key { get; set; }
        public bool IsActive { get; set; }

        //Is Active bool

    }
}
