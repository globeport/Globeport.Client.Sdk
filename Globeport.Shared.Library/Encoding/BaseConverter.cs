using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Globeport.Shared.Library.Encoding
{
    public static class BaseConverter
    {
        const string Digits = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        /// <summary>
        ///     The character set for encoding.
        /// </summary>
        public static string CharList { get; set; }

        /// <summary>
        ///     Convert a <paramref name="number" /> (expressed as a string) from <paramref name="fromBase" /> to
        ///     <paramref name="toBase" />
        /// </summary>
        /// <param name="number">String representation of the number to be converted</param>
        /// <param name="fromBase">The current base of the number</param>
        /// <param name="toBase">The desired base to convert to</param>
        /// <returns></returns>
        public static string Convert(string number, int fromBase, int toBase)
        {
            /*if (string.IsNullOrEmpty(_charList))
                throw new FormatException("You must populate .CharList before calling Convert().");*/
            number = string.Join("", number.Split(new[] { " ", "-", ",", "." }, StringSplitOptions.RemoveEmptyEntries));
            unchecked
            {
                string result = null;
                /*try
                {*/
                int length = number.Length;
                result = string.Empty;
                List<int> nibbles = number.Select(c => CharList.IndexOf(c)).ToList();
                int newlen;
                do
                {
                    int value = 0;
                    newlen = 0;
                    for (int i = 0; i < length; ++i)
                    {
                        value = value * fromBase + nibbles[i];
                        if (value >= toBase)
                        {
                            if (newlen == nibbles.Count)
                                nibbles.Add(0);
                            nibbles[newlen++] = value / toBase;
                            value %= toBase;
                        }
                        else if (newlen > 0)
                        {
                            if (newlen == nibbles.Count)
                                nibbles.Add(0);
                            nibbles[newlen++] = 0;
                        }
                    }
                    length = newlen;
                    result = CharList[value] + result;
                }
                while (newlen != 0);
                /*}
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }*/
                return result;
            }
        }

        /// <summary>
        /// Converts the given decimal number to the numeral system with the
        /// specified radix (in the range [2, 36]).
        /// </summary>
        /// <param name="decimalNumber">The number to convert.</param>
        /// <param name="radix">The radix of the destination numeral system (in the range [2, 36]).</param>
        /// <returns></returns>
        public static string DecimalToArbitrarySystem(long decimalNumber, int radix)
        {
            const int BITS_IN_LONG = 64;
            const string DIGITS = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            if (radix < 2 || radix > DIGITS.Length)
                throw new ArgumentException("The radix must be >= 2 and <= " + DIGITS.Length.ToString());

            if (decimalNumber == 0)
                return "0";

            int index = BITS_IN_LONG - 1;
            long currentNumber = Math.Abs(decimalNumber);
            char[] charArray = new char[BITS_IN_LONG];

            while (currentNumber != 0)
            {
                int remainder = (int)(currentNumber % radix);
                charArray[index--] = DIGITS[remainder];
                currentNumber = currentNumber / radix;
            }

            string result = new String(charArray, index + 1, BITS_IN_LONG - index - 1);
            if (decimalNumber < 0)
            {
                result = "-" + result;
            }

            return result;
        }

        public static long ArbitraryToDecimalSystem(string number, int radix)
        {
            

            if (radix < 2 || radix > Digits.Length)
                throw new ArgumentException("The radix must be >= 2 and <= " +
                    Digits.Length.ToString());

            if (String.IsNullOrEmpty(number))
                return 0;

            // Make sure the arbitrary numeral system number is in upper case
            number = number.ToUpperInvariant();

            long result = 0;
            long multiplier = 1;
            for (int i = number.Length - 1; i >= 0; i--)
            {
                char c = number[i];
                if (i == 0 && c == '-')
                {
                    // This is the negative sign symbol
                    result = -result;
                    break;
                }

                int digit = Digits.IndexOf(c);
                if (digit == -1)
                    throw new ArgumentException(
                        "Invalid character in the arbitrary numeral system number",
                        "number");

                result += digit * multiplier;
                multiplier *= radix;
            }

            return result;
        }
    }
}