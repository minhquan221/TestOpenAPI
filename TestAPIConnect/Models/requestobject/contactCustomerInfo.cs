using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace TestAPIConnect.Models
{
    public class contactCustomerInfo
    {
    //"customer": {
    //  "fullname": "Nguyen van A",
    //  "birthday": "20100722",
    //  "maritalStatus": "1",
    //  "nationality": "Viet Nam",
    //  "branchCode": "VN0010001",
    //  "legal": {
    //    "legalNumber": "025472915",
    //    "lssuedDate": "20120722",
    //    "lssuedPlace": "HCM"
    //  },
    //  "contactInfo": {
    //    "contactAddress": {
    //      "address1": "363/38/7 đất mới",
    //      "address2": "",
    //      "address3": "",
    //      "ward": "Bình trị đông a ward",
    //      "district": "Quận 99",
    //      "city": "Hồ Chí Minh"
    //    },
    //    "permanentAddress": {
    //      "address1": "9c1/c kp 3",
    //      "address2": "",
    //      "address3": "",
    //      "ward": "phường An Phú Đông",
    //      "district": "Quận 12",
    //      "city": "Hồ Chí Minh"
    //    },
    //    "email": "hieunm1@ocb.com.vn",
    //    "mobile": "0919633139"
    //  },
    //  "product": {
    //    "card": {
    //      "cardType": "GOLD"
    //    }
    //  }
    //}
        public contactAddress contactAddress { get; set; }
        public contactAddress permanentAddress { get; set; }
        public string[] email { get; set; }
        public string[] mobile { get; set; }
    }
}