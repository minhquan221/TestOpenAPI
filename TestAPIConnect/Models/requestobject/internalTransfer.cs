using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestAPIConnect.Models
{
    public class internalTransfer
    {
        public string fromAccountNumber { get; set; }
        public long tranferAmount { get; set; }
        public string transferDescription { get; set; }
        public string toAccountNumber { get; set; }
    }
}