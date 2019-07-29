using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestAPIConnect.Models
{
    public class serviceProvider
    {
        public string providerCode { get; set; }
        public string providerName { get; set; }
        public service service { get; set; }
    }
}