using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestAPIConnect.Models
{
    public class dataresponsedataBillInfoResponse
    {
        public billInfoResponse billInfo { get; set; }
        public List<billItem> billItem { get; set; }

    }
}