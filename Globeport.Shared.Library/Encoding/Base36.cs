using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace Globeport.Shared.Library.Encoding
{
    public static class Base36
    {
        private const string Digits = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        public static long FromBase36(string value)
        {
            if (value == null)
                throw new ArgumentNullException("value");

            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("An empty string was passed.");

            value = value.ToUpperInvariant();
            bool negative = value[0] == '-';

            if (negative)
                value = value.Substring(1, value.Length - 1);

            if (value.ToCharArray().Any(c => !Digits.ToCharArray().Contains(c)))
                throw new ArgumentException("Invalid value: \"" + value + "\".");

            var decoded = 0L;
            for (var i = 0; i < value.Length; ++i)
                decoded += Digits.IndexOf(value[i]) * (long)BigInteger.Pow(Digits.Length, value.Length - i - 1);

            return negative ? decoded * -1 : decoded;
        }

        public static string ToBase36(long value)
        {
            // hard coded value due to "Negating the minimum value of a twos complement number is invalid."
            if (value == long.MinValue)
                return "-1Y2P0IJ32E8E8";

            var negative = value < 0;
            value = Math.Abs(value);
            var encoded = string.Empty;

            do
                encoded = Digits[(int)(value % Digits.Length)] + encoded;
            while ((value /= Digits.Length) != 0);

            return negative ? "-" + encoded : encoded;
        }
    }
}
