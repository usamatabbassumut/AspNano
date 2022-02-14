using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNano.DTOs.AuthDTOs
{
    public class TokenResponse
    {
        public string Token { get; set; }
        public string User { get; set; }
        public DateTime Expiration { get; set; }
        public string Id { get; set; }

  
    }
}
