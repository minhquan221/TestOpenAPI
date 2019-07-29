using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestAPIConnect.Models
{
    public class transactionpaymentResponse
    {
        public string bankTransactionId { get; set; }
        public string transactionDate { get; set; }
        public string exchangeRate { get; set; }
        public string transactionFee { get; set; }
        public string taxAmount { get; set; }
        public string totalDebitedAmount { get; set; }

    }
}