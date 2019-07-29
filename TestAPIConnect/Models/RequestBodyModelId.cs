using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestAPIConnect.Models
{
    public class RequestBodyModelId<T>: RequestBodyModel<T> where T: class
    {
        public string id { get; set; }
    }
}