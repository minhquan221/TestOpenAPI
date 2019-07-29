using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Pkcs;
using JWTLibrary.Helper;

namespace JWTLibrary
{
    public class JWTHelper
    {
        //public string PrivateKey { get; set; }
        //public string PublicKey { get; set; }
        //public KeySize KeySize { get; set; }

        private static JWTHelper _current = new JWTHelper();

        public static JWTHelper Current
        {
            get
            {
                return _current != null ? _current : new JWTHelper();
            }
        }

        public string Sign<T>(T payload, string privateKey, Algorithm alg, KeySize size) where T: new()
        {
            List<string> segments = new List<string>();
            JwtHeader header = new JwtHeader().Set(alg, size);

            DateTime issued = DateTime.Now;
            DateTime expire = DateTime.Now.AddHours(10);

            byte[] headerBytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(header, Formatting.None));
            byte[] payloadBytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(payload));

            segments.Add(Helper.Base64Helper.UrlEncode(headerBytes));
            segments.Add(Helper.Base64Helper.UrlEncode(payloadBytes));

            string stringToSign = string.Join(".", segments.ToArray());

            byte[] bytesToSign = Encoding.UTF8.GetBytes(stringToSign);

            byte[] keyBytes = Convert.FromBase64String(privateKey);

            var privKeyObj = Asn1Object.FromByteArray(keyBytes);
            var privStruct = RsaPrivateKeyStructure.GetInstance((Asn1Sequence)privKeyObj);

            ISigner sig = SignerUtilities.GetSigner(GetSignerName(size));

            sig.Init(true, new RsaKeyParameters(true, privStruct.Modulus, privStruct.PrivateExponent));

            sig.BlockUpdate(bytesToSign, 0, bytesToSign.Length);
            byte[] signature = sig.GenerateSignature();

            segments.Add(Helper.Base64Helper.UrlEncode(signature));
            return string.Join(".", segments.ToArray());
        }

        public bool Validate(string token, string PublicKey, KeySize size)
        {
            string[] parts = token.Split('.');
            string header = parts[0];
            string payload = parts[1];
            string signature = parts[2];

            byte[] crypto = Base64Helper.UrlDecode(parts[2]);

            //string headerJson = Encoding.UTF8.GetString(Helper.Base64Helper.UrlDecode(header));
            string payloadJson = Encoding.UTF8.GetString(Helper.Base64Helper.UrlDecode(payload));


            byte[] keyBytes = Convert.FromBase64String(PublicKey);

            AsymmetricKeyParameter asymmetricKeyParameter = PublicKeyFactory.CreateKey(keyBytes);
            RsaKeyParameters rsaKeyParameters = (RsaKeyParameters)asymmetricKeyParameter;
            RSAParameters rsaParameters = new RSAParameters
            {
                Modulus = rsaKeyParameters.Modulus.ToByteArrayUnsigned(),
                Exponent = rsaKeyParameters.Exponent.ToByteArrayUnsigned()
            };
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.ImportParameters(rsaParameters);

            byte[] hash = ComputeHash(header, payload, size);

            RSAPKCS1SignatureDeformatter rsaDeformatter = new RSAPKCS1SignatureDeformatter(rsa);
            rsaDeformatter.SetHashAlgorithm(GetAlgorithmName(size));
            byte[] tmp = Helper.Base64Helper.UrlDecode(signature);
            if (!rsaDeformatter.VerifySignature(hash, tmp))
            {
                return false;
                //throw new Exception("Invalid signature");
            }
            return true;
            //return payloadJson;
        }

        public T Decode<T>(string token, string PublicKey, KeySize size) where T: new()
        {
            string[] parts = token.Split('.');
            string header = parts[0];
            string payload = parts[1];
            string signature = parts[2];

            byte[] crypto = Base64Helper.UrlDecode(parts[2]);

            //string headerJson = Encoding.UTF8.GetString(Helper.Base64Helper.UrlDecode(header));
            string payloadJson = Encoding.UTF8.GetString(Helper.Base64Helper.UrlDecode(payload));

            return JsonConvert.DeserializeObject<T>(payloadJson);
        }

        private string GetSignerName(KeySize size)
        {
            return "SHA" + (int)size + "withRSA";
        }

        private string GetAlgorithmName(KeySize size)
        {
            return "SHA" + (int)size;
        }

        private byte[] ComputeHash(string header, string payload, KeySize size)
        {
            HashAlgorithm sha = HashAlgorithm.Create(GetAlgorithmName(size));

            if (sha == null) throw new Exception("Given key size is not valid.");

            return sha.ComputeHash(Encoding.UTF8.GetBytes(header + '.' + payload));
        }
    }
}
