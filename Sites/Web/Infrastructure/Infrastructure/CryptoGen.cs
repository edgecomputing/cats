using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Cats.Web.Hub.Infrastructure
{
    public class CryptoGen
    {
        public String CreateKey(int numBytes)
        {
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] buff = new byte[numBytes];
            rng.GetBytes(buff);
            return BytesToHexString(buff);
        }

        private String BytesToHexString(byte[] bytes)
        {
            StringBuilder hexString = new StringBuilder(64);
            for (int counter = 0; counter < bytes.Length; counter++)
            {
                hexString.Append(String.Format("{0:X2}", bytes[counter]));
            }
            return hexString.ToString();
        }
    }
}