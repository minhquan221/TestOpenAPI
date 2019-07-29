using System;
using System.Collections.Generic;
using System.Text;

namespace JWTLibrary
{
    public class JwtHeader
    {
        public string alg { get; set; }
        public string typ { get; set; }

        public JwtHeader Set(Helper.Algorithm algorithm, Helper.KeySize size)
        {
            if(!Enum.IsDefined(typeof(Helper.Algorithm), algorithm) || !Enum.IsDefined(typeof(Helper.KeySize), size))
            {
                throw new Exception("Invalid values for algorithm or size.");
            }

            string algstr = "";

            switch(algorithm)
            {
                case Helper.Algorithm.RSA:
                    algstr = "RS";
                    break;
                case Helper.Algorithm.HMAC:
                    algstr = "HS";
                    break;
                case Helper.Algorithm.ECDSA:
                    algstr = "ES";
                    break;
                case Helper.Algorithm.RSASSA:
                    if(size == Helper.KeySize.S512)
                    {
                        throw new Exception("Invalid size for algorithm.");
                    }
                    algstr = "PS";
                    break;
            }

            alg = algstr + ((int)size).ToString();
            typ = "JWT";
            return new JwtHeader {
                alg = alg,
                typ = typ
            };
        }
    }
}
