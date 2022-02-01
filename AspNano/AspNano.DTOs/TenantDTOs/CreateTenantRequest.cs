using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNano.DTOs.TenantDTOs
{
    public class CreateTenantRequest
    {
        public string Key { get; set; }
        public string AdminEmail { get; set; }
        public string Password { get; set; }

    }
}
