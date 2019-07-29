using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestAPIConnect.Models
{
    public class billItem
    {
        public string OrderId { get; set; }
        public string description { get; set; }
        public string amount { get; set; }
        public string fromDate { get; set; }
        public string toDate { get; set; }
        public string billCodeItemNo { get; set; }
        
    }
}