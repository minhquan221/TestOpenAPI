using System;
using System.Collections.Generic;
using System.Text;

namespace JWTLibrary.Helper
{
    public enum Algorithm
    {
        HMAC = 1,
        RSA = 2,
        ECDSA = 3,
        RSASSA = 4
    }
}
