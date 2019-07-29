using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace TestAPIConnect.Models
{
    public class customerProduct:customer
    {
        public product product { get; set; }

    }
}