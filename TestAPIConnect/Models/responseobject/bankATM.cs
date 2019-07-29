using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestAPIConnect.Models
{
    public class bankATM
    {
        public string atmCode { get; set; }
        public string atmName { get; set; }
        public Address atmAddress { get; set; }
        public Coordinates gpsCoordinates { get; set; }
    }
}