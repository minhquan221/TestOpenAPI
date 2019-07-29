using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestAPIConnect.Models
{
    public class transaction
    {
        public string bankTransactionId { get; set; }
        public string transactionDate { get; set; }
        public decimal exchangeRate { get; set; }
        public decimal transactionFee { get; set; }
        public decimal taxAmount { get; set; }
        public decimal totalDebitedAmount { get; set; }
        public string sourceAccountNumber { get; set; }
        public decimal transactionAmount { get; set; }
        public string narrative { get; set; }
        public string currency { get; set; }
        public decimal balanceAfterOperation { get; set; }
    }
}