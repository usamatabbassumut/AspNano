using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNano.Application.ViewModels
{
    public class CompanyVM
    {
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string? Contact { get; set; }
        public string? Website { get; set; }
        public int? NumberOfEmployees { get; set; }
    }
}
