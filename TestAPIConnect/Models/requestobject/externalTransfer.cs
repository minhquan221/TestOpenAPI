using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestAPIConnect.Models
{
    public class externalTransfer
    {
        public string sourceAccountNumber { get; set; }
        public long tranferAmount { get; set; }
        public string transferDescription { get; set; }
        public string payeeAccountNumber { get; set; }
        public string payerName { get; set; }
        public string payeeName { get; set; }
        public string payeeBankId { get; set; }
    }
}