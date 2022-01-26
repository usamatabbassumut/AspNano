using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNano.Core.Entities
{
    public class Tenant: AuditableEntity
    {
        public string Key { get; set; }

    }
}
