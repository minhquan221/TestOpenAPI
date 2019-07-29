using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestAPIConnect.Models
{
    public class billPaymentInfo
    {
        public string debitAccount { get; set; }
        public long amount { get; set; }
        public string serviceCode { get; set; }
        public string serviceProviderCode { get; set; }
        public string billCode { get; set; }
        public string[] billCodeItemNo { get; set; }
    }
}