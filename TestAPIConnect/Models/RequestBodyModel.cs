﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestAPIConnect.Models
{
    public class RequestBodyModel<T> where T : class
    {
        public trace trace { get; set; }
        public T data { get; set; }
    }
}