﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestAPIConnect.Models
{
    public class externalTransferViewModel: externalTransfer
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string Signature { get; set; }
        public string PrivateKey { get; set; }
        public string Certificate { get; set; }
    }
}