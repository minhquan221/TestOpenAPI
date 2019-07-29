using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestAPIConnect.Models
{
    public class branchDetail
    {

      //  "branchDetail": [
      //  {
      //    "branchCode": "giejuk",
      //    "branchName": "Gabriel Hammond",
      //    "province": "MB",
      //    "phoneContact": "(833) 914-2293",
      //    "operatingHours": "20",
      //    "branchAddress": {
      //      "address1": "1225 Imtuf Key",
      //      "address2": "958 Adito Parkway",
      //      "city": "Pimfivim",
      //      "addressCountry": "1777 Wejar Pike",
      //      "addressCountryCode": "1809 Zucuje Drive",
      //      "apartmentNumber": "4311760901242880",
      //      "postCode": "T0X 8F0"
      //    },
      //    "gpsCoordinates": {
      //      "longitude": "27.56568",
      //      "latitude": "52.94117"
      //    }
      //  }
      //]
        public string branchCode { get; set; }
        public string branchName { get; set; }
        public string province { get; set; }
        public string phoneContact { get; set; }
        public string operatingHours { get; set; }
        public Address branchAddress { get; set; }
        public Coordinates gpsCoordinates { get; set; }
    }
}