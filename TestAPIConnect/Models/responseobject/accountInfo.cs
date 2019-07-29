using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestAPIConnect.Models
{
    public class accountInfo
    {
        public string accountNum { get; set; }
        public string accountCurrency { get; set; }
        public string accountBalance { get; set; }
        public string accountMaturityDate { get; set; }
        public string accountStatus { get; set; }
        public string accountInterestRate { get; set; }
        public string productCode { get; set; }
        public string productName { get; set; }
    }
}