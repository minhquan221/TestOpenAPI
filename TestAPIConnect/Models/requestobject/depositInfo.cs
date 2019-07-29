using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestAPIConnect.Models
{
    public class depositInfo
    {
        public long accountOpeningAmount { get; set; }
        public string srcAccountNum { get; set; }
        public string payoutAccount { get; set; }
        public string accountAutoCapitalization { get; set; }
        public string accountAutoRollover { get; set; }
        public int accountPeriodCount { get; set; }
        public string accountPeriodUnit { get; set; }
    }
}