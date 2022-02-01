using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNano.DTOs.IdentityDTOs
{
    public class UpdateProfileRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        //public string PhoneNumber { get; set; }
        public string Email { get; set; }

        public bool IsActive { get; set; }

        //Image - at some point (cloudinary)
    }
}
