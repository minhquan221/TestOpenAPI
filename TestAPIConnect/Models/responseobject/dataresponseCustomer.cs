using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestAPIConnect.Models
{
    public class dataresponseCustomer
    {
        public customerInfo customerInfo { get; set; }
        public string branch { get; set; }
        public IDInfo IDInfo { get; set; }
        public contactInfo contactInfo { get; set; }
        public corporateInfo corporateInfo { get; set; }
        public string shortName { get; set; }

    }
}