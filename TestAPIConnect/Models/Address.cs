using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestAPIConnect.Models
{
    public class Address
    {
        public string address1 { get; set; }
        public string address2 { get; set; }
        public string city { get; set; }
        public string addressCountry { get; set; }
        public string addressCountryCode { get; set; }
        public string apartmentNumber { get; set; }
        public string postCode { get; set; }
    }
}