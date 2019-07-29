using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestAPIConnect.Models
{
    public class error
    {
        public string type { get; set; }
        public string code { get; set; }
        public string details { get; set; }
        public string location { get; set; }
    }
}