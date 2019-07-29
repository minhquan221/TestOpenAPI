using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestAPIConnect.Models
{
    public class fastTransfer
    {
        public string sourceAccountNumber { get; set; }
        public string payeeType { get; set; }
        public long tranferAmount { get; set; }
        public string transferDescription { get; set; }
        public string payeeAccountNumber { get; set; }
        public string payeeCardNumber { get; set; }
        public string bankCode { get; set; }
    }
}