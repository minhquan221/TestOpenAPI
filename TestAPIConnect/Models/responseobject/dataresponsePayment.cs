using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestAPIConnect.Models
{
    public class dataresponsePayment
    {
        public string tokenKey { get; set; }
        public objtransactions transactions { get; set; }
        public List<objaccount> accounts { get; set; }
    }
}