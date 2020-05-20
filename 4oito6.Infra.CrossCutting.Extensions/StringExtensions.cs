using System;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;

namespace _4oito6.Infra.CrossCutting.Extensions
{
    public static class StringExtensions
    {
        public static string ToHash(this string text)
        {
            UnicodeEncoding encoding = new UnicodeEncoding();
            byte[] hashBytes;
            using (HashAlgorithm hash = SHA1.Create())
            {
                hashBytes = hash.ComputeHash(encoding.GetBytes(text));
            }

            StringBuilder hashValue = new StringBuilder(hashBytes.Length * 2);
            foreach (byte b in hashBytes)
            {
                hashValue.AppendFormat(CultureInfo.InvariantCulture, "{0:X2}", b);
            }

            return hashValue.ToString();
        }

        public static bool IsNumeric(this string text) => float.TryParse(text, out float number);

        public static int ToInt(this string text) => text.IsNumeric() ? Convert.ToInt32(text) : 0;

        public static long ToLong(this string text) => text.IsNumeric() ? long.Parse(text) : 0;
    }
}
