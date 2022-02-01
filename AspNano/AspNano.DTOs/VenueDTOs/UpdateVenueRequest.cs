using AspNano.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNano.DTOs.VenueDTOs
{
    public class UpdateVenueRequest
    {

        public string VenueName { get; set; }
        public string VenueDescription { get; set; }
        public VenueTypeEnum VenueType { get; set; }
   
    }
}

//EXAMPLE SUCCESS WRAPPER Response

//{
//    "data": "e0270000-7a28-e4b9-4984-08d9bb36ff09",
//    "messages": [],
//    "succeeded": true
//}