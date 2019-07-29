using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestAPIConnect.Models
{
    public class billInfo
    {
        public string billCode { get; set; }
        public string billType { get; set; }
        public string customerName { get; set; }
        public string customerCode { get; set; }
        public string address { get; set; }
        public string phoneNumber { get; set; }
        public string meterNumber { get; set; }
        public string billPaymentType { get; set; }
        public string billSourceData { get; set; }
        public serviceProvider serviceProvider { get; set; }
        public string providerCode { get; set; }
        public string serviceCode { get; set; }
    }
}