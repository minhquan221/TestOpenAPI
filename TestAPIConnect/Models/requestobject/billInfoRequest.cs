using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestAPIConnect.Models
{
    public class billInfoRequest
    {
        public string billCode { get; set; }
        public string providerCode { get; set; }
        public string serviceCode { get; set; }
    }
}