using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestAPIConnect.Models
{
    public class dataresponsedata<T> where T: class
    {
        public string tokenKey { get; set; }
        public T transactions { get; set; }
        public transaction transaction { get; set; }
        public List<objaccount> accounts { get; set; }
        public accountList accountList { get; set; }
        public string depositAcctNumber { get; set; }
        public serviceProviderList serviceProviderList { get; set; }
        public serviceList serviceList { get; set; }
        public billInfo billInfo { get; set; }
        public branchDetailList branchDetailList { get; set; }
        public string status { get; set; }
        public bankATMList bankATMList { get; set; }
    }
}