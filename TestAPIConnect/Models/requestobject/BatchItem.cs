using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestAPIConnect.Models
{
    public class BatchItem
    {
        public string payeeAccountNumber { get; set; }
        public string payeeName { get; set; }
        public long amount { get; set; }
        public string paymentDescription { get; set; }

    }
}