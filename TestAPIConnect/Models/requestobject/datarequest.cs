using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestAPIConnect.Models
{
    public class datarequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public List<string> accountNumbers { get; set; }
        public conditions conditions { get; set; }
        public string AccountNumber { get; set; }
        public externalTransfer externalTransfer { get; set; }
        public depositInfo depositInfo { get; set; }
        public string serviceCode { get; set; }
    }
}