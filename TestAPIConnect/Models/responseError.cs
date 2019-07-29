using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestAPIConnect.Models
{
    public class responseError<T> where T: class
    {
        public string httpCode { get; set; }
        public string httpMessage { get; set; }
        public string moreInformation { get; set; }
        public T error { get; set; }
        public string error_description { get; set; }
    }
}