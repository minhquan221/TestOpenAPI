using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestAPIConnect.Models
{
    public class account
    {
        public string accountNumber { get; set; }
        public string accountShortName { get; set; }
        public string productType { get; set; }
        public string currency { get; set; }
        public string balance { get; set; }
        public string availableBalance { get; set; }
        public string lockedAmount { get; set; }
        public string postingRestriction { get; set; }
        public string interestRate { get; set; }
        public string lastTransactionBookingDate { get; set; }
    }
}