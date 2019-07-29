using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestAPIConnect.Models
{
    public class transactionResponse
    {
        public string bankTransactionId { get; set; }
        public string transactionDate { get; set; }
        public string sourceAccountNumber { get; set; }
        public string transactionAmount { get; set; }
        public string narrative { get; set; }
        public string currency { get; set; }
        public string balanceAfterOperation { get; set; }

    }
}