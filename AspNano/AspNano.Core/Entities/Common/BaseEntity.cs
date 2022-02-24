using AspNano.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNano.Domain.Entities
{
    public abstract class BaseEntity
    {
        public Guid Id { get; set; }
    }

}
