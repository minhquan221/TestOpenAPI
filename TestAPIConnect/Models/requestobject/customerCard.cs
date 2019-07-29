using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace TestAPIConnect.Models
{
    public class customerCard
    {
        public string fullname { get; set; }
        public string birthday { get; set; }
        public string maritalStatus { get; set; }
        public string nationality { get; set; }
        public string branchCode { get; set; }
        public legal legal { get; set; }
        public contactCustomerInfo contactInfo { get; set; }
        public product product { get; set; }
    }
}