using AspNano.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNano.DTOs.VenueDTOs
{
    public class VenueDTO
    {
        public Guid Id { get; set; }
        public string VenueName { get; set; }
        public string VenueDescription { get; set; }
        public string VenueType { get; set; } //?? how do you handle ENUMS? not sure
    }
}



//EXAMPLE PAGINATED RESPONSE WRAPPER

//{
//    "data": [
//        {
//        "id": "e0270000-7a28-e4b9-4984-08d9bb36ff09",
//            "name": "Razor",
//            "description": "Sample Data"
//        },
//        {
//        "id": "e0270000-7a28-e4b9-622b-08d9bb36ff09",
//            "name": "MSI",
//            "description": "Laptops"
//        },
//        {
//        "id": "e0270000-7a28-e4b9-07aa-08d9bffdafba",
//            "name": "Asus",
//            "description": "Computers"
//        },
//        {
//        "id": "e0270000-7a28-e4b9-6ce6-08d9bffe3063",
//            "name": "NVidia",
//            "description": "Graphics Cards"
//        },      
//    ],
//    "currentPage": 1,
//    "totalPages": 2,
//    "totalCount": 11,
//    "pageSize": 10,
//    "hasPreviousPage": false,
//    "hasNextPage": true,
//    "messages": null,
//    "succeeded": true
//}